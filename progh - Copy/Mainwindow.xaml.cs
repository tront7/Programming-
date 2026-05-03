using System;
using System.IO;
using System.Media;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CybersecurityBot
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Window
    {
        private UserProfile?        _user;
        private ConversationContext _context = new();
        private bool                _nameEntered = false;
        private DispatcherTimer     _sessionTimer = new();

        // Colour palette
        private static readonly SolidColorBrush BotBubble    = new(Color.FromRgb(22, 27, 34));
        private static readonly SolidColorBrush UserBubble   = new(Color.FromRgb(31, 41, 55));
        private static readonly SolidColorBrush BotText      = new(Color.FromRgb(88, 166, 255));
        private static readonly SolidColorBrush UserText     = new(Color.FromRgb(230, 237, 243));
        private static readonly SolidColorBrush SystemText   = new(Color.FromRgb(139, 148, 158));
        private static readonly SolidColorBrush PositiveCol  = new(Color.FromRgb(35, 134, 54));
        private static readonly SolidColorBrush WarningCol   = new(Color.FromRgb(210, 153, 34));
        private static readonly SolidColorBrush ErrorCol     = new(Color.FromRgb(248, 81, 73));

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PlayVoiceGreeting();
            DrawAsciiArt();
            BuildTopicChips();
            SetupActivityLog();
            SetupSessionTimer();
            ShowWelcome();
        }

        // ── Voice greeting ────────────────────────────────────────────────────
       private void PlayVoiceGreeting()
{
    try
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
        if (File.Exists(path))
        {
            var player = new SoundPlayer(path);
            player.Play();
        }
        else
        {
            _context.LogActivity("Voice greeting file not found: greeting.wav");
        }
    }
    catch (Exception ex)
    {
        _context.LogActivity($"Voice greeting error: {ex.Message}");
    }
}
        // ── ASCII art in left panel ───────────────────────────────────────────
        private void DrawAsciiArt()

{
    AsciiArt.Text = "CYBER LIAM\n" +
                    "█▀▀ █▀█ █▀▀ █ █ █▀▀\n" +
                    "█▄▄ █▄█ ██▄ ▀▄▀ ██▄";
    AsciiArt.FontSize = 12; // Larger for test
    AsciiArt.Foreground = System.Windows.Media.Brushes.LimeGreen;
}
        // ── Topic chips ───────────────────────────────────────────────────────
        private void BuildTopicChips()
        {
            foreach (var topic in ResponseEngine.TopicList)
            {
                var btn = new Button
                {
                    Content = topic,
                    Style   = (Style)FindResource("TopicChip"),
                };
               btn.Click += (s, e) =>
{
    if (!_nameEntered)
    {
        AddSystemMessage("⚠ Please enter your name first (type it in the message box and press Enter).");
        InputBox.Focus();
        return;
    }
    string keyword = topic.Length > 2 ? topic.Substring(2).Trim() : topic;
    InputBox.Text = keyword;
    SendMessage();
};
                TopicsPanel.Children.Add(btn);
            }
        }

        // ── Activity log setup ────────────────────────────────────────────────
        private void SetupActivityLog()
        {
            _context.OnActivity += msg =>
            {
                Dispatcher.Invoke(() =>
                {
                    ActivityLog.Text += msg + "\n";
                    LogScroller.ScrollToEnd();
                });
            };
        }

        // ── Session timer ─────────────────────────────────────────────────────
        private void SetupSessionTimer()
        {
            _sessionTimer.Interval = TimeSpan.FromSeconds(30);
            _sessionTimer.Tick += (s, e) =>
            {
                if (_user != null)
                    SessionLabel.Text = $"⏱ {_user.SessionDuration}  |  💬 {_context.MessageCount} messages";
            };
            _sessionTimer.Start();
        }

        // ── Initial welcome ───────────────────────────────────────────────────
        private void ShowWelcome()
        {
            AddBotMessage("👋 Welcome to the Cybersecurity Awareness Bot!");
            AddBotMessage("I'm here to help you stay safe in the digital world. 🛡");
            AddSystemMessage("To get started, please enter your name below.");
            InputBox.Focus();
        }

        // ── Send message flow ─────────────────────────────────────────────────
