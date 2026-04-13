using System;
using System.Threading;

namespace CybersecurityBot
{
    public static class UI
    {
        // ── Colours ──────────────────────────────────────────────────────────
        public static void PrintColored(string text, ConsoleColor color)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = original;
        }

        public static void TypeText(string text, ConsoleColor color = ConsoleColor.White, int delay = 18)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ForegroundColor = original;
        }

        // ── Dividers ─────────────────────────────────────────────────────────
        public static void PrintDivider(char symbol = '═', int width = 60)
        {
            PrintColored(new string(symbol, width), ConsoleColor.DarkCyan);
        }

        public static void PrintSectionHeader(string title)
        {
            Console.WriteLine();
            PrintDivider();
            PrintColored($"  {title}", ConsoleColor.Cyan);
            PrintDivider();
            Console.WriteLine();
        }

        // ── ASCII Logo ────────────────────────────────────────────────────────
        public static void DisplayASCIILogo()
        {
            Console.Clear();
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
             Console.WriteLine(@"  ╔══════════════════════════════════════════════════════════╗");
             Console.WriteLine(@"  ║   _      ___    _    __  __                              ║");
             Console.WriteLine(@"  ║  | |    |_ _|  / \  |  \/  |                             ║");
             Console.WriteLine(@"  ║  | |     | |  / _ \ | |\/| |                             ║");
             Console.WriteLine(@"  ║  | |___  | | / ___ \| |  | |                             ║");
             Console.WriteLine(@"  ║  |_____| |___/_/   \_\_| |_|                             ║");
             Console.WriteLine(@"  ║                                                          ║");
             Console.WriteLine(@"  ║         A W A R E N E S S   B O T  🛡                    ║");
             Console.WriteLine(@"  ╚══════════════════════════════════════════════════════════╝");
             Console.WriteLine();
            Console.ForegroundColor = original;

            PrintColored("  Protecting you in the digital world, one tip at a time.", ConsoleColor.DarkGreen);
            Console.WriteLine();
            Thread.Sleep(800);
        }
    }
}