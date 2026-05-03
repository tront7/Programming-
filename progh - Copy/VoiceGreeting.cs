using System;
using System.IO;
using System.Media;
using System.Runtime.Versioning;

namespace CybersecurityBot
{
    [SupportedOSPlatform("windows")]
    public static class VoiceGreeting
    {
        private const string FileName = "greeting.wav";

        public static void Play()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

            if (!File.Exists(path))
            {
                UI.PrintColored(
                    $"  [ℹ  Place '{FileName}' in the output folder to enable the voice greeting.]",
                    ConsoleColor.DarkGray);
                Console.WriteLine();
                return;
            }

            try   { using var p = new SoundPlayer(path); p.PlaySync(); }
            catch (Exception ex)
            {
                UI.PrintColored($"  [⚠  Voice greeting failed: {ex.Message}]", ConsoleColor.DarkYellow);
                Console.WriteLine();
            }
        }
    }
}