private void SendMessage()
{
    string input = InputBox.Text.Trim();
    if (string.IsNullOrWhiteSpace(input)) return;

    InputBox.Clear();

    // Exit command handling
    if (IsExitCommand(input))
    {
        ExitApplication();
        return;
    }

    // Name capture phase
    if (!_nameEntered)
    {
        if (input.Length < 2)
        {
            AddSystemMessage("⚠  Please enter a valid name (at least 2 characters).");
            return;
        }
       _user = new UserProfile(input);
_nameEntered = true;

AddUserMessage(input);
AddBotMessage($"{_user.TimeGreeting}, {_user.FormattedName}! 👋 Great to meet you!");
AddSystemMessage("💡 Tip: You can type 'exit', 'quit', or 'bye' at any time to close the application."); // <-- ADD THIS
AddBotMessage("Here's what I can help you with — click any topic on the left, or just type your question:");               ShowTopicSummary();
                StatusLabel.Text = $"Chatting with {_user.FormattedName}";
                _context.LogActivity($"Session started for {_user.FormattedName}");
                return;
            }

    // Main conversation
    AddUserMessage(input);
    _user!.MessageCount++;


            // Detect sentiment
            var sentiment = SentimentDetector.Detect(input);
            string prefix  = SentimentDetector.GetPrefix(sentiment);
            string emoji   = SentimentDetector.GetEmojiForSentiment(sentiment);

            // Show sentiment indicator
            if (sentiment != SentimentDetector.Sentiment.Neutral)
            {
                SentimentBar.Visibility = Visibility.Visible;
                SentimentLabel.Text = $"{emoji} Detected tone: {sentiment}  —  {prefix.TrimEnd()}";
            }
            else
            {
                SentimentBar.Visibility = Visibility.Collapsed;
            }

            // Check for follow-up
            if (_context.IsFollowUp(input) && !string.IsNullOrEmpty(_context.LastTopic))
            {
                string? followUp = ResponseEngine.GetResponse(_context.LastTopic);
                if (followUp != null)
                {
                    AddBotMessage($"{prefix}Here's more on {_context.LastTopic}:\n\n{followUp}");
                    _context.LogActivity($"Follow-up on: {_context.LastTopic}");
                    return;
                }
            }

            // Check for memory recall request
            if (input.ToLower().Contains("remember") || input.ToLower().Contains("what do you know about me"))
            {
                string recap = _context.BuildMemoryRecap();
                if (!string.IsNullOrEmpty(recap))
                {
                    AddBotMessage($"Based on our conversation, I know that {recap}. Is there anything specific you'd like help with regarding these?");
                    _context.LogActivity("Memory recall requested");
                    return;
                }
                else
                {
                    AddBotMessage("I haven't learned much about you yet! Tell me what device you use, what browser you prefer, or what security topics concern you most.");
                    return;
                }
            }

            // Get topic response
            string? response = ResponseEngine.GetResponse(input);
            string? topicKey = ResponseEngine.GetLastTopicKey(input);

            if (topicKey != null)
                _context.RecordMessage(input, topicKey);

            if (response != null)
            {
                string fullResponse = string.IsNullOrEmpty(prefix)
                    ? response
                    : $"{prefix}\n{response}";
                AddBotMessage(fullResponse);

                // Memory recall hint
                string recap = _context.BuildMemoryRecap();
                if (!string.IsNullOrEmpty(recap) && _context.MessageCount % 4 == 0)
                    AddSystemMessage($"💭 I remember: {recap}. This might be relevant to your question!");
            }
            else
            {
                AddBotMessage(
                    $"I didn't quite catch that, {_user.FormattedName}. 🤔\n\n" +
                    "Try asking about one of the topics on the left panel, or type things like:\n" +
                    "  • \"Tell me about phishing\"\n" +
                    "  • \"How do I create a strong password?\"\n" +
                    "  • \"What is ransomware?\"\n" +
                    "  • \"Give me a VPN tip\"");
                _context.LogActivity("Unrecognised input received");
            }
        }

        // ── Topic summary on login ────────────────────────────────────────────
        private void ShowTopicSummary()
        {
            AddSystemMessage(
                "🔐 passwords  🎣 phishing  🌐 safe browsing  🦠 malware  🔏 privacy\n" +
                "🎭 social engineering  🔑 2FA  🔒 VPN  💰 ransomware  📡 WiFi\n" +
                "🔓 encryption  🚨 data breach  🔴 hacking  🛡 firewall  📧 spam");
        }

        // ── Chat bubble builders ──────────────────────────────────────────────
        private void AddBotMessage(string text)
        {
            AddMessage($"🤖 CyberBot\n{text}", BotBubble, BotText, HorizontalAlignment.Left);
        }

        private void AddUserMessage(string text)
        {
             
            string name = _user?.FormattedName ?? "You";
            AddMessage($"👤 {name}\n{text}", UserBubble, UserText, HorizontalAlignment.Right);
           
        }

        private void AddSystemMessage(string text)
{
    var tb = new TextBlock
    {
        Text              = text,
        Foreground        = SystemText,
        FontFamily        = new FontFamily("Segoe UI"),
        FontSize          = 13,   // was 11
        FontStyle         = FontStyles.Italic,
        TextWrapping      = TextWrapping.Wrap,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin            = new Thickness(0, 4, 0, 4),
    };
    ChatPanel.Children.Add(tb);
    ScrollToBottom();
}

        private void AddMessage(string text, SolidColorBrush bg, SolidColorBrush fg,
                                HorizontalAlignment align)
        {
            var border = new Border
            {
                Background          = bg,
                CornerRadius        = new CornerRadius(10),
                Padding             = new Thickness(14, 10, 14, 10),
                Margin              = new Thickness(
                    align == HorizontalAlignment.Right ? 80 : 0, 4,

                HorizontalAlignment = align,
                MaxWidth            = 620,
            };

            var tb = new TextBlock
            {
                Text         = text,
                Foreground   = fg,
                FontFamily   = new FontFamily("Consolas"),
                FontSize     = 14,
                TextWrapping = TextWrapping.Wrap,
                LineHeight   = 18,
            };

            // Make first line (name) bold/smaller
            if (text.Contains('\n'))
            {
                var lines   = text.Split('\n', 2);
                var nameRun = new System.Windows.Documents.Run(lines[0] + "\n")
                {
                    FontSize   = 11,
                    FontFamily = new FontFamily("Segoe UI"),
                    FontWeight = FontWeights.SemiBold,
                    Foreground = SystemText,
                };
                var bodyRun = new System.Windows.Documents.Run(lines[1]);
                tb.Inlines.Clear();
                tb.Inlines.Add(nameRun);
                tb.Inlines.Add(bodyRun);
            }

            border.Child = tb;
            ChatPanel.Children.Add(border);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            Dispatcher.InvokeAsync(() => ChatScroller.ScrollToEnd(),
                DispatcherPriority.Background);
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void SendButton_Click(object sender, RoutedEventArgs e) => SendMessage();

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SendMessage();
        }

       private void ClearButton_Click(object sender, RoutedEventArgs e)
{
    ChatPanel.Children.Clear();
    SentimentBar.Visibility = Visibility.Collapsed;
    _context.LogActivity("Chat cleared by user");
    if (_nameEntered && _user != null)
        AddBotMessage($"Chat cleared! What else can I help you with, {_user.FormattedName}?");
    else
        ShowWelcome(); // shows name prompt again
}
        private bool IsExitCommand(string input)
{
    string trimmed = input.Trim().ToLower();
    return trimmed == "exit" || trimmed == "quit" || trimmed == "bye";
}
private async void ExitApplication()
{
    AddBotMessage("👋 Goodbye! Stay safe out there. The application will close in 2 seconds.");
    _context.LogActivity("User exited the application");
    await System.Threading.Tasks.Task.Delay(2000);
    Application.Current.Shutdown();
}

    }

}