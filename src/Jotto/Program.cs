using System;

namespace Jotto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            HumanPlayer user = new HumanPlayer();
            ComputerPlayer comp = new ComputerPlayer();

            var gameOver = false;
            var userCorrect = false;
            var compCorrect = false;

            while (!gameOver)
            {
                userCorrect = user.MakeGuess();
                compCorrect = comp.MakeGuess();
                gameOver = (userCorrect || compCorrect);
            }

            if (userCorrect)
            {
                if (compCorrect)
                {
                    Console.WriteLine("You tie!");
                }
                else
                {
                    Console.WriteLine("You win!");
                }
            } else 
            {
                Console.WriteLine($"You lose. Your word was '{user.wordToGuess}'.");
            }
            
        }

    }
}
