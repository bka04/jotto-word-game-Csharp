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
                Console.WriteLine(randomWord);
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
                Console.WriteLine(randomWord);
            }

            //Assert
            Assert.Equal(lastWord, randomWord);
        }
    }
}
