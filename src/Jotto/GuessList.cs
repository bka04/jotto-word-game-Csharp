using System.Collections.Generic;
using System;

namespace Jotto
{

    public class GuessList
    {
        private List<Guess> guessList;

        public GuessList()
        {
            guessList = new List<Guess>();
        }

        public void DisplayPreviousGuesses()
        {
            foreach (var guess in guessList)
            {
                Console.WriteLine($"{guess.Word} : {guess.LettersMatched}");
            }
        }

        public WordList RecalcGuesses()
        {
            WordList wordList = new WordList("Five Letter Words");
            foreach (var guess in guessList)
            {
                Console.WriteLine($"RECALC! {guess.Word} {guess.LettersMatched}");
                wordList.NarrowWordList(guess.Word, guess.LettersMatched);
                Console.WriteLine(wordList.wordCount());
            }
            return wordList;
        }

        public void AddGuess(Guess guess)
        {
            guess.Word = guess.Word.ToLower();
            guessList.Add(guess);
        }

        public bool InGuessList(string wordGuessed) //already guessed this word?
        {
            if (guessList.Exists(x => x.Word == wordGuessed.ToLower()))
            {
                return true;
            }
            return false;
        }

        public void UpdateLettersMatched(string wordGuessed, int lettersMatched)
        {
            Guess guess = guessList.Find(x => x.Word == wordGuessed.ToLower()); //get guess in list
            if (guess != null) 
            {
                guess.LettersMatched = lettersMatched; //update to new value
            } 
        }

    }

}