using System;              // Provides basic system functionality
using System.Threading;   // Allows use of Thread.Sleep for delays

namespace CybersecurityBot
{
    /// <summary>
    /// Handles all console user interface elements such as colors,
    /// text animation, dividers, and ASCII art display.
    /// </summary>
    public static class UI
    {
        // ── Colours ──────────────────────────────────────────────────────────

        /// <summary>
        /// Prints text to the console in a specified color,
        /// then resets the color back to the original.
        /// </summary>
        /// <param name="text">The message to display</param>
        /// <param name="color">The color to use for the text</param>
        public static void PrintColored(string text, ConsoleColor color)
        {
            ConsoleColor original = Console.ForegroundColor; // Store current color
            Console.ForegroundColor = color;                  // Set new color
            Console.WriteLine(text);                          // Print text
            Console.ForegroundColor = original;               // Reset to original color
        }

        /// <summary>
        /// Simulates typing animation by printing one character at a time.
        /// </summary>
        /// <param name="text">Text to display</param>
        /// <param name="color">Text color (default: White)</param>
        /// <param name="delay">Delay in milliseconds between characters</param>
        public static void TypeText(string text, ConsoleColor color = ConsoleColor.White, int delay = 18)
        {
            ConsoleColor original = Console.ForegroundColor; // Store original color
            Console.ForegroundColor = color;                 // Set desired color

            // Loop through each character to simulate typing
            foreach (char c in text)
            {
                Console.Write(c);       // Print character
                Thread.Sleep(delay);   // Pause briefly
            }

            Console.WriteLine();                     // Move to next line
            Console.ForegroundColor = original;     // Reset color
        }

        // ── Dividers ─────────────────────────────────────────────────────────

        /// <summary>
        /// Prints a horizontal divider line using a specified symbol and width.
        /// </summary>
        /// <param name="symbol">Character used for the divider</param>
        /// <param name="width">Length of the divider</param>
        public static void PrintDivider(char symbol = '═', int width = 60)
        {
            // Creates a string of repeated symbols and prints it
            PrintColored(new string(symbol, width), ConsoleColor.DarkCyan);
        }

        /// <summary>
        /// Displays a formatted section header with divider lines above and below.
        /// </summary>
        /// <param name="title">The section title</param>
        public static void PrintSectionHeader(string title)
        {
            Console.WriteLine();           // Add spacing
            PrintDivider();                // Top divider
            PrintColored($"  {title}", ConsoleColor.Cyan); // Title text
            PrintDivider();                // Bottom divider
            Console.WriteLine();           // Add spacing
        }

        // ── ASCII Logo ────────────────────────────────────────────────────────

        /// <summary>
        /// Clears the console and displays the application ASCII logo.
        /// Also shows a tagline with a short delay.
        /// </summary>
        public static void DisplayASCIILogo()
        {
            Console.Clear(); // Clears previous console output

            ConsoleColor original = Console.ForegroundColor; // Store original color
            Console.ForegroundColor = ConsoleColor.Green;    // Set logo color

            // ASCII art logo for branding and visual appeal
            Console.WriteLine();
            Console.WriteLine(@"  ╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine(@"  ║                                                              ║");
            Console.WriteLine(@"  ║   ██████╗██╗   ██╗██████╗ ███████╗██████╗                    ║");
            Console.WriteLine(@"  ║  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗                   ║");
            Console.WriteLine(@"  ║  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝                   ║");
            Console.WriteLine(@"  ║  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗                   ║");
            Console.WriteLine(@"  ║  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║                   ║");
            Console.WriteLine(@"  ║   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝                   ║");
            Console.WriteLine(@"  ║                                                              ║");
            Console.WriteLine(@"  ║  ██╗     ██╗ █████╗ ███╗   ███╗                              ║");
            Console.WriteLine(@"  ║  ██║     ██║██╔══██╗████╗ ████║                              ║");
            Console.WriteLine(@"  ║  ██║     ██║███████║██╔████╔██║                              ║");
            Console.WriteLine(@"  ║  ██║     ██║██╔══██║██║╚██╔╝██║                              ║");
            Console.WriteLine(@"  ║  ███████╗██║██║  ██║██║ ╚═╝ ██║                              ║");
            Console.WriteLine(@"  ║  ╚══════╝╚═╝╚═╝  ╚═╝╚═╝     ╚═╝                              ║");
            Console.WriteLine(@"  ║                                                              ║");
            Console.WriteLine(@"  ║      ── A W A R E N E S S   B O T   🛡 ──                    ║");
            Console.WriteLine(@"  ║                                                              ║");
            Console.WriteLine(@"  ╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.ForegroundColor = original; // Reset color after logo

            // Display tagline below the logo
            PrintColored("  Protecting you in the digital world, one tip at a time.", ConsoleColor.DarkGreen);
            Console.WriteLine();

            Thread.Sleep(800); // Small delay for smoother user experience
        }
    }
}