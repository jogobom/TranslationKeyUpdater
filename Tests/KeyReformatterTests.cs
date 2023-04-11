namespace Tests;

public class KeyReformatterTests
{
    [Theory]
    [InlineData("key", "key")]
    [InlineData("a key", "aKey")]
    [InlineData("a more complex key", "aMoreComplexKey")]
    [InlineData("aCorrectKey", "aCorrectKey")]
    [InlineData("a key: with a colon", "aKeyWithAColon")]
    [InlineData("Key with caps at start", "keyWithCapsAtStart")]
    public void ToSnakeCase(string input, string expectedResult)
    {
        (input, "").PairWithSnakeCase().Should().Be((input, expectedResult, ""));
    }
}
