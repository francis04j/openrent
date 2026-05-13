namespace OpenRentStringLib
{
    public static class OpenRentErrors
    {
        public static readonly Error NullOrEmptyInput = new("OpenRent.Input.NullOrEmpty", "Input is null or empty.");
        public static readonly Error MissingAlphabeticCharacter = new("OpenRent.Input.MissingAlphabeticCharacter", "Input does not contain an alphabetic character.");

        public static Error NegativeVowelCount(int vowelCount)
        {
            return new Error("OpenRent.VowelCount.Negative", $"Vowel count cannot be negative: {vowelCount}.");
        }
    }
}