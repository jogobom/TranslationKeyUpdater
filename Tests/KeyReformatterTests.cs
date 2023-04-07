namespace Tests;

public class KeyReformatterTests
{
    [Theory]
    [InlineData("key", "key")]
    [InlineData("a key", "aKey")]
    [InlineData("a more complex key", "aMoreComplexKey")]
    [InlineData("aCorrectKey", "aCorrectKey")]
    [InlineData("a key: with a colon", "aKeyWithAColon")]
    public void ToSnakeCase(string input, string expectedResult)
    {
        input.ToSnakeCase().Should().Be(expectedResult);
    }
}
