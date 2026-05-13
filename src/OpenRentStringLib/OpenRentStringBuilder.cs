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

        public Result<string> ReverseInput(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Failure<string>(OpenRentErrors.NullOrEmptyInput);
            }

            return Result<string>.Success(new string(input.Reverse().ToArray()));
        }

        public Result<char> GetEarliestAlphabet(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Failure<char>(OpenRentErrors.NullOrEmptyInput);
            }

            char? earliestCharacter = input
                .Where(char.IsLetter)
                .Select(char.ToLowerInvariant)
                .Cast<char?>()
                .DefaultIfEmpty(null)
                .Min();

            if (!earliestCharacter.HasValue)
            {
                return Failure<char>(OpenRentErrors.MissingAlphabeticCharacter);
            }

            return Result<char>.Success(earliestCharacter.Value);
        }

        public Result<int> CountVowels(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Failure<int>(OpenRentErrors.NullOrEmptyInput);
            }

            return Result<int>.Success(input.Count(character => Vowels.Contains(char.ToLowerInvariant(character))));
        }

        public Result<string> GetSuffixWordByVowelCount(int vowelCount)
        {
            if (vowelCount < 0)
            {
                return Failure<string>(OpenRentErrors.NegativeVowelCount(vowelCount));
            }

            return Result<string>.Success(vowelCount % 2 == 0 ? "rent" : "open");
        }

        public Result<string> BuildResult(string? input)
        {
            var reversedInput = ReverseInput(input);
            if (reversedInput.IsFailure)
            {
                return Result<string>.Failure(reversedInput.Error);
            }

            var earliestCharacter = GetEarliestAlphabet(input);
            if (earliestCharacter.IsFailure)
            {
                return Result<string>.Failure(earliestCharacter.Error);
            }

            var vowelCount = CountVowels(input);
            if (vowelCount.IsFailure)
            {
                return Result<string>.Failure(vowelCount.Error);
            }

            var suffix = GetSuffixWordByVowelCount(vowelCount.Value);
            if (suffix.IsFailure)
            {
                return Result<string>.Failure(suffix.Error);
            }

            return Result<string>.Success(string.Concat(reversedInput.Value, earliestCharacter.Value.ToString(), suffix.Value));
        }

        private Result<T> Failure<T>(Error error)
        {
            _logger.LogWarning(error.Message);
            return Result<T>.Failure(error);
        }
    }
}