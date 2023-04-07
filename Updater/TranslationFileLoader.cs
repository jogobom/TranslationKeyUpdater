using System.Text.Json;
using System.Text.Json.Nodes;

public static class TranslationFileLoader
{
    public static async Task<IEnumerable<string>> LoadKeys(string dir, string language)
    {
        string path = Path.Join(dir, $"{language}.json");
        using var openStream = File.OpenRead(path);
        var jsonContent = await JsonSerializer.DeserializeAsync<JsonNode>(openStream);

        return jsonContent?.AsObject().Select(x => x.Key) ?? Enumerable.Empty<string>();
    }
}
