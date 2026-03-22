using System;
using CybersecurityBot;

class Program
{
    static void Main(string[] args)
    {
        VoiceGreeting.Play();
        UI.DisplayASCIILogo();
        Chatbot bot = new Chatbot();
        bot.Start();
    }
}