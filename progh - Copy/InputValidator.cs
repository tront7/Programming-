using System;

namespace CybersecurityBot
{
    /// <summary>
    /// Responsible for validating user input and ensuring data entered
    /// into the system meets basic requirements.
    /// Demonstrates input validation and user interaction control.
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Validates general user input.
        /// Ensures the input is not empty, not whitespace, and is meaningful.
        /// </summary>
        /// <param name="input">The user's input string</param>
        /// <returns>True if valid, false if invalid</returns>
        public static bool IsValid(string input)
        {
            // Check if input is null, empty, or only whitespace
            if (string.IsNullOrWhiteSpace(input))
            {
                // Display warning message to the user
                UI.PrintColored(
                    "  ⚠  I didn't catch that. Please type something!", 
                    ConsoleColor.Yellow
                );
                return false; // Invalid input
            }

            // Check if input is too short to be meaningful
            if (input.Trim().Length < 2)
            {
                // Inform user to provide more detailed input
                UI.PrintColored(
                    "  ⚠  That's too short. Could you be a bit more specific?", 
                    ConsoleColor.Yellow
                );
                return false; // Invalid input
            }

            return true; // Input passed all validation checks
        }

        /// <summary>
        /// Prompts the user to enter their name.
        /// Continues prompting until a valid name (minimum 2 characters) is provided.
        /// </summary>
        /// <returns>A valid, trimmed user name</returns>
        public static string GetUserName()
{
    Console.Write("Please enter your name: ");
    string? name = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(name) || name.Length < 2)
    {
        Console.Write("Name must be at least 2 characters. Please try again: ");
        name = Console.ReadLine();
    }
    return name.Trim();
}
    }
}