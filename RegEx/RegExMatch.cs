using System.Text.RegularExpressions;
using FluentAssertions;

namespace RegExTests;

public class RegExMatch
{
    [Theory]
    [InlineData("abc", @"\w", true)]
    [InlineData("990", @"\w", true)]
    [InlineData("abc", @"\W", false)]
    [InlineData("  ", @"\w", false)]
    [InlineData("  ", @"\W", true)]
    public void IsMatch_w(string input, string pattern, bool expected)
    {
        var result = Regex.IsMatch(input, pattern);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("abc", @"\d", false)]
    [InlineData("990", @"\d", true)] //Not a digit
    [InlineData("  ", @"\D", true)]
    public void IsMatch_Digits(string input, string pattern, bool expected)
    {
        var result = Regex.IsMatch(input, pattern);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("(202) 555-4242")]
    [InlineData("(202) 555-1302")]
    [InlineData("(202) 555-130")]
    public void Match_PhoneNumbers(string input)
    {
        var result = Regex.IsMatch(input, @"\(\d{3}\) \d{3}-\d{3,4}");
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("ø")]
    public void Match_Unicode(string input)
    {
        var result = Regex.IsMatch(input, @"\u00F8");
        result.Should().BeTrue();
    }
        
    // [Theory]
    // [InlineData("ice ❄️")]
    // public void Match_Emojis(string input)
    // {
    //     var result = Regex.IsMatch(input, @"\p{Emoji}");
    //     result.Should().BeTrue();
    // }
    
    [Theory]
    [InlineData("(202) 555-4242")]
    [InlineData("(202) 555 1302")]
    [InlineData("(202) 555b130")]
    public void Match_WildCard(string input)
    {
        var result = Regex.IsMatch(input, @"\(\d{3}\) \d{3}.\d{3,4}");
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("mulder",  true)]
    [InlineData("mulder@gmail.com",  false)]
    public void Match_Boundary(string input, bool match)
    {
        var result = Regex.IsMatch(input, @"^mulder$");
        result.Should().Be(match);
    }
}