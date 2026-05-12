namespace OpenRentStringLib
{
    public interface IOpenRentStringBuilder
    {
        string? ReverseInput(string? input);

        char? GetEarliestAlphabet(string input);

        int CountVowels(string input);

        string GetSuffixWordByVowelCount(int vowelCount);

        string BuildResult(string input);
    }
}