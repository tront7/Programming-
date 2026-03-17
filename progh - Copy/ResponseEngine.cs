using System;
using System.Collections.Generic;

namespace CybersecurityBot
{
    public static class ResponseEngine
    {
        // keyword → response pairs
        private static readonly Dictionary<string, string> Responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // General
            { "how are you",        "I'm fully operational and vigilant — always watching out for cyber threats! How can I help you stay safe today?" },
            { "what is your purpose","My purpose is to educate you about cybersecurity threats and best practices so you can stay safe online." },
            { "what can i ask you about", "You can ask me about:\n  • Password safety\n  • Phishing attacks\n  • Safe browsing\n  • Malware & viruses\n  • Data privacy\n  • Social engineering\n  • Two-factor authentication" },
            { "hello",              "Hello! Great to see you here. Ask me anything about staying safe online." },
            { "help",               "Sure! Try asking about: passwords, phishing, safe browsing, malware, privacy, or 2FA." },

            // Password safety
            { "password",           "🔐 PASSWORD SAFETY TIPS:\n  • Use at least 12 characters — mix letters, numbers & symbols.\n  • Never reuse the same password across sites.\n  • Use a trusted password manager (e.g., Bitwarden or 1Password).\n  • Avoid obvious choices like 'password123' or your name.\n  • Change passwords immediately if a breach is suspected." },

            // Phishing
            { "phishing",           "🎣 PHISHING AWARENESS:\n  • Phishing emails pretend to be from trusted sources to steal your info.\n  • Look for misspelled domains (e.g., 'amaz0n.com').\n  • Never click suspicious links — hover over them to check the URL first.\n  • Legitimate organisations never ask for your password via email.\n  • When in doubt, contact the sender directly through official channels." },

            // Safe browsing
            { "browsing",           "🌐 SAFE BROWSING TIPS:\n  • Always check for HTTPS (padlock icon) before entering personal info.\n  • Use a reputable browser with security extensions (e.g., uBlock Origin).\n  • Avoid downloading files from untrusted websites.\n  • Keep your browser and plugins up to date.\n  • Use a VPN on public Wi-Fi networks." },
            { "safe browsing",      "🌐 SAFE BROWSING TIPS:\n  • Always check for HTTPS (padlock icon) before entering personal info.\n  • Use a reputable browser with security extensions (e.g., uBlock Origin).\n  • Avoid downloading files from untrusted websites.\n  • Keep your browser and plugins up to date.\n  • Use a VPN on public Wi-Fi networks." },

            // Malware
            { "malware",            "🦠 MALWARE PROTECTION:\n  • Install reputable antivirus software and keep it updated.\n  • Don't open email attachments from unknown senders.\n  • Avoid pirated software — it often contains hidden malware.\n  • Regularly back up your data to an external drive or cloud.\n  • Be cautious of USB drives from unknown sources." },
            { "virus",              "🦠 VIRUS PROTECTION:\n  • Keep your operating system and software up to date.\n  • Use antivirus software and run regular scans.\n  • Don't click on pop-up ads or suspicious download buttons.\n  • Scan external drives before opening files from them." },

            // Privacy
            { "privacy",            "🔏 DATA PRIVACY TIPS:\n  • Read app permissions before installing — does a torch app really need your contacts?\n  • Limit personal info shared on social media.\n  • Regularly review and revoke app access to your accounts.\n  • Use privacy-focused search engines like DuckDuckGo.\n  • Enable full-disk encryption on your devices." },

            // Social engineering
            { "social engineering", "🎭 SOCIAL ENGINEERING:\n  • Attackers manipulate people psychologically to reveal confidential info.\n  • Be sceptical of urgent, unexpected requests — even from 'known' contacts.\n  • Verify identities before sharing sensitive information.\n  • Common tactics include pretexting, baiting, and tailgating.\n  • Awareness training is your best defence." },

            // 2FA
            { "two-factor",         "🔑 TWO-FACTOR AUTHENTICATION (2FA):\n  • 2FA adds a second verification step beyond your password.\n  • Use an authenticator app (e.g., Google Authenticator) rather than SMS where possible.\n  • Enable 2FA on email, banking, and social media accounts.\n  • Keep backup codes in a safe, offline location." },
            { "2fa",                "🔑 TWO-FACTOR AUTHENTICATION (2FA):\n  • 2FA adds a second verification step beyond your password.\n  • Use an authenticator app (e.g., Google Authenticator) rather than SMS where possible.\n  • Enable 2FA on email, banking, and social media accounts.\n  • Keep backup codes in a safe, offline location." },
        };

        public static string? GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            foreach (var entry in Responses)
            {
                if (input.IndexOf(entry.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return entry.Value;
            }

            return null;
        }
    }
}