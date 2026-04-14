using System;                 // Provides basic system functionality
using System.Media;           // Enables audio playback (SoundPlayer)
using System.IO;              // Allows file handling (checking if file exists, paths)

namespace CybersecurityBot
{
    /// <summary>
    /// Handles playing a voice greeting when the application starts.
    /// Demonstrates file handling, exception handling, and audio playback.
    /// </summary>
    public static class VoiceGreeting
    {
        /// <summary>
        /// Plays the greeting.wav audio file if it exists in the output directory.
        /// </summary>
        public static void Play()
        {
            try
            {
                // Build the full file path to "greeting.wav" in the application's base directory
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "greeting.wav"
                );

                // Check if the audio file exists before attempting to play it
                if (File.Exists(audioPath))
                {
                    // Create a SoundPlayer object to play the .wav file
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.PlaySync(); // Plays the sound and waits until it finishes
                    }
                }
                else
                {
                    // Inform the user if the file is missing
                    UI.PrintColored(
                        "[Voice greeting file not found. Place 'greeting.wav' in the output folder.]", 
                        ConsoleColor.DarkYellow
                    );
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during audio playback
                // Example: unsupported file format or missing dependencies
                UI.PrintColored(
                    $"[Could not play voice greeting: {ex.Message}]", 
                    ConsoleColor.DarkYellow
                );
                Console.WriteLine();
            }
        }
    }
}