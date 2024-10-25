using System.Text.RegularExpressions;
using FluentAssertions;

namespace RegExTests;

public partial class RegexCaptureTests
{
    [Theory]
    [InlineData("ABC123:SteamEngine", "ABC123")]
    [InlineData("  ABC123:SteamEngine", "ABC123")]
    [InlineData("ABC123   :SteamEngine", "ABC123")]
    [InlineData("ABC123:   SteamEngine", "ABC123")]
    [InlineData("ABC123:SteamEngine  ", "ABC123")]
    public void RegexCapture_Captures_Groups_When_String_Is_Valid(String input, String expectedCode)
    {
        String expectedCategory = "SteamEngine";
        var category = ExtractCategory(input);
        category.Code.Should().Be(expectedCode);
        category.Description.Should().Be(expectedCategory);
    }
    
    [Theory]
    [InlineData("<h1>hello, world</h1>", "<h1>hello, world</h1>")]
    public void Captures_UsingGreedyQuantifiers(String input, String expectedCode)
    {
        var result = Regex.Match(input, @"(<.+>)"); //one or more time
        result.Groups[1].Value.Should().Be(expectedCode);
    }
    
    [Theory]
    [InlineData("<h1>hello, world</h1>")]
    public void Captures_UsingLazyQuantifiers(String input)
    {
        var result = Regex.Match(input, @"(<.+?>)");
        result.Groups[1].Value.Should().Be("<h1>");
    }

    private static Category ExtractCategory(string input)
    {
        var match = CategoryExtractingRegex().Match(input);
        return new Category(match.Groups["code"].Value, match.Groups["description"].Value);
    }

    [GeneratedRegex(@"^\s*(?<code>[a-zA-Z]{3}[0-3]{3})\s*:\s*(?<description>.+?)\s*$")]
    private static partial Regex CategoryExtractingRegex();
}

internal record Category(string Code, string Description);