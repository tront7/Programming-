using System;

namespace CybersecurityBot
{
    public class Chatbot
    {
        private string _userName = string.Empty;

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

            _userName = InputValidator.GetUserName();

            Console.WriteLine();
            UI.TypeText($"  Great to meet you, {_userName}! 👋", ConsoleColor.Green);
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
                Console.Write($"  {_userName}: ");
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
                        $"  🤖 Bot: I didn't quite understand that, {_userName}. Could you rephrase?\n" +
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
            string trimmed = input.Trim().ToLower();
            return trimmed == "exit" || trimmed == "quit" || trimmed == "bye";
        }

        private void Farewell()
        {
            Console.WriteLine();
            UI.PrintDivider();
            UI.TypeText($"  Stay safe out there, {_userName}! 🛡  Goodbye!", ConsoleColor.Green);
            UI.PrintDivider();
            Console.WriteLine();
        }
    }
}