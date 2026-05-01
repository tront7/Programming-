using System;
using System.Collections.Generic;

namespace CybersecurityBot
{
    /// <summary>
    /// Tracks conversation state: last topic, follow-up detection,
    /// and memory of things the user has shared.
    /// </summary>
    public class ConversationContext
    {
        // ── Automatic properties ──────────────────────────────────────────────
        public string LastTopic       { get; set; } = string.Empty;
        public string LastResponse    { get; set; } = string.Empty;
        public int    MessageCount    { get; set; } = 0;
        public string UserName        { get; set; } = string.Empty;
        public string UserConcern     { get; set; } = string.Empty;  // remembered concern
        public string UserDevice      { get; set; } = string.Empty;  // remembered device
        public string UserBrowser     { get; set; } = string.Empty;  // remembered browser

        // ── Memory store — things user has mentioned ──────────────────────────
        private readonly List<string> _memoryLog = new();

        public IReadOnlyList<string> MemoryLog => _memoryLog;

        // ── Follow-up keywords ────────────────────────────────────────────────
        private static readonly List<string> FollowUpPhrases = new()
        {
            "tell me more", "more info", "explain more", "give me more",
            "another tip", "more tips", "elaborate", "go on", "continue",
            "and?", "what else", "anything else", "more please", "expand"
        };

        // ── Delegates ─────────────────────────────────────────────────────────
        // Delegate for logging activity — satisfies the delegate requirement
        public delegate void ActivityLogHandler(string message);
        public event ActivityLogHandler? OnActivity;

        // ── Methods ───────────────────────────────────────────────────────────
        public bool IsFollowUp(string input)
        {
            string lower = input.Trim().ToLower();
            foreach (var phrase in FollowUpPhrases)
                if (lower.Contains(phrase))
                    return true;
            return false;
        }

        public void RecordMessage(string input, string topic)
        {
            MessageCount++;
            LastTopic = topic;

            // Extract memory — remember what the user mentions
            ExtractMemory(input);

            // Fire activity log event
            OnActivity?.Invoke($"[{DateTime.Now:HH:mm:ss}] User asked about: {topic}");
        }

        private void ExtractMemory(string input)
        {
            string lower = input.ToLower();

            // Remember device mentions
            if (lower.Contains("windows"))   UserDevice  = "Windows";
            if (lower.Contains("mac"))       UserDevice  = "Mac";
            if (lower.Contains("android"))   UserDevice  = "Android";
            if (lower.Contains("iphone") || lower.Contains("ios")) UserDevice = "iPhone";

            // Remember browser mentions
            if (lower.Contains("chrome"))    UserBrowser = "Chrome";
            if (lower.Contains("firefox"))   UserBrowser = "Firefox";
            if (lower.Contains("edge"))      UserBrowser = "Edge";
            if (lower.Contains("safari"))    UserBrowser = "Safari";

            // Remember concerns
            if (lower.Contains("hacked"))    UserConcern = "being hacked";
            if (lower.Contains("scammed"))   UserConcern = "being scammed";
            if (lower.Contains("phishing"))  UserConcern = "phishing";
            if (lower.Contains("malware"))   UserConcern = "malware";
            if (lower.Contains("password"))  UserConcern = "password security";
        }

        public void LogActivity(string message)
        {
            _memoryLog.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            OnActivity?.Invoke(message);
        }

        public string BuildMemoryRecap()
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(UserDevice))  parts.Add($"you're on {UserDevice}");
            if (!string.IsNullOrEmpty(UserBrowser)) parts.Add($"you use {UserBrowser}");
            if (!string.IsNullOrEmpty(UserConcern)) parts.Add($"you're concerned about {UserConcern}");
            return parts.Count > 0 ? string.Join(", ", parts) : string.Empty;
        }
    }
}