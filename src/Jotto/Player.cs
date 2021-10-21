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
            guessList = new GuessList();
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
                    var wordGuessed = Console.ReadLine().ToLower();
                    //TO DO: PULL thIS CHECK OUT INTO A SEPARATE METHOD
                    if (!wordList.CheckWord(wordGuessed)) //is word valid? Check against initial word list.
                    {
                        throw new ArgumentException($"{wordGuessed} is not a valid word.");
                    }

                    madeValidGuess = guessList.HaveNotGuessedYet(wordGuessed); //throws exception if already guessed
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
                }
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
            var wordGuessed = wordList.RandomWord; //get a random word that the computer hasn't eliminated yet
            Console.WriteLine($"I guess '{wordGuessed}'. How many letters match (0-5)? Enter JOTTO if correct.");

            //get number of letters matched and bool for if game is over or not
            var item = getMatchedLettersFromUser(wordGuessed); 
            int matchedLetters = item.Item1;
            bool correctGuess = item.Item2;
            guessList.AddGuess(new Guess() {Word = wordGuessed, LettersMatched = matchedLetters}); //add to list of guesses
            return correctGuess;
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
            return (matchedLetters, correctGuess);
        }

    }
}