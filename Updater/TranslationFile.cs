using System.Text.Json;
using System.Text.Json.Nodes;

public static class TranslationFile
{
    public static async Task<IEnumerable<(string key, string value)>> LoadKeys(string dir, string language)
    {
        string path = Path.Join(dir, $"{language}.json");
        using var openStream = File.OpenRead(path);
        var jsonContent = await JsonSerializer.DeserializeAsync<JsonNode>(openStream);

        return jsonContent?.AsObject().Select(x => (x.Key, (string?)x.Value ?? string.Empty)) ?? Enumerable.Empty<(string, string?)>();
    }

    public static void SaveKeys(string dir, string language, Dictionary<string, string> translations)
    {
        string path = Path.Join(dir, $"{language}.json");

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        File.WriteAllText(path, JsonSerializer.Serialize(translations, options));
    }
}
