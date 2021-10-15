using System;
using Xunit;

namespace Jotto.Tests
{
    public class JottoTests
    {
        [Fact]
        public void CanRetrieveFirstWord()
        {
            //Arrange
            var wordList = new WordList("Five Letter Words Test");
            var firstWord = wordList.GetWordByIndex(0);
            var randomWord = "";
            var counter = 0;

            //Act
            while (randomWord != firstWord && counter < 30) {
                randomWord = wordList.RandomWord;
                counter += 1;
            }

            //Assert
            Assert.Equal(firstWord, randomWord);
        }

        [Fact]
        public void CanRetrieveLastWord()
        {
            //Arrange
            var wordList = new WordList("Five Letter Words Test");
            var lastWord = wordList.GetWordByIndex(2);
            var randomWord = "";
            var counter = 0;

            //Act
            while (randomWord != lastWord && counter < 30) {
                randomWord = wordList.RandomWord;
                counter += 1;
            }

            //Assert
            Assert.Equal(lastWord, randomWord);
        }

        [Fact]
        public void WordListChecksWord()
        {
            //Arrange
            var wordList = new WordList("Five Letter Words Test");
            var wordToCheck = "abets";
            var wordToCheck2 = "zzzzz";

            //Act
            var wordFound = wordList.CheckWord(wordToCheck);
            var wordFound2 = wordList.CheckWord(wordToCheck2);

            //Assert
            Assert.True(wordFound);
            Assert.False(wordFound2);
        }

        [Fact]
        public void ComputerCanSolveJotto()
        {
            //Arrange
            var wordList = new WordList("Five Letter Words Test Full");
            var wordToGuess = wordList.RandomWord;
            var guess = "";
            var counter = 0; //to avoid infinite loop
            Console.WriteLine($"Word to guess: {wordToGuess}");

            //Act
            while (guess != wordToGuess && counter < 50) {
                guess = wordList.RandomWord;
                var lettersMatched = WordList.GetNumberOfMatchedLetters(guess, wordToGuess);
                Console.WriteLine($"Guess: {guess}. Letters matched: {lettersMatched}.");
                wordList.NarrowWordList(guess, lettersMatched);
                counter++;
            }

            //Assert
            Assert.Equal(guess, wordToGuess);
        }
    }
}
