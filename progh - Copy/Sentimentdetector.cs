using System;
using System.Collections.Generic;

namespace CybersecurityBot
{
    /// <summary>
    /// Detects the user's emotional tone from their input and
    /// returns an appropriate empathetic prefix for the bot's response.
    /// </summary>
    public static class SentimentDetector
    {
        public enum Sentiment { Positive, Negative, Worried, Confused, Angry, Neutral }

        private static readonly Dictionary<Sentiment, List<string>> Keywords = new()
        {
            [Sentiment.Positive] = new() { "great", "thanks", "thank you", "awesome", "love", "cool",
                                           "good", "helpful", "nice", "excellent", "perfect", "amazing" },
            [Sentiment.Negative] = new() { "scared", "afraid", "fear", "terrified", "nervous",
                                           "stressed", "worried", "anxious", "unsafe", "vulnerable" },
            [Sentiment.Confused]  = new() { "confused", "don't understand", "not sure", "what is",
                                            "what does", "explain", "how does", "help me understand",
                                            "lost", "unclear", "complicated" },
            [Sentiment.Angry]     = new() { "angry", "frustrated", "annoyed", "hate", "stupid",
                                            "useless", "terrible", "worst", "awful", "ridiculous" },
            [Sentiment.Worried]   = new() { "hacked", "attacked", "breached", "stolen", "leaked",
                                            "compromised", "victim", "my account", "someone got in" },
        };

        private static readonly Dictionary<Sentiment, string[]> Prefixes = new()
        {
            [Sentiment.Positive]  = new[] {
                "I'm glad you're feeling positive! ",
                "Great to hear! ",
                "Love the enthusiasm! " },
            [Sentiment.Negative]  = new[] {
                "I understand this can feel overwhelming — you're not alone. ",
                "It's completely normal to feel nervous about this. ",
                "Don't worry, I'm here to help you through this. " },
            [Sentiment.Confused]  = new[] {
                "No worries, let me break this down simply for you. ",
                "Great question — let me explain that clearly. ",
                "I'll make this as simple as possible. " },
            [Sentiment.Angry]     = new[] {
                "I hear your frustration — let me help sort this out. ",
                "I'm sorry you're feeling this way. Let me do my best to help. ",
                "I understand — let's tackle this together. " },
            [Sentiment.Worried]   = new[] {
                "That sounds serious — let's address this right away. ",
                "I can hear your concern. Here's what you should do: ",
                "Don't panic — here are the steps to take immediately: " },
            [Sentiment.Neutral]   = new[] { "", "", "" },
        };

        private static readonly Random _rng = new();

        public static Sentiment Detect(string input)
        {
            string lower = input.ToLower();
            foreach (var kvp in Keywords)
                foreach (var word in kvp.Value)
                    if (lower.Contains(word))
                        return kvp.Key;
            return Sentiment.Neutral;
        }

        public static string GetPrefix(Sentiment sentiment)
        {
            var options = Prefixes[sentiment];
            return options[_rng.Next(options.Length)];
        }

        public static string GetEmojiForSentiment(Sentiment sentiment) => sentiment switch
        {
            Sentiment.Positive => "😊",
            Sentiment.Negative => "😟",
            Sentiment.Confused => "🤔",
            Sentiment.Angry    => "😤",
            Sentiment.Worried  => "⚠️",
            _                  => "🤖",
        };
    }
}