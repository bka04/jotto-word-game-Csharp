using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jotto
{
    public class WordList
    {
        public WordList(string name) //constructor - intialize word list from text file
        {
            Name = name;
            words = File.ReadLines($"/Users/brentaronsen/jotto/{Name}.txt").ToList();
        }

        private List<string> words;
        public string Name
        {
            get; set;
        }
        public string RandomWord
        {
            get
            {
                int r = rnd.Next(words.Count);
                return words[r];
            }
        }

        public string GetWordByIndex(int index) //primarily for unit test
        {
            return words[index];
        }

        public bool CheckWord(string word) //is word in list?
        {
            return words.Contains(word);
        }

        public void NarrowWordList(string guess, int lettersMatched) //narrow word list based on guess and how many letters match
        {
            words = words.FindAll(word => (GetNumberOfMatchedLetters(guess, word) == lettersMatched) && (word != guess) );
        }

        static public int GetNumberOfMatchedLetters(string guess, string wordListWord)
        {
            var matches = 0;
            foreach (char letter in guess)
            {
                if (wordListWord.Contains(letter))
                {
                    matches++;
                }
            }
            return matches;
        }

        static Random rnd = new Random();
    }
}