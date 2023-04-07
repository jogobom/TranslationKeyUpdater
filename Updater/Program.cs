using System.Diagnostics;

const string translationsDir = "/Users/chris/dev/bitbucket/TranslationKeyUpdater/TestFiles";
const string sourceFilesDir = "/Users/chris/dev/bitbucket/TranslationKeyUpdater/TestFiles";
const string sourceLanguage = "en-US";

var timer = Stopwatch.StartNew();

Console.WriteLine($"Searching directory \"{translationsDir}\" for {sourceLanguage}.json");

var originalKeys = (await TranslationFileLoader.LoadKeys(translationsDir, sourceLanguage)).ToList();

Console.WriteLine($"Found {originalKeys.Count} keys in {sourceLanguage}.json");

var keysWithReplacements = originalKeys.Select(KeyReformatter.PairWithSnakeCase);

var codeFiles = FileFinder.FindCodeFiles(sourceFilesDir);

foreach (var codeFile in codeFiles)
{
    var fileContent = File.ReadAllText(codeFile);
    if (originalKeys.Any(fileContent.Contains))
    {
        Console.WriteLine($"Founds keys to replace in {codeFile}");

        var newContent = fileContent;

        foreach (var keyToReplace in keysWithReplacements.Where(p => newContent.Contains(p.original)))
        {
            newContent = newContent.Replace(keyToReplace.original, keyToReplace.replacement);
        }

        File.WriteAllText(codeFile, newContent);
    }
}

timer.Stop();

Console.WriteLine($"Finished replacing old keys in {timer.Elapsed}");