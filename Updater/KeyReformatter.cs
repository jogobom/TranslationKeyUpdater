public static class KeyReformatter
{
    public static (string original, string replacement, string value) PairWithSnakeCase(this (string key, string value) original)
    {
        var delimeters = new[] { ' ', ':' };
        var words = original.key.Split(delimeters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var firstWord = words.Take(1).Select(UncapitalizeFirstLetter);
        var subsequentWords = words.Skip(1).Select(CapitalizeFirstLetter);
        
        return (original.key, string.Join(string.Empty, firstWord.Concat(subsequentWords)), original.value);
    }

    private static string CapitalizeFirstLetter(string word)
    {
        return DoSomethingToFirstLetter(word, char.ToUpper);
    }

    private static string UncapitalizeFirstLetter(string word)
    {
        return DoSomethingToFirstLetter(word, char.ToLower);
    }

    private static string DoSomethingToFirstLetter(string word, Func<char, char> thingToDo)
    {
        var firstLetter = word.Take(1).Select(thingToDo);
        var subsequentLetters = word.Skip(1);
        return new string(firstLetter.Concat(subsequentLetters).ToArray()) ?? word;
    }

}

