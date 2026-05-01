using System;
using System.Collections.Generic;

namespace CybersecurityBot
{
    /// <summary>
    /// Keyword-based response engine with random response variation per topic.
    /// Uses delegates for response selection to satisfy the delegate requirement.
    /// </summary>
    public static class ResponseEngine
    {
        private static readonly Random _rng = new();

        // Delegate for selecting a response from a list
        public delegate string ResponseSelector(List<string> responses);

        // Default selector — picks randomly
        public static ResponseSelector SelectResponse = (list) =>
            list[_rng.Next(list.Count)];

        // ── Keyword → multiple responses ──────────────────────────────────────
        private static readonly Dictionary<string, List<string>> Responses =
            new(StringComparer.OrdinalIgnoreCase)
        {
            // ── General ───────────────────────────────────────────────────────
            { "how are you", new() {
                "I'm fully operational and vigilant — always watching out for cyber threats! How can I help you today?",
                "Running at 100%! Ready to help you stay safe online. What would you like to know?",
                "All systems go! I'm here and ready to boost your cybersecurity knowledge." }},

            { "what is your purpose", new() {
                "My purpose is to educate you about cybersecurity threats and best practices so you can stay safe online.",
                "I'm here to be your personal cybersecurity guide — helping you understand threats and how to avoid them.",
                "I exist to make cybersecurity simple and accessible. Ask me anything!" }},

            { "hello", new() {
                "Hello! Great to see you here. Ask me anything about staying safe online.",
                "Hey there! Ready to learn about cybersecurity? Ask me anything!",
                "Hi! I'm your Cybersecurity Awareness Bot. What would you like to know today?" }},

            { "hi", new() {
                "Hi there! What cybersecurity topic can I help you with today?",
                "Hello! Feel free to ask me anything about online safety.",
                "Hey! Great to have you here. What would you like to learn about?" }},

            { "help", new() {
                "Sure! Try asking about: passwords, phishing, malware, safe browsing, privacy, 2FA, VPNs, ransomware, or social engineering.",
                "I can help with passwords, phishing, malware, VPNs, ransomware, privacy, 2FA, safe browsing, and social engineering. What interests you?",
                "Here are some topics: passwords, phishing, malware, safe browsing, privacy, 2FA, VPN, encryption, data breaches. Just ask!" }},

            // ── Passwords ─────────────────────────────────────────────────────
            { "password", new() {
                "🔐 PASSWORD SAFETY:\n  • Use at least 12 characters with letters, numbers & symbols.\n  • Never reuse passwords across different sites.\n  • Use a password manager like Bitwarden or 1Password.\n  • Avoid birthdays, names, or simple words.\n  • Change passwords immediately if a breach is suspected.",
                "🔐 STRONG PASSWORDS:\n  • A passphrase like 'Coffee!Monkey$River7' is strong and memorable.\n  • Enable a password manager — never rely on memory alone.\n  • Different password for every account — no exceptions.\n  • Never share your password with anyone, even IT support.",
                "🔐 PASSWORD TIPS:\n  • Length beats complexity — 16 random characters is ideal.\n  • Use multi-factor authentication alongside strong passwords.\n  • Check haveibeenpwned.com to see if your passwords were leaked.\n  • Update your most important passwords every 6 months." }},

            // ── Phishing ──────────────────────────────────────────────────────
            { "phishing", new() {
                "🎣 PHISHING AWARENESS:\n  • Phishing emails impersonate trusted sources to steal your info.\n  • Check for misspelled domains (e.g. 'amaz0n.com').\n  • Hover over links before clicking — check the actual URL.\n  • Legitimate companies never ask for passwords via email.\n  • When in doubt, contact the sender through official channels.",
                "🎣 SPOT A PHISHING ATTACK:\n  • Be suspicious of urgent language like 'Act now or lose access'.\n  • Check the sender's email address carefully — scammers spoof real addresses.\n  • Never enter login details after clicking an email link — go directly to the site.\n  • Phishing also happens via SMS (smishing) and phone calls (vishing).",
                "🎣 PHISHING PROTECTION:\n  • Enable spam filters on your email.\n  • Use a browser with phishing protection (Chrome, Firefox, Edge all have it).\n  • Report phishing emails to your provider — it helps protect others.\n  • Train yourself: practice at phishingquiz.withgoogle.com." }},

            { "scam", new() {
                "🎣 SCAM AWARENESS:\n  • If it sounds too good to be true, it probably is.\n  • Never send money or gift cards to someone you've only met online.\n  • Romance scams, tech support scams, and lottery scams are all common.\n  • Verify any urgent requests by calling the organisation directly.",
                "🎣 AVOIDING SCAMS:\n  • Scammers create urgency — slow down and verify before acting.\n  • Never give remote access to your computer to unsolicited callers.\n  • Check reviews and verify websites before making purchases.\n  • Use credit cards (not debit) for online purchases for extra fraud protection." }},

            // ── Safe browsing ─────────────────────────────────────────────────
            { "browsing", new() {
                "🌐 SAFE BROWSING:\n  • Always look for HTTPS (padlock icon) before entering personal info.\n  • Use browser extensions like uBlock Origin to block malicious ads.\n  • Avoid downloading files from unknown websites.\n  • Keep your browser and extensions updated.\n  • Use a VPN on public Wi-Fi.",
                "🌐 BROWSE SAFELY:\n  • Use private/incognito mode when using shared computers.\n  • Clear your cookies and browsing history regularly.\n  • Be careful of pop-ups saying your device is infected — these are scams.\n  • Stick to reputable websites and verify URLs before clicking.",
                "🌐 BROWSER SECURITY:\n  • Enable two-factor authentication on your Google or browser account.\n  • Disable unnecessary browser extensions — they can track you.\n  • Use a privacy-focused browser like Firefox or Brave.\n  • Consider a DNS-over-HTTPS provider for extra security." }},

            { "internet safety", new() {
                "🌐 INTERNET SAFETY:\n  • Think before you click — most attacks start with a single click.\n  • Use strong, unique passwords on every account.\n  • Enable 2FA wherever possible.\n  • Keep all software and apps updated.\n  • Back up your data regularly.",
                "🌐 STAYING SAFE ONLINE:\n  • Be mindful of what personal information you share publicly.\n  • Use secure, encrypted messaging apps like Signal.\n  • Avoid public Wi-Fi for sensitive transactions.\n  • Review your social media privacy settings regularly." }},

            // ── Malware ───────────────────────────────────────────────────────
            { "malware", new() {
                "🦠 MALWARE PROTECTION:\n  • Install reputable antivirus software and keep it updated.\n  • Never open attachments from unknown senders.\n  • Avoid pirated software — it's a common malware source.\n  • Regularly back up your data to an external drive or cloud.\n  • Be cautious of USB drives from unknown sources.",
                "🦠 AVOID MALWARE:\n  • Download apps only from official stores (Google Play, App Store).\n  • Keep your operating system updated — patches fix vulnerabilities.\n  • Run regular antivirus scans even if you think you're safe.\n  • Malware can arrive via ads, email attachments, and compromised websites.",
                "🦠 MALWARE FACTS:\n  • Ransomware, spyware, trojans, and worms are all types of malware.\n  • Signs of infection: slow device, strange pop-ups, new programs you didn't install.\n  • If infected: disconnect from internet, run antivirus, and seek expert help.\n  • Prevention is always better than cure — stay updated and stay alert." }},

            { "virus", new() {
                "🦠 VIRUS PROTECTION:\n  • Keep your OS and all software up to date — updates patch vulnerabilities.\n  • Use reputable antivirus and run regular scans.\n  • Don't click on pop-up ads or suspicious download buttons.\n  • Scan external drives before opening files from them.",
                "🦠 ANTIVIRUS TIPS:\n  • Free antivirus options include Avast, AVG, and Windows Defender.\n  • Paid options like Malwarebytes offer more thorough protection.\n  • No antivirus catches everything — safe habits matter just as much.\n  • Keep your antivirus definitions updated for the latest threat detection." }},

            { "antivirus", new() {
                "🦠 ANTIVIRUS GUIDANCE:\n  • Windows Defender (built-in) is good for basic protection.\n  • For more coverage, consider Malwarebytes, Bitdefender, or ESET.\n  • Keep definitions updated — new threats appear daily.\n  • Antivirus is a safety net, not a replacement for safe browsing habits." }},

            // ── Privacy ───────────────────────────────────────────────────────
            { "privacy", new() {
                "🔏 DATA PRIVACY:\n  • Read app permissions before installing.\n  • Limit personal info shared on social media.\n  • Review and revoke app access to your accounts regularly.\n  • Use privacy-focused search engines like DuckDuckGo.\n  • Enable full-disk encryption on your devices.",
                "🔏 PROTECT YOUR PRIVACY:\n  • Use a VPN to hide your browsing from ISPs and hackers.\n  • Disable location tracking on apps that don't need it.\n  • Use a unique email alias for each service to reduce spam and tracking.\n  • Check your social media privacy settings — make sure only friends can see your posts.",
                "🔏 PRIVACY TIPS:\n  • Your data is valuable — treat it like cash.\n  • Use Signal or WhatsApp for encrypted messaging.\n  • Cover your webcam when not in use.\n  • Regularly google yourself to see what's publicly visible about you." }},

            // ── Social engineering ────────────────────────────────────────────
            { "social engineering", new() {
                "🎭 SOCIAL ENGINEERING:\n  • Attackers manipulate people psychologically to extract information.\n  • Be sceptical of urgent, unexpected requests — even from known contacts.\n  • Verify identities before sharing sensitive info.\n  • Common tactics: pretexting, baiting, tailgating, vishing.\n  • Awareness is your best defence.",
                "🎭 MANIPULATION TACTICS:\n  • Pretexting: attacker creates a fake scenario to extract info.\n  • Baiting: leaving infected USB drives in public places.\n  • Vishing: phone calls impersonating banks or IT support.\n  • Tailgating: following someone into a secure area.\n  • Always verify before you trust." }},

            // ── 2FA ───────────────────────────────────────────────────────────
            { "two-factor", new() {
                "🔑 TWO-FACTOR AUTHENTICATION (2FA):\n  • 2FA adds a second verification step beyond your password.\n  • Use an authenticator app (Aegis, Google Authenticator) over SMS.\n  • Enable 2FA on email, banking, and all social media accounts.\n  • Keep backup codes stored safely offline.",
                "🔑 WHY USE 2FA:\n  • Even if your password is stolen, 2FA stops attackers from logging in.\n  • Hardware keys like YubiKey offer the strongest 2FA protection.\n  • SMS-based 2FA is better than nothing, but can be intercepted.\n  • Set up 2FA today — it only takes 2 minutes per account." }},

            { "2fa", new() {
                "🔑 2FA SETUP:\n  • Download an authenticator app like Aegis (Android) or Raivo (iOS).\n  • Go to your account's security settings and enable 2FA.\n  • Scan the QR code with your app.\n  • Store the backup codes somewhere safe and offline.",
                "🔑 2FA FACTS:\n  • 2FA blocks 99.9% of automated account attacks (Microsoft data).\n  • SMS codes are convenient but can be intercepted via SIM swapping.\n  • Authenticator apps generate codes offline — more secure than SMS.\n  • Use 2FA on every important account — email, banking, work accounts first." }},

            { "authentication", new() {
                "🔑 AUTHENTICATION TIPS:\n  • Always prefer authenticator apps over SMS for 2FA.\n  • Biometrics (fingerprint/face) are secure and convenient — use them.\n  • Review active sessions in account settings and remove unknown devices.\n  • Never approve a 2FA request you didn't initiate." }},

            // ── VPN ───────────────────────────────────────────────────────────
            { "vpn", new() {
                "🔒 VPN BASICS:\n  • A VPN encrypts your internet traffic, hiding it from snoopers.\n  • Essential on public Wi-Fi (cafes, airports, hotels).\n  • Choose a reputable paid VPN — Mullvad, ProtonVPN, or NordVPN.\n  • Free VPNs often log and sell your data — avoid them.",
                "🔒 USING A VPN:\n  • A VPN hides your IP address but doesn't make you fully anonymous.\n  • Enable the kill switch feature so your traffic stops if the VPN drops.\n  • Use split tunneling to only route sensitive traffic through the VPN.\n  • Keep your VPN app updated for the latest security patches." }},

            // ── Ransomware ────────────────────────────────────────────────────
            { "ransomware", new() {
                "💰 RANSOMWARE PROTECTION:\n  • Ransomware encrypts your files and demands payment for the key.\n  • Back up using the 3-2-1 rule: 3 copies, 2 media types, 1 offsite.\n  • Never open unexpected email attachments.\n  • Keep all software patched — ransomware exploits outdated systems.\n  • If infected: disconnect from internet immediately and seek expert help.",
                "💰 RANSOMWARE FACTS:\n  • Paying the ransom doesn't guarantee file recovery.\n  • Ransomware attacks cost businesses billions annually.\n  • Regular offline backups are your best defence.\n  • Disable Remote Desktop Protocol (RDP) if you don't need it." }},

            // ── Wi-Fi ─────────────────────────────────────────────────────────
            { "wifi", new() {
                "📡 WI-FI SECURITY:\n  • Change your router's default username and password immediately.\n  • Use WPA3 or WPA2 encryption on your home network.\n  • Create a guest network for IoT devices and visitors.\n  • Regularly check for router firmware updates.",
                "📡 PUBLIC WI-FI RISKS:\n  • Public Wi-Fi is unencrypted — attackers can intercept traffic.\n  • Always use a VPN on public networks.\n  • Avoid accessing banking or sensitive accounts on public Wi-Fi.\n  • Watch out for 'evil twin' hotspots mimicking legitimate networks." }},

            { "wi-fi", new() {
                "📡 WI-FI SAFETY:\n  • Never auto-connect to open networks — disable auto-join.\n  • Use your phone's hotspot instead of public Wi-Fi for sensitive tasks.\n  • Forget public networks after use to prevent automatic reconnection.",
                "📡 HOME NETWORK TIPS:\n  • Position your router centrally — don't broadcast signal outside your home.\n  • Disable WPS — it has known vulnerabilities.\n  • Consider a network monitoring tool to see all connected devices." }},

            { "public wifi", new() {
                "📡 PUBLIC WI-FI:\n  • Treat all public Wi-Fi as compromised — always use a VPN.\n  • Never do banking or enter passwords on public networks.\n  • Use your mobile data instead when possible.",
                "📡 PUBLIC NETWORK DANGERS:\n  • Man-in-the-middle attacks are common on public Wi-Fi.\n  • Even HTTPS can be stripped by sophisticated attackers on open networks.\n  • Confirm the network name with staff before connecting — fake hotspots are common." }},

            // ── Encryption ────────────────────────────────────────────────────
            { "encrypt", new() {
                "🔓 ENCRYPTION BASICS:\n  • Encryption scrambles data so only authorised parties can read it.\n  • Enable full-disk encryption: BitLocker (Windows) or FileVault (Mac).\n  • Use end-to-end encrypted apps like Signal for sensitive chats.\n  • Always look for HTTPS when submitting any personal data online.",
                "🔓 WHY ENCRYPTION MATTERS:\n  • If your laptop is stolen, encryption keeps your data safe.\n  • Encrypted messaging means even the app provider can't read your messages.\n  • Encryption is the foundation of all secure online communication." }},

            // ── Data breach ───────────────────────────────────────────────────
            { "data breach", new() {
                "🚨 DATA BREACH RESPONSE:\n  • Check haveibeenpwned.com to see if your email was exposed.\n  • Change passwords for affected accounts immediately.\n  • Enable 2FA on all accounts as an extra layer.\n  • Watch for suspicious activity on your bank accounts.\n  • Be extra vigilant for phishing emails after a breach.",
                "🚨 BREACH PREVENTION:\n  • Use unique passwords for every site — a breach on one won't affect others.\n  • Enable breach alert notifications in your password manager.\n  • Consider a credit freeze if financial data was exposed." }},

            { "breach", new() {
                "🚨 IF YOU'VE BEEN BREACHED:\n  • Act fast — change passwords and enable 2FA immediately.\n  • Check haveibeenpwned.com to understand the scope.\n  • Notify your bank if financial data was involved.\n  • Monitor your accounts for the next few months for suspicious activity.",
                "🚨 DATA BREACH FACTS:\n  • The average breach takes 207 days to be identified.\n  • Most breaches involve weak or stolen passwords.\n  • Regular password changes and unique passwords per site greatly reduce your risk." }},

            { "hack", new() {
                "🔴 IF YOU THINK YOU'VE BEEN HACKED:\n  • Change all important passwords immediately from a safe device.\n  • Enable 2FA on every account.\n  • Check for unrecognised devices or logins in your account settings.\n  • Contact your bank if financial accounts may be affected.\n  • Run a full antivirus scan.",
                "🔴 PREVENTING HACKING:\n  • Hackers often use stolen credentials from previous breaches.\n  • Use a password manager so every account has a unique password.\n  • Keep software updated — most hacks exploit known, patchable vulnerabilities.\n  • Be suspicious of unsolicited contacts asking for information or access." }},

            // ── Updates ───────────────────────────────────────────────────────
            { "update", new() {
                "🔄 SOFTWARE UPDATES:\n  • Updates patch security vulnerabilities — apply them promptly.\n  • Enable automatic updates for your OS and applications.\n  • Apply critical patches immediately — don't delay for convenience.\n  • Update router firmware, smart TVs, and IoT devices too.",
                "🔄 WHY UPDATES MATTER:\n  • Most successful attacks exploit unpatched, known vulnerabilities.\n  • WannaCry ransomware infected 200,000 computers in 150 countries — all via an unpatched Windows flaw.\n  • Enabling auto-updates is one of the easiest security wins available." }},

            // ── Firewall ──────────────────────────────────────────────────────
            { "firewall", new() {
                "🛡 FIREWALL BASICS:\n  • A firewall monitors and controls incoming/outgoing network traffic.\n  • Windows and macOS both have built-in firewalls — make sure they're enabled.\n  • Consider a hardware firewall (router-level) for home network protection.\n  • Firewalls are your first line of defence against network-based attacks.",
                "🛡 FIREWALL TIPS:\n  • Don't disable your firewall for any application unless absolutely necessary.\n  • Review firewall rules periodically to remove outdated exceptions.\n  • Business networks should have dedicated next-generation firewalls (NGFW)." }},

            // ── Identity theft ────────────────────────────────────────────────
            { "identity theft", new() {
                "🪪 IDENTITY THEFT PROTECTION:\n  • Never share your ID number, bank details, or passwords online.\n  • Shred documents containing personal info before discarding.\n  • Place a fraud alert with credit bureaus if you suspect theft.\n  • Monitor your credit report regularly for unusual activity.",
                "🪪 IF YOUR IDENTITY IS STOLEN:\n  • Report it to your bank and credit bureaus immediately.\n  • File a police report — you'll need it for insurance and disputes.\n  • Place a credit freeze to prevent new accounts being opened in your name.\n  • Change passwords and enable 2FA on all accounts." }},

            // ── Spam ──────────────────────────────────────────────────────────
            { "spam", new() {
                "📧 DEALING WITH SPAM:\n  • Never click links or open attachments in spam emails.\n  • Mark spam as junk — it helps train your email filter.\n  • Never unsubscribe from obvious spam — it confirms your email is active.\n  • Use a separate email address for online sign-ups.",
                "📧 REDUCING SPAM:\n  • Use email aliases (SimpleLogin, AnonAddy) for sign-ups.\n  • Don't publicly post your email address.\n  • Enable your email provider's spam filter.\n  • Report phishing spam to Action Fraud or your local cyber authority." }},
        };

        // ── Topics list for display ───────────────────────────────────────────
        public static readonly string[] TopicList =
        {
            "🔐 passwords", "🎣 phishing", "🌐 safe browsing", "🦠 malware",
            "🔏 privacy",   "🎭 social engineering", "🔑 2FA",  "🔒 VPN",
            "💰 ransomware","📡 WiFi security", "🔓 encryption", "🚨 data breach",
            "🔴 hacking",   "🔄 software updates", "🛡 firewall", "🪪 identity theft",
            "📧 spam",      "🎣 scams"
        };

        // ── Public API ────────────────────────────────────────────────────────
        public static string? GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            foreach (var entry in Responses)
            {
                if (input.IndexOf(entry.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return SelectResponse(entry.Value);
            }
            return null;
        }

        public static string? GetLastTopicKey(string input)
        {
            foreach (var key in Responses.Keys)
                if (input.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return key;
            return null;
        }
    }
}