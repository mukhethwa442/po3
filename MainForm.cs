
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Media;
    using System.Windows.Forms;

    namespace CybersecurityAwarenessChatbot
    {
        public class MainForm : Form
        {
            private RichTextBox chatBox;
            private TextBox inputBox;
            private Button sendButton;

            private Dictionary<string, List<string>> responses;
            private Dictionary<string, string> memory;
            private Random random = new Random();

            private string currentTopic = "";

            public MainForm()
            {
                Text = "Cybersecurity Awareness Chatbot";
                Width = 800;
                Height = 600;

                chatBox = new RichTextBox()
                {
                    Dock = DockStyle.Top,
                    Height = 450,
                    ReadOnly = true,
                    Font = new Font("Consolas", 11)
                };

                inputBox = new TextBox()
                {
                    Dock = DockStyle.Bottom,
                    Height = 30
                };

                sendButton = new Button()
                {
                    Text = "Send",
                    Dock = DockStyle.Bottom,
                    Height = 40
                };

                sendButton.Click += SendButton_Click;

                Controls.Add(chatBox);
                Controls.Add(sendButton);
                Controls.Add(inputBox);

                InitializeResponses();
                memory = new Dictionary<string, string>();

                DisplayAsciiArt();
                VoiceGreeting();
                BotReply("Hello! I am your Cybersecurity Awareness Assistant.");
            }

            private void InitializeResponses()
            {
                responses = new Dictionary<string, List<string>>()
                {
                    {
                        "password",
                        new List<string>()
                        {
                            "Use strong and unique passwords for every account.",
                            "Avoid using personal details in your passwords.",
                            "Enable multi-factor authentication for added security."
                        }
                    },
                    {
                        "phishing",
                        new List<string>()
                        {
                            "Never click suspicious links in emails.",
                            "Scammers often pretend to be trusted organisations.",
                            "Always verify email addresses before responding."
                        }
                    },
                    {
                        "privacy",
                        new List<string>()
                        {
                            "Review your account privacy settings regularly.",
                            "Avoid sharing sensitive information online.",
                            "Use secure websites that start with HTTPS."
                        }
                    },
                    {
                        "scam",
                        new List<string>()
                        {
                            "Be careful of offers that seem too good to be true.",
                            "Never share banking information with strangers.",
                            "Research companies before making online payments."
                        }
                    }
                };
            }

            private void DisplayAsciiArt()
            {
                chatBox.AppendText(
@"  ____       _               _     
 / ___|  ___| |__   ___  ___| |__  
 \___ \ / __| '_ \ / _ \/ __| '_ \ 
  ___) | (__| | | |  __/ (__| | | |
 |____/ \___|_| |_|\___|\___|_| |_|

");
            }

            private void VoiceGreeting()
            {
                try
                {
                    SoundPlayer player = new SoundPlayer("greeting.wav");
                    player.Play();
                }
                catch
                {
                    BotReply("Voice greeting file not found.");
                }
            }

            private void SendButton_Click(object sender, EventArgs e)
            {
                string userInput = inputBox.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(userInput))
                    return;

                chatBox.AppendText("\nYou: " + userInput + "\n");

                HandleMemory(userInput);

                string sentiment = DetectSentiment(userInput);

                if (responses.Keys.Any(k => userInput.Contains(k)))
                {
                    foreach (var key in responses.Keys)
                    {
                        if (userInput.Contains(key))
                        {
                            currentTopic = key;

                            string response = responses[key][random.Next(responses[key].Count)];

                            if (sentiment == "worried")
                            {
                                response = "It's understandable to feel worried. " + response;
                            }
                            else if (sentiment == "frustrated")
                            {
                                response = "I understand your frustration. " + response;
                            }

                            BotReply(response);
                            break;
                        }
                    }
                }
                else if (userInput.Contains("another tip") || userInput.Contains("tell me more") || userInput.Contains("explain more"))
                {
                    if (currentTopic != "")
                    {
                        string response = responses[currentTopic][random.Next(responses[currentTopic].Count)];
                        BotReply(response);
                    }
                    else
                    {
                        BotReply("Please mention a cybersecurity topic first.");
                    }
                }
                else
                {
                    BotReply("I'm not sure I understand. Can you try rephrasing?");
                }

                inputBox.Clear();
            }

            private void HandleMemory(string input)
            {
                if (input.Contains("my name is"))
                {
                    string name = input.Replace("my name is", "").Trim();
                    memory["name"] = name;
                    BotReply("Nice to meet you, " + name + "!");
                }

                if (input.Contains("i'm interested in"))
                {
                    string topic = input.Replace("i'm interested in", "").Trim();
                    memory["interest"] = topic;
                    BotReply("Great! I'll remember that you're interested in " + topic + ".");
                }

                if (memory.ContainsKey("interest"))
                {
                    BotReply("As someone interested in " + memory["interest"] + ", staying informed is important.");
                }
            }

            private string DetectSentiment(string input)
            {
                if (input.Contains("worried"))
                    return "worried";

                if (input.Contains("frustrated"))
                    return "frustrated";

                if (input.Contains("curious"))
                    return "curious";

                return "neutral";
            }

            private void BotReply(string message)
            {
                chatBox.AppendText("Bot: " + message + "\n");
            }
        }
    }
