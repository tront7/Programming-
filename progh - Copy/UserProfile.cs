using System;

namespace CybersecurityBot
{
    public class UserProfile
    {
        public string   Name         { get; set; } = string.Empty;
        public DateTime SessionStart { get; set; } = DateTime.Now;
        public int      MessageCount { get; set; } = 0;
        public string   LastTopic    { get; set; } = string.Empty;

        public string FormattedName =>
            string.IsNullOrWhiteSpace(Name)
                ? "User"
                : char.ToUpper(Name[0]) + Name.Substring(1).ToLower();

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

        public string SessionDuration
        {
            get
            {
                TimeSpan d = DateTime.Now - SessionStart;
                if (d.TotalMinutes < 1)  return "less than a minute";
                if (d.TotalMinutes < 60) return $"{(int)d.TotalMinutes} minute(s)";
                return $"{(int)d.TotalHours} hour(s)";
            }
        }

        public UserProfile(string name)
        {
            Name         = name.Trim();
            SessionStart = DateTime.Now;
        }
    }
}
