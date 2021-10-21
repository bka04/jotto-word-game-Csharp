using System.Collections.Generic;
using System;
using System.Linq;

namespace Jotto
{

    public class GuessList
    {
        private List<Guess> guessList;

        public GuessList()
        {
            guessList = new List<Guess>();
        }

        public void AddGuess(Guess guess)
        {
            guess.Word = guess.Word.ToLower();
            guessList.Add(guess);
        }

        public bool HaveNotGuessedYet(string wordGuessed)
        {
            if (guessList.Exists(x => x.Word == wordGuessed.ToLower()))
            {
                throw new System.ArgumentException($"You already guessed {wordGuessed}.");
            }
            return true; //if no exception, then haven't guessed it yet
        }
    }

}