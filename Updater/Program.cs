using System.Diagnostics;

const string translationsDir = @"C:\repos\bitbucket\cragside\Cragside\ui\src\assets\i18n";
const string sourceFilesDir = @"C:\repos\bitbucket\cragside\Cragside\ui\src";
const string sourceLanguage = "en-US";

var timer = Stopwatch.StartNew();

Console.WriteLine($"Searching directory \"{translationsDir}\" for {sourceLanguage}.json");

var originalKeys = (await TranslationFile.LoadKeys(translationsDir, sourceLanguage)).ToList();

Console.WriteLine($"Found {originalKeys.Count} keys in {sourceLanguage}.json");

var keysWithReplacements = originalKeys.Select(KeyReformatter.PairWithSnakeCase);

var duplicates = keysWithReplacements.GroupBy(kv => kv.replacement).Where(g => g.Count() > 1);

if (duplicates.Any())
{
    Console.WriteLine("Found keys whose updated names would clash:");
    foreach (var duplicate in duplicates)
    {
        var origKeyNames = duplicate.Select(d => d.original);

        Console.WriteLine($"Keys named {string.Join(", ", origKeyNames.Select(k => $"\"{k}\""))} all convert to \"{duplicate.Key}\".");
    }
    Console.WriteLine("Exiting without updating anything.");
    return;
}

var codeFiles = FileFinder.FindCodeFiles(sourceFilesDir);

foreach (var codeFile in codeFiles)
{
    var fileContent = File.ReadAllText(codeFile);
    if (originalKeys.Select(o => o.key).Any(fileContent.Contains))
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

var newTranslationFileContent = keysWithReplacements.ToDictionary(kv => kv.replacement, kv => kv.value);

TranslationFile.SaveKeys(translationsDir, sourceLanguage, newTranslationFileContent);

timer.Stop();

Console.WriteLine($"Finished replacing old keys in {timer.Elapsed}");