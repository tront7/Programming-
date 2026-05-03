using System;

namespace CybersecurityBot
{
    public static class InputValidator
    {
        private const int MinLength = 2;

        public static bool IsValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                UI.PrintColored("  ⚠  Please type something before pressing Enter.", ConsoleColor.Yellow);
                return false;
            }

            if (input.Trim().Length < MinLength)
            {
                UI.PrintColored("  ⚠  That's too short — could you be more specific?", ConsoleColor.Yellow);
                return false;
            }

            return true;
        }

        public static string GetUserName()
        {
            UI.PrintColored("  What's your name?", ConsoleColor.DarkCyan);
            Console.Write("  ➤ ");

            string? name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || name.Trim().Length < MinLength)
            {
                UI.PrintColored($"  ⚠  Name must be at least {MinLength} characters. Please try again.", ConsoleColor.Yellow);
                Console.Write("  ➤ ");
                name = Console.ReadLine();
            }

            return name.Trim();
        }
    }
}