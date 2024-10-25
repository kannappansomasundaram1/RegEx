using System.Text.RegularExpressions;
using FluentAssertions;

namespace RegExTests;

public class ExtractPhoneNumbersTest
{
    [Fact]
    public void ExtractPhoneNumbers()
    {
        var input = """
                    users:
                    - name: Fox Mulder
                      firstname: Fox
                      lastname: Mulder
                      phone: (202) 555-4242
                    - name: Monica Reyes
                      firstname: Monica
                      lastname: Reyes
                      phone: (202) 555-5555
                    - name: John Doggett
                      firstname: John
                      lastname: Doggett
                      phone: (202) 555-6666
                    """;
        var result = Regex.Matches(input, @"\(\d{3}\) \d{3}-\d{4}");
        result.Select(x=>x.Value).Should().BeEquivalentTo(
            "(202) 555-4242", 
            "(202) 555-5555", 
            "(202) 555-6666");
    }
}