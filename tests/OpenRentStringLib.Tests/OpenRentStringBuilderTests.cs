using System;
using Microsoft.Extensions.Logging;
using OpenRentStringLib;
using Xunit;

namespace OpenRentStringLib.Tests
{
    public sealed class OpenRentStringBuilderTests
    {
        private readonly IOpenRentStringBuilder _builder = new OpenRentStringBuilder();

        [Fact]
        public void ReverseInput_ReturnsReversedString()
        {
            var result = _builder.ReverseInput("nepo");

            Assert.Equal("open", result);
        }

        [Fact]
        public void ReverseInput_LogsWarningAndReturnsNull_WhenInputIsNull()
        {
            var logger = new TestLogger();
            var builder = new OpenRentStringBuilder(logger);

            var result = builder.ReverseInput(null);

            Assert.Null(result);
            Assert.Equal(LogLevel.Warning, logger.LastLogLevel);
            Assert.Equal("Input is null or empty.", logger.LastMessage);
        }

        [Fact]
        public void ReverseInput_LogsWarningAndReturnsEmptyString_WhenInputIsEmpty()
        {
            var logger = new TestLogger();
            var builder = new OpenRentStringBuilder(logger);

            var result = builder.ReverseInput(string.Empty);

            Assert.Equal(string.Empty, result);
            Assert.Equal(LogLevel.Warning, logger.LastLogLevel);
            Assert.Equal("Input is null or empty.", logger.LastMessage);
        }

        [Fact]
        public void GetEarliestAlphabetCharacter_ReturnsAlphabeticallyEarliestCharacter()
        {
            var result = _builder.GetEarliestAlphabet("nepo");

            Assert.Equal('e', result);
        }

        [Fact]
        public void GetEarliestAlphabetCharacter_LogsWarningAndReturnsNull_WhenNoAlphabeticCharacterExists()
        {
            var logger = new TestLogger();
            var builder = new OpenRentStringBuilder(logger);

            var result = builder.GetEarliestAlphabet("1234");

            Assert.Null(result);
            Assert.Equal(LogLevel.Warning, logger.LastLogLevel);
            Assert.Equal("Input does not contain an alphabetic character.", logger.LastMessage);
        }

        [Fact]
        public void CountVowels_ReturnsNumberOfVowels()
        {
            var result = _builder.CountVowels("nepo");

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetVowelParityWord_ReturnsOpenForOddVowelCount()
        {
            var result = _builder.GetSuffixWordByVowelCount(3);

            Assert.Equal("open", result);
        }

        [Fact]
        public void BuildResult_ReturnsExpectedCombinedString()
        {
            var result = _builder.BuildResult("nepo");

            Assert.Equal("openerent", result);
        }

        private sealed class TestLogger : ILogger<OpenRentStringBuilder>
        {
            public LogLevel? LastLogLevel { get; private set; }
            public string? LastMessage { get; private set; }

            public IDisposable BeginScope<TState>(TState state)
            {
                return NoOpDisposable.Instance;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                LastLogLevel = logLevel;
                LastMessage = formatter(state, exception);
            }

            private sealed class NoOpDisposable : IDisposable
            {
                public static readonly NoOpDisposable Instance = new NoOpDisposable();

                public void Dispose()
                {
                }
            }
        }
    }
}