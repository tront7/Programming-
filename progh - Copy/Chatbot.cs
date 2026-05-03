using System;

namespace CybersecurityBot
{
    /// <summary>
    /// Orchestrates the console chat session: greeting, conversation loop, and farewell.
    /// </summary>
    public sealed class Chatbot
    {
        private UserProfile?         _user;
        private readonly ConversationContext _ctx = new();

        public void Start()
        {
            _ctx.OnActivity += _ => { }; // Extend here to wire up file logging, etc.
            GreetUser();
            RunLoop();
        }

        // ── Greeting ──────────────────────────────────────────────────────────

        private void GreetUser()
        {
            UI.PrintSectionHeader("WELCOME");
            UI.TypeText("  Hello! I'm Liam — your Cybersecurity Awareness Bot.", ConsoleColor.Cyan);
            UI.TypeText("  I'm here to help you stay safe in the digital world.", ConsoleColor.Cyan);
            Console.WriteLine();

            _user = new UserProfile(InputValidator.GetUserName());
            Console.WriteLine();

            UI.TypeText($"  {_user.TimeGreeting}, {_user.FormattedName}! 👋", ConsoleColor.Green);
            UI.TypeText("  Ask me anything, or type 'help' to browse topics.", ConsoleColor.Green);
            UI.TypeText("  Type 'exit', 'quit', or 'bye' to end the session.", ConsoleColor.DarkGray);
            Console.WriteLine();

            _ctx.Log($"Session started for {_user.FormattedName}");
        }

        // ── Main loop ─────────────────────────────────────────────────────────

        private void RunLoop()
        {
            while (true)
            {
                UI.PrintDivider('─');
                UI.PrintColoredInline($"  {_user!.FormattedName}: ", ConsoleColor.White);

                string input = Console.ReadLine() ?? string.Empty;

                if (IsExit(input)) { ShowFarewell(); break; }
                if (!InputValidator.IsValid(input)) continue;

                Console.WriteLine();
                Respond(input);
                Console.WriteLine();
            }
        }

        // ── Response dispatch ─────────────────────────────────────────────────

        private void Respond(string input)
        {
            var    sentiment = SentimentDetector.Detect(input);
            string prefix    = SentimentDetector.GetPrefix(sentiment);
            string emoji     = SentimentDetector.GetEmoji(sentiment);

            // Memory recall
            if (IsMemoryQuery(input))
            {
                string recap = _ctx.BuildMemoryRecap();
                UI.TypeText(string.IsNullOrEmpty(recap)
                    ? $"  🤖 I haven't picked up much about you yet, {_user!.FormattedName}. " +
                      "Mention your device, browser, or a concern and I'll remember it."
                    : $"  🤖 I know that {recap}. Anything specific I can help with?",
                    ConsoleColor.Cyan, delay: 10);
                return;
            }

            // Follow-up
            if (_ctx.IsFollowUp(input) && !string.IsNullOrEmpty(_ctx.LastTopic))
            {
                string? more = ResponseEngine.GetResponse(_ctx.LastTopic);
                if (more is not null)
                {
                    string msg = string.IsNullOrEmpty(prefix)
                        ? $"Here's more on '{_ctx.LastTopic}':\n\n{more}"
                        : $"{prefix}Here's more on '{_ctx.LastTopic}':\n\n{more}";
                    UI.TypeText($"  {emoji} {msg}", ConsoleColor.Cyan, delay: 10);
                    return;
                }
            }

            // Standard lookup
            string? response = ResponseEngine.GetResponse(input);
            string? topicKey = ResponseEngine.GetMatchedTopicKey(input);

            if (topicKey is not null) _ctx.RecordMessage(input, topicKey);

            if (response is not null)
            {
                string full = string.IsNullOrEmpty(prefix) ? response : $"{prefix}\n{response}";
                UI.TypeText($"  {emoji} {full}", ConsoleColor.Cyan, delay: 10);

                // Periodic memory recap every 4 messages
                string recap = _ctx.BuildMemoryRecap();
                if (!string.IsNullOrEmpty(recap) && _ctx.MessageCount % 4 == 0)
                {
                    Console.WriteLine();
                    UI.PrintColored($"  💭 Note: I remember that {recap}.", ConsoleColor.DarkGray);
                }
            }
            else
            {
                UI.TypeText(
                    $"  🤔 I didn't catch that, {_user!.FormattedName}.\n\n" +
                    "     Try: passwords · phishing · malware · VPN · 2FA · ransomware\n" +
                    "     Or type 'help' for the full topic list.",
                    ConsoleColor.DarkYellow, delay: 10);
            }
        }

        // ── Farewell ──────────────────────────────────────────────────────────

        private void ShowFarewell()
        {
            Console.WriteLine();
            UI.PrintDivider();
            UI.TypeText($"  Stay safe out there, {_user!.FormattedName}! 🛡  Goodbye!", ConsoleColor.Green);
            UI.TypeText($"  {_ctx.MessageCount} message(s) sent · session time: {_user.SessionDuration}.",
                ConsoleColor.DarkGray);
            UI.PrintDivider();
            Console.WriteLine();
            _ctx.Log($"Session ended — {_ctx.MessageCount} messages");
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static bool IsExit(string input) =>
            input.Trim().ToLowerInvariant() is "exit" or "quit" or "bye";

        private static bool IsMemoryQuery(string input)
        {
            string lower = input.ToLowerInvariant();
            return lower.Contains("what do you know about me")
                || lower.Contains("what do you remember");
        }
    }
}