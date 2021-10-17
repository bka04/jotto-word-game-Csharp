using System.Collections.Generic;
using System;
using System.Linq;

namespace Jotto
{

    public abstract class GuessList
    {
        public GuessList()
        {
            guessList = new List<string>();
        }

        public abstract void AddGuess(string guess); //will implement differently for computer vs player
        protected List<string> guessList;
    }

    public class HumanGuessList : GuessList
    {

        public override void AddGuess(string guess) //must validate player's guess before adding
        {
            if (guessList.Contains(guess.ToLower())) //can't guess the same word again
            {
                throw new System.ArgumentException($"You already guessed {guess}.");
            }
            // else if (hasNonLetters(guess)) //must be all letters
            // {
            //     throw new System.ArgumentException($"{guess} has non-letters.");
            // }
            // else if (hasRepeatingLetters(guess)) //can't have repeating letters
            // {
            //     throw new System.ArgumentException($"{guess} has repeating letters.");
            // }
            // else if (guess.Length != 5)
            // {
            //     throw new System.ArgumentException($"{guess} does not have five letters");
            // }
            else
            {
                guessList.Add(guess.ToLower());
            }

        }

        private static bool hasNonLetters(string word)
        {
            return word.Any(ch => !Char.IsLetter(ch));
        }

        private static bool hasRepeatingLetters(string word)
        {
            return word.ToLower().GroupBy(x => x).Any(g => g.Count() > 1);
        }

        public string GetGuessByIndex(int index) //for unit test
        {
            return guessList[index];
        }
    }

    public class ComputerGuessList : GuessList
    {
        public override void AddGuess(string guess) //just add guess - no need to validate
        {
            guessList.Add(guess);
        }
    }
}