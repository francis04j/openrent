using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace OpenRentStringLib
{
    public sealed class OpenRentStringBuilder : IOpenRentStringBuilder
    {
        private static readonly HashSet<char> Vowels = new() { 'a', 'e', 'i', 'o', 'u' };
        private readonly ILogger<OpenRentStringBuilder> _logger;

        public OpenRentStringBuilder()
            : this(NullLogger<OpenRentStringBuilder>.Instance)
        {
        }

        public OpenRentStringBuilder(ILogger<OpenRentStringBuilder> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string? ReverseInput(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                _logger.LogWarning("Input is null or empty.");
                return input;
            }

            return new string(input.Reverse().ToArray());
        }

        public char? GetEarliestAlphabet(string input)
        {
           if (string.IsNullOrEmpty(input))
            {
                _logger.LogWarning("Input is null or empty.");
                return null;
            }

            char? earliestCharacter = input
                .Where(char.IsLetter)
                .Select(char.ToLowerInvariant)
                .Cast<char?>()
                .DefaultIfEmpty(null)
                .Min();

            if (!earliestCharacter.HasValue)
            {
                _logger.LogWarning("Input does not contain an alphabetic character.");
                return null;
            }

            return earliestCharacter;
        }

        public int CountVowels(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                _logger.LogWarning("Input is null or empty.");
                return 0;
            }

            return input.Count(character => Vowels.Contains(char.ToLowerInvariant(character)));
        }

        public string GetSuffixWordByVowelCount(int vowelCount)
        {
            if (vowelCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vowelCount), "Vowel count cannot be negative.");
            }

            return vowelCount % 2 == 0 ? "rent" : "open";
        }

        public string BuildResult(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                _logger.LogWarning("Input is null or empty.");
                return string.Empty;
            }

            var reversedInput = ReverseInput(input);
            var earliestCharacter = GetEarliestAlphabet(input);
            var vowelCount = CountVowels(input);
            var suffix = GetSuffixWordByVowelCount(vowelCount);

            return string.Concat(reversedInput, earliestCharacter?.ToString() ?? string.Empty, suffix);
        }
    }
}