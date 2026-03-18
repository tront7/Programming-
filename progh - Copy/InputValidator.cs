using System;

namespace CybersecurityBot
{
    public static class InputValidator
    {
        /// <summary>
        /// Returns true if the input is valid (non-empty, non-whitespace).
        /// Prints a helpful error message if invalid.
        /// </summary>
        public static bool IsValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                UI.PrintColored("  ⚠  I didn't catch that. Please type something!", ConsoleColor.Yellow);
                return false;
            }

            if (input.Trim().Length < 2)
            {
                UI.PrintColored("  ⚠  That's too short. Could you be a bit more specific?", ConsoleColor.Yellow);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prompts the user for their name, re-prompting until a valid name is entered.
        /// </summary>
        public static string GetUserName()
        {
            string name = string.Empty;

            while (true)
            {
                Console.Write("  Enter your name: ");
                name = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(name) && name.Trim().Length >= 2)
                    break;

                UI.PrintColored("  ⚠  Please enter a valid name (at least 2 characters).", ConsoleColor.Yellow);
            }

            return name.Trim();
        }
    }
}