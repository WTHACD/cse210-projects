using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
    class Activity
    {
        protected string name;
        protected string description;

        public Activity(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        
        public int DisplayStartMessage()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Welcome to the {name} Activity.");
            Console.WriteLine(description);
            int duration = 0;
            while (true)
            {
                Console.Write("Enter the duration of the activity in seconds: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out duration) && duration > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a positive number.");
                }
            }
            return duration;
        }

        
        public void DisplayPreparation()
        {
            Console.WriteLine("\nGet ready...");
            ShowCountdown(3);
        }

        
      public void ShowCountdown(int seconds)
{
    for (int i = seconds; i > 0; i--)
    {
        
        Console.Write($"\r{i}   "); 
        Thread.Sleep(1000);
    }
    Console.WriteLine();  
}


        
        public void Spinner(int seconds)
        {
            char[] spinnerChars = { '|', '/', '-', '\\' };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int counter = 0;
            while (DateTime.Now < endTime)
            {
                Console.Write(spinnerChars[counter % spinnerChars.Length]);
                Thread.Sleep(1);
                Console.Write("\b");  
                counter++;
            }
            Console.WriteLine();
        }

        
        public void DisplayEndMessage(int duration)
        {
            Console.WriteLine("\nWell done!");
            Spinner(3);
            Console.WriteLine($"You have completed the {name} Activity for {duration} seconds.");
            Spinner(3);
            Console.WriteLine(new string('=', 50));
            Console.WriteLine();
        }
    }

    
    class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public void Run()
        {
            int duration = DisplayStartMessage();
            DisplayPreparation();

            Console.WriteLine("Let's begin the breathing exercise.\n");
            DateTime startTime = DateTime.Now;
            
            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                Console.WriteLine("Breathe in...");
                int remaining = duration - (int)(DateTime.Now - startTime).TotalSeconds;
                int waitTime = Math.Min(4, remaining);
                if (waitTime > 0)
                {
                    ShowCountdown(waitTime);
                }
                if ((DateTime.Now - startTime).TotalSeconds >= duration)
                    break;

                Console.WriteLine("Breathe out...");
                remaining = duration - (int)(DateTime.Now - startTime).TotalSeconds;
                waitTime = Math.Min(4, remaining);
                if (waitTime > 0)
                {
                    ShowCountdown(waitTime);
                }
            }
            DisplayEndMessage(duration);
        }
    }


    class ReflectionActivity : Activity
    {
        public ReflectionActivity()
            : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        public void Run()
        {
            int duration = DisplayStartMessage();
            DisplayPreparation();

                List<string> prompts = new List<string>
            {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something truly selfless."
            };

            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine("When you have something in mind, press Enter to continue...");
            Console.ReadLine();

            List<string> questions = new List<string>
            {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            };

            Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience:");
            DateTime startTime = DateTime.Now;
            
            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                string question = questions[rand.Next(questions.Count)];
                Console.WriteLine($"> {question}");
                int timeLeft = duration - (int)(DateTime.Now - startTime).TotalSeconds;
                int pauseTime = Math.Min(5, timeLeft);
                if (pauseTime > 0)
                {
                    Spinner(pauseTime);
                }
            }
            DisplayEndMessage(duration);
        }
    }

       class ListingActivity : Activity
    {
        public ListingActivity()
            : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public void Run()
        {
            int duration = DisplayStartMessage();
            DisplayPreparation();

          
            List<string> prompts = new List<string>
            {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            };

            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine("You will have a few seconds to think about this prompt.");
            ShowCountdown(5);
            Console.WriteLine("Now, list as many items as you can. Press Enter after each one.\n");

            List<string> items = new List<string>();
            DateTime startTime = DateTime.Now;
            
            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                if ((DateTime.Now - startTime).TotalSeconds >= duration)
                {
                    break;
                }
                string response = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(response))
                {
                    items.Add(response.Trim());
                }
            }
            Console.WriteLine($"\nYou listed {items.Count} items!");
            DisplayEndMessage(duration);
        }
    }

        class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Mindfulness Program - Main Menu");
                Console.WriteLine("1. Start Breathing Activity");
                Console.WriteLine("2. Start Reflection Activity");
                Console.WriteLine("3. Start Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Select a choice from the menu (1-4): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Run();
                }
                else if (choice == "2")
                {
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Run();
                }
                else if (choice == "3")
                {
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Run();
                }
                else if (choice == "4")
                {
                    Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.\n");
                }
            }
        }
    }
}
