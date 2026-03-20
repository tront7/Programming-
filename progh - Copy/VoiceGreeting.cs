using System;
using System.Media;
using System.IO;

namespace CybersecurityBot
{
    public static class VoiceGreeting
    {
        public static void Play()
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.PlaySync(); // plays and waits until done
                    }
                }
                else
                {
                    UI.PrintColored("[Voice greeting file not found. Place 'greeting.wav' in the output folder.]", ConsoleColor.DarkYellow);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                UI.PrintColored($"[Could not play voice greeting: {ex.Message}]", ConsoleColor.DarkYellow);
                Console.WriteLine();
            }
        }
    }
}
