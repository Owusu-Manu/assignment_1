using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<string> _entries;

    public Journal()
    {
        _entries = new List<string>();
    }

    public void DisplayJournal()
    {
        Console.WriteLine("Journal Entries:");
        foreach (string entry in _entries)
        {
            Console.WriteLine(entry);
            Console.WriteLine();
        }
    }

    public void SaveJournal()
    {
        Console.WriteLine("Saving journal to file...");
        File.WriteAllLines("journal.txt", _entries);
        Console.WriteLine("Journal saved successfully!");
        Console.WriteLine();
    }

    public void LoadJournal()
    {
        if (File.Exists("journal.txt"))
        {
            Console.WriteLine("Loading journal from file...");
            _entries = new List<string>(File.ReadAllLines("journal.txt"));
            Console.WriteLine("Journal loaded successfully!");
        }
        else
        {
            Console.WriteLine("No journal file found. Creating a new journal.");
            _entries.Clear();
        }
        Console.WriteLine();
    }
}

class Entry
{
    private List<string> _prompts;
    private Random _randomGenerator;
    private DateTime _date;

    public Entry()
    {
        _prompts = new List<string>
        {
            "Write about a memorable experience.",
            "What are you grateful for today?",
            "Describe your goals for the future.",
            "Reflect on a challenge you overcame.",
            "Write about a person who inspires you."
        };

        _randomGenerator = new Random();
        _date = DateTime.Now;
    }

    public string WriteEntry()
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine("Prompt: " + prompt);
        Console.WriteLine("Date: " + _date.ToString("MM/dd/yyyy"));

        Console.WriteLine("Enter your journal entry:");
        string input = Console.ReadLine();

        string entry = $"Entry Date: {_date.ToString("MM/dd/yyyy")}\nPrompt: {prompt}\n{input}";
        return entry;
    }

    private string GetRandomPrompt()
    {
        int index = _randomGenerator.Next(_prompts.Count);
        return _prompts[index];
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Entry entry = new Entry();

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Write a journal entry");
            Console.WriteLine("2. Display all journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int option))
            {
                switch (option)
                {
                    case 1:
                        string journalEntry = entry.WriteEntry();
                        journal.SaveJournal();
                        journal.LoadJournal();
                        _ = journalEntry != null ? journal.DisplayJournal() : Console.WriteLine("Invalid journal entry.");
                        break;
                    case 2:
                        journal.DisplayJournal();
                        break;
                    case 3:
                        journal.SaveJournal();
                        break;
                    case 4:
                        journal.LoadJournal();
                        break;
                    case 5:
                        Console.WriteLine("Exiting the program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.WriteLine();
            }
        }
    }
}
