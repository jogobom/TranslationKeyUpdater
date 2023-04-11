namespace Tests;

public class TranslationFileTests
{
    [Fact]
    public async Task LoadsExpectedKeys()
    {
        var keys = await TranslationFile.LoadKeys("TestFiles", "en-US");

        keys.Should().ContainInOrder(("jar ts", "Fixed TS"), ("jam cs", "Fixed CS"), ("marm html", "Your company's global reason policy requires that you provide a reason for this action. You can select a reason previously defined by your System Administrator and add comments to it or you can type your own reason."));
    }
}