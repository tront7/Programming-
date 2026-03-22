using System;

namespace CybersecurityBot
{
    public class Chatbot
    {
        // Uses UserProfile with automatic properties instead of a plain string
        private UserProfile _user = new UserProfile(string.Empty);

        public void Start()
        {
            GreetUser();
            RunConversationLoop();
        }

        // ── Greeting ──────────────────────────────────────────────────────────
        private void GreetUser()
        {
            UI.PrintSectionHeader("WELCOME");

            UI.TypeText("  Hello! I am the Cybersecurity Awareness Bot.", ConsoleColor.Cyan);
            UI.TypeText("  I'm here to help you stay safe in the digital world.", ConsoleColor.Cyan);
            Console.WriteLine();

            // Capture name and store in UserProfile
            string rawName = InputValidator.GetUserName();
            _user = new UserProfile(rawName);

            Console.WriteLine();

            // TimeGreeting uses the SessionStart automatic property
            // FormattedName applies string manipulation (title-case) via automatic property
            UI.TypeText($"  {_user.TimeGreeting}, {_user.FormattedName}! 👋", ConsoleColor.Green);
            UI.TypeText($"  Ask me anything about cybersecurity, or type 'help' to see topics.", ConsoleColor.Green);
            UI.TypeText("  Type 'exit' or 'quit' at any time to leave.", ConsoleColor.DarkGray);
            Console.WriteLine();
        }

        // ── Main loop ─────────────────────────────────────────────────────────
        private void RunConversationLoop()
        {
            while (true)
            {
                UI.PrintDivider('─');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  {_user.FormattedName}: ");
                Console.ResetColor();

                string input = Console.ReadLine() ?? string.Empty;

                // Exit check
                if (IsExitCommand(input))
                {
                    Farewell();
                    break;
                }

                // Validate input
                if (!InputValidator.IsValid(input))
                {
                    continue;
                }

                // Increment message count automatic property
                _user.MessageCount++;

                // Store last topic using string manipulation (Trim + Substring)
                _user.LastTopic = input.Trim().Length > 30
                    ? input.Trim().Substring(0, 30) + "..."
                    : input.Trim();

                // Get and display response
                string? response = ResponseEngine.GetResponse(input);

                Console.WriteLine();
                if (response != null)
                {
                    UI.TypeText($"  🤖 Bot: {response}", ConsoleColor.Cyan, delay: 12);
                }
                else
                {
                    UI.TypeText(
                        $"  🤖 Bot: I didn't quite understand that, {_user.FormattedName}. Could you rephrase?\n" +
                        "         Try asking about: passwords, phishing, safe browsing, malware, privacy, or 2FA.",
                        ConsoleColor.DarkYellow, delay: 12);
                }

                Console.WriteLine();
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private bool IsExitCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            // String manipulation: Trim() and ToLower() to normalise input
            string trimmed = input.Trim().ToLower();
            return trimmed == "exit" || trimmed == "quit" || trimmed == "bye";
        }

        private void Farewell()
        {
            Console.WriteLine();
            UI.PrintDivider();
            // Uses FormattedName, MessageCount and SessionDuration automatic properties
            UI.TypeText($"  Stay safe out there, {_user.FormattedName}! 🛡  Goodbye!", ConsoleColor.Green);
            UI.TypeText($"  You sent {_user.MessageCount} message(s) in this session ({_user.SessionDuration}).",
                ConsoleColor.DarkGray);
            UI.PrintDivider();
            Console.WriteLine();
        }
    }
}