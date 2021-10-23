using System;
using System.Linq;

namespace Jotto
{
    public abstract class Player
    {
        public Player()
        {
            wordList = new WordList("Five Letter Words");
        }

        public WordList wordList;
        public GuessList guessList;
        public abstract bool MakeGuess();
    }

    public class HumanPlayer : Player
    {
        public string wordToGuess { get; private set; }

        public HumanPlayer()
        {
            guessList = new GuessList();
            SetNewWord();
        }

        public void SetNewWord() //set the word that the user is trying to guess!
        {
            wordToGuess = wordList.RandomWord;
        }

        //user makes a guess; computer tells user how many letters match. Return true if guess is correct.
        public override bool MakeGuess() 
        {
            var madeValidGuess = false;
            var correctGuess = false;
            while (!madeValidGuess)
            {
                try
                {
                    Console.WriteLine("Enter a five letter word with no repeating letters!");
                    var wordGuessed = Console.ReadLine().ToLower();

                    if (!wordList.CheckWord(wordGuessed)) //is word valid? Check against initial word list.
                    {
                        throw new ArgumentException($"{wordGuessed} is not a valid word.");
                    }
                    if (guessList.InGuessList(wordGuessed))
                    {
                        throw new System.ArgumentException($"You already guessed {wordGuessed}.");
                    }

                    if (wordGuessed == wordToGuess) //win!
                    {
                        Console.WriteLine("You are correct!");
                        correctGuess = true;
                    }
                    else //incorrect guess. Add to guessList and let user know how many letters matched
                    {
                        var numberOfMatches = WordList.GetNumberOfMatchedLetters(wordGuessed, wordToGuess);
                        Guess guess = new Guess() {Word = wordGuessed, LettersMatched = numberOfMatches};
                        guessList.AddGuess(guess);
                        Console.WriteLine($"{wordGuessed} has {numberOfMatches} matching letters.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                madeValidGuess = true;
            }
            return correctGuess;

        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            guessList = new GuessList();
        }

        //computer makes a guess; user has to tell comp how many letters match. Return true if guess is correct.
        public override bool MakeGuess() 
        {
            //START HERE AND USE ASKUSERTOFIXMATCHEDLETTERS IF WORDLIST IS EMPTY

            var wordGuessed = wordList.RandomWord; //get a random word that the computer hasn't eliminated yet
            Console.WriteLine($"I guess '{wordGuessed}'. How many letters match (0-5)? Enter JOTTO if correct.");

            //get number of letters matched and bool for if game is over or not
            var item = getMatchedLettersFromUser(wordGuessed); 
            int matchedLetters = item.Item1;
            bool correctGuess = item.Item2;
            if (!correctGuess) 
            {
                wordList.NarrowWordList(wordGuessed, matchedLetters); //narrow remaining possible words
                guessList.AddGuess(new Guess() {Word = wordGuessed, LettersMatched = matchedLetters}); //add to list of guesses
            }
            return correctGuess;
        }

        private void askUserToFixMatchedLetters()
        {
            string wordToUpdate = "";
            bool doneGettingInput = false;
            Console.Clear();
            Console.WriteLine("There are no remaining possible words.\nPlease review my previous guesses to ensure the number of letters matched is accurate.\n");
            while (!doneGettingInput)
            {
                guessList.DisplayPreviousGuesses();
                Console.WriteLine("\n Please enter the word that needs its letters matched updated. Enter 'Done' when finished.");
                wordToUpdate = Console.ReadLine().ToLower();

                if (wordToUpdate.ToUpper() == "DONE")
                {
                    doneGettingInput = true;
                } else
                {
                    try {
                        if (!guessList.InGuessList(wordToUpdate))
                            {
                                throw new System.ArgumentException($"I never guessed {wordToUpdate}.");
                            }
                    }  
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    var item = getMatchedLettersFromUser(wordToUpdate);
                    guessList.UpdateLettersMatched(wordToUpdate, item.Item1);
                }
            }
            //recalc word list
            //if still none, clear out guess list and start over.
        }

        private (int, bool) getMatchedLettersFromUser(string guess)
        {
            var correctGuess = false;
            var input = "";
            var matchedLetters = -1;
            var doneGettingInput = false;
            while (!doneGettingInput) {
                input = Console.ReadLine(); //get number of matched letters from user (or JOTTO if correct)
                if (input.ToUpper() == "JOTTO")
                {
                    correctGuess = true;
                    doneGettingInput = true;
                } 
                else 
                {
                    try
                    {
                        matchedLetters = int.Parse(input);
                        doneGettingInput = Guess.ValidateDigitInput(matchedLetters, 0, 5);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Enter a digit 0 through 5 or 'JOTTO' if correct.");
                        continue;
                    }
                }
            }
            return (matchedLetters, correctGuess);
        }

    }
}