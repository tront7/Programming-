using System;                  // Provides basic system functionality
using CybersecurityBot;        // Allows access to classes in the CybersecurityBot namespace

/// <summary>
/// Entry point of the Cybersecurity Awareness Bot application.
/// This class is responsible for starting the program and initializing core components.
/// </summary>
class Program
{
    /// <summary>
    /// Main method where the application begins execution.
    /// </summary>
    /// <param name="args">Command-line arguments (not used in this application)</param>
    static void Main(string[] args)
    {
        // Play the voice greeting (if greeting.wav exists in the output folder)
        VoiceGreeting.Play();

        // Display the ASCII logo and welcome branding
        UI.DisplayASCIILogo();

        // Create an instance of the Chatbot class
        Chatbot bot = new Chatbot();

        // Start the chatbot interaction (greeting + conversation loop)
        bot.Start();
    }
}