namespace Jotto
{
    public class Guess
    {
        public int LettersMatched {get; set;}
        public string Word {get; set;}
    }

    // private static bool hasNonLetters(string word)
    // {
    //     return word.Any(ch => !Char.IsLetter(ch));
    // }

    // private static bool hasRepeatingLetters(string word)
    // {
    //     return word.ToLower().GroupBy(x => x).Any(g => g.Count() > 1);
    // }

    // public static bool GuessFollowedRules(string word)
    // {
        // if (hasNonLetters(guess)) //must be all letters
        // {
        //     throw new System.ArgumentException($"{guess} has non-letters.");
        // }
        // else if (hasRepeatingLetters(guess)) //can't have repeating letters
        // {
        //     throw new System.ArgumentException($"{guess} has repeating letters.");
        // }
        // else if (guess.Length != 5)
        // {
        //     throw new System.ArgumentException($"{guess} does not have five letters");
        // }
    // }
}