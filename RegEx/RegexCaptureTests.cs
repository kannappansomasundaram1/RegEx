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

    private static Category ExtractCategory(string input)
    {
        var match = CategoryExtractingRegex().Match(input);
        return new Category(match.Groups["code"].Value, match.Groups["description"].Value);
    }

    [GeneratedRegex(@"^\s*(?<code>[a-zA-Z]{3}[0-3]{3})\s*:\s*(?<description>.+?)\s*$")]
    private static partial Regex CategoryExtractingRegex();
}

internal record Category(string Code, string Description);