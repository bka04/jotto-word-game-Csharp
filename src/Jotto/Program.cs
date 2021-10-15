using System;

namespace Jotto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            WordList wordList = new WordList("Five Letter Words");
            var rando = wordList.RandomWord;
            Console.WriteLine(rando);
        }
    }
}
