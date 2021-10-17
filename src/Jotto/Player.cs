using System;

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
        private string wordToGuess;

        public HumanPlayer()
        {
            guessList = new HumanGuessList();
            SetNewWord();
        }

        public void SetNewWord() //set the word that the user is trying to guess!
        {
            wordToGuess = wordList.RandomWord;
        }

        public string GetWordToGuess()
        {
            return wordToGuess;
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
                    var guess = Console.ReadLine().ToLower();
                    if (!wordList.CheckWord(guess)) //is word valid? Check against initial word list.
                    {
                        throw new ArgumentException($"{guess} is not a valid word.");
                    }
                    guessList.AddGuess(guess); //validates guess and adds to list of guesses

                    madeValidGuess = true;
                    if (guess == wordToGuess) //win!
                    {
                        Console.WriteLine("You are correct!");
                        correctGuess = true;
                    }
                    else
                    {
                        var numberOfMatches = WordList.GetNumberOfMatchedLetters(guess, wordToGuess);
                        Console.WriteLine($"{guess} has {numberOfMatches} matching letters.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return correctGuess;

        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            guessList = new ComputerGuessList();
        }

        //computer makes a guess; user has to tell comp how many letters match. Return true if guess is correct.
        public override bool MakeGuess() 
        {
            var guess = wordList.RandomWord; //get a random word that the computer hasn't eliminated yet
            guessList.AddGuess(guess); //add to list of guesses
            Console.WriteLine($"I guess '{guess}'. How many letters match (0-5)? Enter JOTTO if correct.");
            return getMatchedLettersFromUser(guess); //return bool for if game is over or not
        }

        private bool getMatchedLettersFromUser(string guess)
        {
            var correctGuess = false;
            var input = "";
            var matchedLetters = -1;
            var doneGettingInput = false;
            while (!doneGettingInput) {
                input = Console.ReadLine();
                if (input.ToUpper() == "JOTTO")
                {
                    correctGuess = true;
                    doneGettingInput = true;
                } else 
                {
                    try
                    {
                        matchedLetters = int.Parse(input);
                        if (matchedLetters < 6 && matchedLetters > -1)
                        {
                            doneGettingInput = true;
                            wordList.NarrowWordList(guess, matchedLetters); //narrow remaining possible words
                        }
                        else 
                        {
                            throw new FormatException();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Enter a digit 0 through 5 or 'JOTTO' if correct.");
                        continue;
                    }
                }
            }
            return correctGuess;
        }

    }
}