using System;
using System.Collections.Generic;
using System.IO;


public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

// PromptGenerator

public class PromptGenerator
{
    private List<Entry> entries = new List<Entry>();

    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What did I learn today that I didn't know before?",
        "What moment made me laugh or smile the most today?",
        "What challenges did I face today, and how did I overcome them?",
        "What is one thing I accomplished today that I’m proud of?",
        "What am I grateful for today, and why?"
    };

    public void AddEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        entries.Add(new Entry(prompt, response, date));
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries in the journal.");
            return;
        }

        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
            Console.WriteLine("------------------------");
        }
    }

   public void SaveToFile(string filename)
{
    using (StreamWriter writer = new StreamWriter(filename))
    {
        writer.WriteLine("Date,Prompt,Response"); // CSV header
        foreach (var entry in entries)
        {
            string prompt = entry.Prompt.Replace(",", ";").Replace("\"", "\"\"");
            string response = entry.Response.Replace(",", ";").Replace("\"", "\"\"");
            writer.WriteLine($"\"{entry.Date}\",\"{prompt}\",\"{response}\"");
        }
    }
    Console.WriteLine("Journal saved successfully.");
}

    public void LoadFromFile(string filename)
{
    if (!File.Exists(filename))
    {
        Console.WriteLine("File not found.");
        return;
    }

    entries.Clear();
    using (StreamReader reader = new StreamReader(filename))
    {
        reader.ReadLine(); // Skip header
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 3)
            {
                string date = parts[0].Trim('"');
                string prompt = parts[1].Trim('"');
                string response = parts[2].Trim('"');
                entries.Add(new Entry(prompt, response, date));
            }
        }
    }
    Console.WriteLine("Journal loaded successfully.");
}
}

// Program/menu


class Program
{
    static void Main(string[] args)
    {
        PromptGenerator journal = new PromptGenerator();
        string choice;

        do
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter the filename to save to: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter the filename to load from: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        } while (choice != "5");
    }
}
