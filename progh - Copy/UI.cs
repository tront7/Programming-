using System;
using System.Threading;

namespace CybersecurityBot
{
    public static class UI
    {
        private const int  DividerWidth  = 62;
        private const char DividerChar   = '═';
        private const int  TypingDelay   = 16;   // ms per character
        private const int  LogoShowDelay = 750;  // ms after logo renders

        // ── Output ────────────────────────────────────────────────────────────

        public static void PrintColored(string text, ConsoleColor color)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = original;
        }

        public static void PrintColoredInline(string text, ConsoleColor color)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = original;
        }

        public static void TypeText(string text, ConsoleColor color = ConsoleColor.White, int delay = TypingDelay)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                if (delay > 0) Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ForegroundColor = original;
        }

        // ── Dividers ──────────────────────────────────────────────────────────

        public static void PrintDivider(char symbol = DividerChar, int width = DividerWidth)
            => PrintColored(new string(symbol, width), ConsoleColor.DarkCyan);

        public static void PrintSectionHeader(string title)
        {
            Console.WriteLine();
            PrintDivider();
            PrintColored($"  {title}", ConsoleColor.Cyan);
            PrintDivider();
            Console.WriteLine();
        }

        // ── ASCII logo ────────────────────────────────────────────────────────

        public static void DisplayASCIILogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║                                                              ║");
            Console.WriteLine("  ║   ██████╗██╗   ██╗██████╗ ███████╗██████╗                   ║");
            Console.WriteLine("  ║  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗                  ║");
            Console.WriteLine("  ║  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝                  ║");
            Console.WriteLine("  ║  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗                  ║");
            Console.WriteLine("  ║  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║                  ║");
            Console.WriteLine("  ║   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝                  ║");
            Console.WriteLine("  ║                                                              ║");
            Console.WriteLine("  ║  ██╗     ██╗ █████╗ ███╗   ███╗                             ║");
            Console.WriteLine("  ║  ██║     ██║██╔══██╗████╗ ████║                             ║");
            Console.WriteLine("  ║  ██║     ██║███████║██╔████╔██║                             ║");
            Console.WriteLine("  ║  ██║     ██║██╔══██║██║╚██╔╝██║                             ║");
            Console.WriteLine("  ║  ███████╗██║██║  ██║██║ ╚═╝ ██║                             ║");
            Console.WriteLine("  ║  ╚══════╝╚═╝╚═╝  ╚═╝╚═╝     ╚═╝                             ║");
            Console.WriteLine("  ║                                                              ║");
            Console.WriteLine("  ║       ── A W A R E N E S S   B O T  🛡 ──                   ║");
            Console.WriteLine("  ║                                                              ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.ResetColor();
            PrintColored("  Protecting you in the digital world, one tip at a time.", ConsoleColor.DarkGreen);
            Console.WriteLine();
            Thread.Sleep(LogoShowDelay);
        }
    }
}