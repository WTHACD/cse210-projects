using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    #region Clases de Metas

    public abstract class Goal
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public bool IsComplete { get; protected set; }

        public abstract int RecordEvent();

        public abstract string DisplayStatus();

        public void LoadState(bool complete)
        {
            IsComplete = complete;
        }
    }

    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points)
        {
            Name = name;
            Points = points;
            IsComplete = false;
        }

        public override int RecordEvent()
        {
            if (!IsComplete)
            {
                IsComplete = true;
                Console.WriteLine($"Completed: {Name}, Points Earned: {Points}");
                return Points;
            }
            else
            {
                Console.WriteLine($"The goal '{Name}' is already completed.");
                return 0;
            }
        }

        public override string DisplayStatus()
        {
            return IsComplete ? $"[X] {Name}" : $"[ ] {Name}";
        }
    }

    public class EternalGoal : Goal
    {
        public EternalGoal(string name, int points)
        {
            Name = name;
            Points = points;
            IsComplete = false; 
        }

        public override int RecordEvent()
        {
            Console.WriteLine($"Recorded: {Name}, Points Earned: {Points}");
            return Points;
        }

        public override string DisplayStatus()
        {
            return $"[ ] {Name} (Eternal)";
        }
    }

   
    public class ChecklistGoal : Goal
    {
        public int CompletionCount { get; private set; }
        public int RequiredCount { get; private set; }
        private int BonusPoints;

        public ChecklistGoal(string name, int points, int requiredCount, int bonusPoints)
        {
            Name = name;
            Points = points;
            RequiredCount = requiredCount;
            BonusPoints = bonusPoints;
            CompletionCount = 0;
            IsComplete = false;
        }

        public override int RecordEvent()
        {
            if (!IsComplete)
            {
                CompletionCount++;
                int earned = Points;
                if (CompletionCount >= RequiredCount)
                {
                    IsComplete = true;
                    earned += BonusPoints;
                    Console.WriteLine($"Completed: {Name}, Points Earned: {Points} + Bonus: {BonusPoints} = {earned}");
                }
                else
                {
                    Console.WriteLine($"Recorded: {Name}, Progress: {CompletionCount}/{RequiredCount}, Points Earned: {Points}");
                }
                return earned;
            }
            else
            {
                Console.WriteLine($"The checklist goal '{Name}' is already completed.");
                return 0;
            }
        }

        public override string DisplayStatus()
        {
            return IsComplete
                ? $"[X] {Name} (Completed {CompletionCount}/{RequiredCount})"
                : $"[ ] {Name} (Completed {CompletionCount}/{RequiredCount})";
        }

        
        public void SetCompletionCount(int count)
        {
            CompletionCount = count;
        }
    }

    #endregion

    #region Clase Usuario

  
    public class User
    {
        public List<Goal> Goals { get; private set; } = new List<Goal>();
        public int TotalScore { get; private set; }

        
        public void AddGoal(Goal goal)
        {
            Goals.Add(goal);
            Console.WriteLine($"Added Goal: {goal.Name}");
        }

      
        public void RecordGoalEvent(int index)
        {
            if (index >= 0 && index < Goals.Count)
            {
                Goal goal = Goals[index];
               
                if (!goal.IsComplete || goal is EternalGoal)
                {
                    int earnedPoints = goal.RecordEvent();
                    TotalScore += earnedPoints;
                    Console.WriteLine($"Total Score: {TotalScore}\n");
                }
                else
                {
                    Console.WriteLine($"Goal '{goal.Name}' is already completed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid goal index.");
            }
        }


        public void DisplayGoals()
        {
            Console.WriteLine("\n-- Your Goals --");
            for (int i = 0; i < Goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Goals[i].DisplayStatus()}");
            }
        }


        public void DisplayScore()
        {
            Console.WriteLine($"\nTotal Score: {TotalScore}");
        }

        #region Guardar y Cargar


        public void SaveGoals(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(TotalScore);
                foreach (var goal in Goals)
                {
                    if (goal is SimpleGoal)
                    {
                        writer.WriteLine($"SimpleGoal|{goal.Name}|{goal.Points}|{goal.IsComplete}");
                    }
                    else if (goal is EternalGoal)
                    {
                        writer.WriteLine($"EternalGoal|{goal.Name}|{goal.Points}|{goal.IsComplete}");
                    }
                    else if (goal is ChecklistGoal checklist)
                    {
                        writer.WriteLine($"ChecklistGoal|{checklist.Name}|{checklist.Points}|{checklist.IsComplete}|{checklist.CompletionCount}|{checklist.RequiredCount}|{checklist.Points}"); // Último valor se puede usar para el bonus, si se desea
                       
                    }
                }
            }
            Console.WriteLine("Goals saved successfully.\n");
        }

     
        public void LoadGoals(string fileName)
        {
            if (File.Exists(fileName))
            {
                Goals.Clear();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string scoreLine = reader.ReadLine();
                    if (int.TryParse(scoreLine, out int score))
                    {
                        TotalScore = score;
                    }
                    else
                    {
                        TotalScore = 0;
                    }
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split('|');
                        if (parts.Length > 0)
                        {
                            string type = parts[0];
                            switch (type)
                            {
                                case "SimpleGoal":
                                    if (parts.Length == 4)
                                    {
                                        var goal = new SimpleGoal(parts[1], int.Parse(parts[2]));
                                        goal.LoadState(bool.Parse(parts[3]));
                                        Goals.Add(goal);
                                    }
                                    break;
                                case "EternalGoal":
                                    if (parts.Length == 4)
                                    {
                                        var goal = new EternalGoal(parts[1], int.Parse(parts[2]));
                                        goal.LoadState(bool.Parse(parts[3]));
                                        Goals.Add(goal);
                                    }
                                    break;
                                case "ChecklistGoal":
                                    // Se espera que el orden sea: Tipo, Name, Points, IsComplete, CompletionCount, RequiredCount, BonusPoints
                                    if (parts.Length >= 7)
                                    {
                                        var goal = new ChecklistGoal(parts[1], int.Parse(parts[2]), int.Parse(parts[5]), int.Parse(parts[6]));
                                        goal.LoadState(bool.Parse(parts[3]));
                                        goal.SetCompletionCount(int.Parse(parts[4]));
                                        Goals.Add(goal);
                                    }
                                    break;
                            }
                        }
                    }
                }
                Console.WriteLine("Goals loaded successfully.\n");
            }
            else
            {
                Console.WriteLine("File not found.\n");
            }
        }

        #endregion
    }

    #endregion

    #region Clase Program (Punto de Entrada)

    public class Program
    {
        public static void Main(string[] args)
        {
            User user = new User();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("======== Eternal Quest Menu ========");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Display Goals");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateNewGoal(user);
                        break;
                    case "2":
                        RecordGoalEvent(user);
                        break;
                    case "3":
                        user.DisplayGoals();
                        Console.WriteLine();
                        break;
                    case "4":
                        user.DisplayScore();
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.Write("Enter file name to save (e.g., goals.txt): ");
                        string saveFile = Console.ReadLine();
                        user.SaveGoals(saveFile);
                        break;
                    case "6":
                        Console.Write("Enter file name to load (e.g., goals.txt): ");
                        string loadFile = Console.ReadLine();
                        user.LoadGoals(loadFile);
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.\n");
                        break;
                }
            }
        }

      
        private static void CreateNewGoal(User user)
        {
            Console.WriteLine("Select goal type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choice: ");
            string typeChoice = Console.ReadLine();
            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter points per event: ");
            int points = int.Parse(Console.ReadLine());

            switch (typeChoice)
            {
                case "1":
                    user.AddGoal(new SimpleGoal(name, points));
                    break;
                case "2":
                    user.AddGoal(new EternalGoal(name, points));
                    break;
                case "3":
                    Console.Write("Enter number of times required to complete: ");
                    int required = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points on completion: ");
                    int bonus = int.Parse(Console.ReadLine());
                    user.AddGoal(new ChecklistGoal(name, points, required, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid goal type.\n");
                    break;
            }
            Console.WriteLine();
        }

        
        private static void RecordGoalEvent(User user)
        {
            if (user.Goals.Count == 0)
            {
                Console.WriteLine("No goals available. Create a goal first.\n");
                return;
            }

            user.DisplayGoals();
            Console.Write("\nSelect the goal number to record an event: ");
            if (int.TryParse(Console.ReadLine(), out int goalNumber))
            {
                
                user.RecordGoalEvent(goalNumber - 1);
            }
            else
            {
                Console.WriteLine("Invalid input.\n");
            }
        }
    }

    #endregion
}
