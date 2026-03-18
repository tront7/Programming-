using System;

namespace CybersecurityBot
{
    /// <summary>
    /// Stores information about the current user session.
    /// Demonstrates the use of automatic properties as required by the POE.
    /// </summary>
    public class UserProfile
    {
        // ── Automatic properties ──────────────────────────────────────────────

        /// <summary>The user's name, captured at the start of the session.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>The date and time the session started.</summary>
        public DateTime SessionStart { get; set; } = DateTime.Now;

        /// <summary>Total number of messages sent by the user this session.</summary>
        public int MessageCount { get; set; } = 0;

        /// <summary>The last topic the user asked about.</summary>
        public string LastTopic { get; set; } = string.Empty;

        // ── Derived / computed properties ─────────────────────────────────────

        /// <summary>Returns the user's name formatted in title case using string manipulation.</summary>
        public string FormattedName =>
            string.IsNullOrWhiteSpace(Name)
                ? "User"
                : char.ToUpper(Name[0]) + Name.Substring(1).ToLower();

        /// <summary>Returns a greeting that changes based on the time of day.</summary>
        public string TimeGreeting
        {
            get
            {
                int hour = SessionStart.Hour;
                if (hour < 12) return "Good morning";
                if (hour < 17) return "Good afternoon";
                return "Good evening";
            }
        }

        /// <summary>Returns how long the current session has been running.</summary>
        public string SessionDuration
        {
            get
            {
                TimeSpan duration = DateTime.Now - SessionStart;
                if (duration.TotalMinutes < 1)
                    return "less than a minute";
                if (duration.TotalMinutes < 60)
                    return $"{(int)duration.TotalMinutes} minute(s)";
                return $"{(int)duration.TotalHours} hour(s)";
            }
        }

        // ── Constructor ───────────────────────────────────────────────────────
        public UserProfile(string name)
        {
            // Applies string manipulation: trim whitespace and title-case the name
            Name = name.Trim();
            SessionStart = DateTime.Now;
        }
    }
}
