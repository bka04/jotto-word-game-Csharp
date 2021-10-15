using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jotto
{
    public class WordList
    {
        public WordList(string name) //constructor
        {
            Name = name;
            words = File.ReadLines($"{Name}.txt").ToList();
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

        public string GetWordByIndex(int index)
        {
            return words[index];
        }

        public bool CheckWord()
        {
            return true;
        }

        static Random rnd = new Random();
    }
}