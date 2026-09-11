using Task3.Parsing;

namespace Task3.Tests;

public class LogLevelParserTests
{
    [Theory]
    [InlineData("INFO", LogLevel.Info)]
    [InlineData("INFORMATION", LogLevel.Info)]
    [InlineData("WARN", LogLevel.Warning)]
    [InlineData("WARNING", LogLevel.Warning)]
    [InlineData("ERROR", LogLevel.Error)]
    [InlineData("DEBUG", LogLevel.Debug)]
    public void Parse_ReturnsCorrectEnumeratedValue(string logLevelString, LogLevel parsedLogLevel)
    {
        LogLevelParser logLevelParser = new();
        var result = logLevelParser.Parse(logLevelString);

        Assert.Equal(parsedLogLevel, result);
    }
}
