using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        bool running = true;
        List<string> sessionMessages = new List<string>();
        string file = "encryptions.txt";

        while (running)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======= Message Encryptor =======");
            Console.WriteLine("1. Encrypt...");
            Console.WriteLine("2. Decrypt...");
            Console.WriteLine("0. Exit...\n");
            Console.ResetColor();

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("======= Encryption =======");
                Console.Write("Enter text to encrypt: ");
                string rawText = Console.ReadLine();

                string encrypted = Encrypt(rawText);
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                string paragraph = $"[{timestamp}]\n{encrypted}\n";

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nEncrypted:");
                Console.WriteLine(paragraph);
                Console.ResetColor();

                sessionMessages.Add(paragraph);

                File.AppendAllText(file, paragraph + Environment.NewLine);
            }
            else if (choice == "2")
            {
                Console.WriteLine("======= Decryption =======\n");

                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(line);
                            Console.ResetColor();
                        }
                        else if (!string.IsNullOrWhiteSpace(line))
                        {
                            string decrypted = Decrypt(line);
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("→ " + decrypted + "\n");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No messages found.");
                }
            }
            else if (choice == "0")
            {
                Console.WriteLine("Goodbye");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.\n");
            }
        }

        Console.WriteLine("======= All Encryptions for this Session =======");

        foreach (string encrypted in sessionMessages)
        {
            Console.WriteLine(encrypted);
        }
    }

    public static string Encrypt(string text)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            sb.Append((char)(c + 3));
        }
        return sb.ToString();
    }

    public static string Decrypt(string text)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            sb.Append((char)(c - 3));
        }
        return sb.ToString();
    }
}
