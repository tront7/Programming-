using System;
using CybersecurityBot;

[STAThread]
static void Main()
{
    try
    {
        VoiceGreeting.Play();
        UI.DisplayASCIILogo();
        new Chatbot().Start();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n  [Fatal] {ex.Message}");
        Console.ResetColor();
        Console.WriteLine("  Press any key to exit...");
        Console.ReadKey(intercept: true);
    }
}