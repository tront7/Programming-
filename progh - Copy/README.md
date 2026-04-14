# 🛡 Cybersecurity Awareness Chatbot (Liam)

## 📌 Overview

The **Cybersecurity Awareness Bot (Liam)** is a C# console-based application designed to educate users about cybersecurity best practices through an interactive chat interface.

The bot provides real-time responses on topics such as:

* Password security 🔐
* Phishing attacks 🎣
* Malware protection 🦠
* Safe browsing 🌐
* Data privacy 🔏
* Social engineering 🎭
* Two-factor authentication (2FA) 🔑

It also includes:

* A personalized user session
* ASCII UI styling
* Simulated typing effects
* Optional voice greeting

---

## 🧠 Features

### ✅ Interactive Chat System

* Continuous conversation loop
* Keyword-based response engine
* Smart input validation

### ✅ User Personalization

* Captures and formats user name
* Tracks:

  * Number of messages
  * Session duration
  * Last discussed topic

### ✅ UI/UX Enhancements

* ASCII art logo display
* Colored console output
* Typing animation effect

### ✅ Audio Support

* Plays a `greeting.wav` file at startup (if available)

### ✅ Error Handling

* Input validation with feedback
* Graceful handling of missing audio file

---

## 📁 Project Structure

```
CybersecurityBot/
│
├── Program.cs              # Entry point of the application
├── Chatbot.cs              # Core chatbot logic and conversation loop
├── UserProfile.cs          # Stores user session data (auto properties)
├── ResponseEngine.cs       # Keyword-response mapping logic
├── InputValidator.cs       # Input validation and name handling
├── UI.cs                   # Console UI styling and animations
├── VoiceGreeting.cs        # Audio playback functionality
│
├── CybersecurityBot.csproj # Project configuration file
│
├── greeting.wav            # (Optional) Voice greeting file
│
└── .github/
    └── workflows/
        └── dotnet.yml      # GitHub Actions CI pipeline
```

---

## ⚙️ Requirements

* .NET SDK **6.0 or higher** (Project targets .NET 9.0)
* Windows, Linux, or macOS
* IDE:

  * ✅ Visual Studio
  * ✅ Visual Studio Code

---

## ▶️ How to Run the Application

### 🔹 Using Visual Studio

1. Open **Visual Studio**
2. Click **Open a Project or Solution**
3. Select `CybersecurityBot.csproj`
4. Press **F5** or click **Start**
5. The chatbot will launch in the console

---

### 🔹 Using Visual Studio Code

1. Install:

   * .NET SDK
   * C# Extension (by Microsoft)

2. Open the project folder:

   ```bash
   code .
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

---

### 🔹 Using Terminal / Command Prompt

Navigate to the project directory:

```bash
cd CybersecurityBot
dotnet run
```

---

## 🔊 Audio Setup (Optional)

To enable the voice greeting:

1. Place a file named:

   ```
   greeting.wav
   ```
2. Inside the output directory:

   ```
   bin/Debug/net9.0/
   ```

   *(or Release folder if applicable)*

If the file is missing, the program will continue without audio.

---

## 🧩 How It Works

### 🔹 Program Flow

1. `Program.cs`

   * Entry point
   * Plays voice greeting
   * Displays ASCII logo
   * Starts chatbot

2. `Chatbot.cs`

   * Handles:

     * Greeting
     * User interaction loop
     * Exit conditions

3. `InputValidator.cs`

   * Ensures valid user input
   * Prompts for correct name format

4. `UserProfile.cs`

   * Stores session data using **automatic properties**
   * Provides computed values:

     * Formatted name
     * Time-based greeting
     * Session duration

5. `ResponseEngine.cs`

   * Uses a dictionary to match keywords with responses

6. `UI.cs`

   * Handles all console styling:

     * Colors
     * Typing animation
     * Dividers
     * ASCII logo

7. `VoiceGreeting.cs`

   * Plays `.wav` file using `System.Media.SoundPlayer`

---

## 🔁 Continuous Integration (CI)

This project includes a **GitHub Actions workflow**:

**File:** `.github/workflows/dotnet.yml`

### What it does:

* Restores dependencies
* Builds the project
* Treats warnings as errors

### Runs on:

* Push to `main` or `master`
* Pull requests

---

## 🧱 Technologies Used

* **Language:** C#
* **Framework:** .NET 9.0
* **Libraries:**

  * `System`
  * `System.Media`
  * `System.Threading`
  * `System.Collections.Generic`
  * `System.Windows.Extensions` (for audio support)

---

## 💡 Compatibility

This project is fully compatible with:

* ✅ **Visual Studio (recommended)**
* ✅ **Visual Studio Code**
* ✅ Command Line (.NET CLI)

---

## 🚪 Exit Commands

Users can exit the chatbot by typing:

```
exit
quit
bye
```

---

## 📌 Notes

* The chatbot uses **simple keyword matching**, not AI/NLP
* Designed for educational purposes
* Easily extendable with:

  * More responses
  * GUI (Windows Forms/WPF)
  * Database integration

---

## 🚀 Future Improvements

* Add AI/NLP support (e.g., OpenAI API)
* Store chat history
* GUI interface
* Multi-language support
* Web-based version

---

## 👨‍💻 Author

Nemukongwe Oripfa Clinton

Developed as a **Cybersecurity Awareness Project** to promote safe digital practices through interactive learning.
