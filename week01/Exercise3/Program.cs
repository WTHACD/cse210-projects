using System;

class Program
{
    static void Main(string[] args)
    {
        bool exercise3 = true; 
        while (exercise3)
        {
            Console.WriteLine("Welcome to the Guess My Number game!");

            
            Random rand = new Random();
            int magicNumber = rand.Next(1, 101);

            int guess = 0; 
            int attempts = 0; 

            Console.WriteLine("I have picked a number between 1 and 100. Try to guess it!");
          
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                attempts++; 

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            Console.WriteLine($"It took you {attempts} guesses to find the magic number.");

            Console.Write("Do you want to play again? (yes/no): ");
            string response = Console.ReadLine().ToLower();
                    if (response != "yes")
            {
                exercise3 = false;
                Console.WriteLine("Thanks for playing! Goodbye.");
            }
        }
    }
} 