namespace OpenRentStringLib
{
    public interface IOpenRentStringBuilder
    {
        Result<string> ReverseInput(string? input);

        Result<char> GetEarliestAlphabet(string? input);

        Result<int> CountVowels(string? input);

        Result<string> GetSuffixWordByVowelCount(int vowelCount);

        Result<string> BuildResult(string? input);
    }
}