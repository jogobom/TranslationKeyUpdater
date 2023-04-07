public static class KeyReformatter
{
    public static (string original, string replacement) PairWithSnakeCase(this string key)
    {
        var delimeters = new[] { ' ', ':' };
        var words = key.Split(delimeters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var firstWord = words.Take(1);
        var subsequentWords = words.Skip(1).Select(CapitalizeFirstLetter);
        
        return (key, string.Join(string.Empty, firstWord.Concat(subsequentWords)));
    }

    private static string CapitalizeFirstLetter(string word)
    {
        var firstLetter = word.Take(1).Select(char.ToUpper);
        var subsequentLetters = word.Skip(1);
        return new string(firstLetter.Concat(subsequentLetters).ToArray())?? word;
    }
}
