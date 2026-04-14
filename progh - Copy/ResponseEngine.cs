using System;                     // Provides basic system functionality
using System.Collections.Generic; // Allows use of Dictionary collection

namespace CybersecurityBot
{
    /// <summary>
    /// Handles generating responses based on user input.
    /// Uses a keyword-matching approach with a dictionary to simulate chatbot intelligence.
    /// Demonstrates use of collections (Dictionary) and string searching.
    /// </summary>
    public static class ResponseEngine
    {
        /// <summary>
        /// Stores keyword → response pairs.
        /// The dictionary is case-insensitive to improve user experience.
        /// </summary>
        private static readonly Dictionary<string, string> Responses =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // ── General Responses ─────────────────────────────────────────────
            { "how are you", "I'm fully operational and vigilant — always watching out for cyber threats! How can I help you stay safe today?" },
            { "what is your purpose", "My purpose is to educate you about cybersecurity threats and best practices so you can stay safe online." },
            { "what can i ask you about", "You can ask me about:\n  • Password safety\n  • Phishing attacks\n  • Safe browsing\n  • Malware & viruses\n  • Data privacy\n  • Social engineering\n  • Two-factor authentication" },
            { "hello", "Hello! Great to see you here. Ask me anything about staying safe online." },
            { "help", "Sure! Try asking about: passwords, phishing, safe browsing, malware, privacy, or 2FA." },

            // ── Password Safety ───────────────────────────────────────────────
            { "password", "🔐 PASSWORD SAFETY TIPS:\n  • Use at least 12 characters — mix letters, numbers & symbols.\n  • Never reuse the same password across sites.\n  • Use a trusted password manager (e.g., Bitwarden or 1Password).\n  • Avoid obvious choices like 'password123' or your name.\n  • Change passwords immediately if a breach is suspected." },

            // ── Phishing Awareness ────────────────────────────────────────────
            { "phishing", "🎣 PHISHING AWARENESS:\n  • Phishing emails pretend to be from trusted sources to steal your info.\n  • Look for misspelled domains (e.g., 'amaz0n.com').\n  • Never click suspicious links — hover over them to check the URL first.\n  • Legitimate organisations never ask for your password via email.\n  • When in doubt, contact the sender directly through official channels." },

            // ── Safe Browsing ─────────────────────────────────────────────────
            { "browsing", "🌐 SAFE BROWSING TIPS:\n  • Always check for HTTPS (padlock icon) before entering personal info.\n  • Use a reputable browser with security extensions (e.g., uBlock Origin).\n  • Avoid downloading files from untrusted websites.\n  • Keep your browser and plugins up to date.\n  • Use a VPN on public Wi-Fi networks." },
            { "safe browsing", "🌐 SAFE BROWSING TIPS:\n  • Always check for HTTPS (padlock icon) before entering personal info.\n  • Use a reputable browser with security extensions (e.g., uBlock Origin).\n  • Avoid downloading files from untrusted websites.\n  • Keep your browser and plugins up to date.\n  • Use a VPN on public Wi-Fi networks." },

            // ── Malware & Virus Protection ────────────────────────────────────
            { "malware", "🦠 MALWARE PROTECTION:\n  • Install reputable antivirus software and keep it updated.\n  • Don't open email attachments from unknown senders.\n  • Avoid pirated software — it often contains hidden malware.\n  • Regularly back up your data to an external drive or cloud.\n  • Be cautious of USB drives from unknown sources." },
            { "virus", "🦠 VIRUS PROTECTION:\n  • Keep your operating system and software up to date.\n  • Use antivirus software and run regular scans.\n  • Don't click on pop-up ads or suspicious download buttons.\n  • Scan external drives before opening files from them." },

            // ── Privacy ───────────────────────────────────────────────────────
            { "privacy", "🔏 DATA PRIVACY TIPS:\n  • Read app permissions before installing — does a torch app really need your contacts?\n  • Limit personal info shared on social media.\n  • Regularly review and revoke app access to your accounts.\n  • Use privacy-focused search engines like DuckDuckGo.\n  • Enable full-disk encryption on your devices." },

            // ── Social Engineering ────────────────────────────────────────────
            { "social engineering", "🎭 SOCIAL ENGINEERING:\n  • Attackers manipulate people psychologically to reveal confidential info.\n  • Be sceptical of urgent, unexpected requests — even from 'known' contacts.\n  • Verify identities before sharing sensitive information.\n  • Common tactics include pretexting, baiting, and tailgating.\n  • Awareness training is your best defence." },

            // ── Two-Factor Authentication (2FA) ───────────────────────────────
            { "two-factor", "🔑 TWO-FACTOR AUTHENTICATION (2FA):\n  • 2FA adds a second verification step beyond your password.\n  • Use an authenticator app (e.g., Google Authenticator) rather than SMS where possible.\n  • Enable 2FA on email, banking, and social media accounts.\n  • Keep backup codes in a safe, offline location." },
            { "2fa", "🔑 TWO-FACTOR AUTHENTICATION (2FA):\n  • 2FA adds a second verification step beyond your password.\n  • Use an authenticator app (e.g., Google Authenticator) rather than SMS where possible.\n  • Enable 2FA on email, banking, and social media accounts.\n  • Keep backup codes in a safe, offline location." },
        };

        /// <summary>
        /// Searches for a matching response based on user input.
        /// Uses partial matching (keyword contained in input).
        /// </summary>
        /// <param name="input">The user's message</param>
        /// <returns>A matching response, or null if no match is found</returns>
        public static string? GetResponse(string input)
        {
            // Check if input is empty or invalid
            if (string.IsNullOrWhiteSpace(input))
                return null;

            // Loop through all keyword-response pairs
            foreach (var entry in Responses)
            {
                // Check if the input contains the keyword (case-insensitive)
                if (input.IndexOf(entry.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return entry.Value; // Return the matching response
            }

            // Return null if no keyword match is found
            return null;
        }
    }
}