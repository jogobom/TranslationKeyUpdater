namespace Tests;

public class TranslationFileLoaderTests
{
    [Fact]
    public async Task LoadsExpectedKeys()
    {
        var keys = await TranslationFileLoader.LoadKeys("/Users/chris/dev/github/TranslationKeyUpdater/TestFiles", "en-US");

        keys.Should().ContainInOrder("jar ts", "jam cs", "marm html");
    }
}