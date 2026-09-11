using Task3.Parsing;
using Task3.Tests.Mocks;

namespace Task3.Tests;

public class FirstFormatLogParserTests
{
    [Fact]
    public void TryParse_CorrectFormat_ReturnsTrueAndCorrectlySetsLogRecord()
    {
        string logRecordString = "10.03.2025 15:14:49.523 INFORMATION Âåðñèÿ ïðîãðàììû: '3.4.0.48729'";

        LogLevelParserMock logLevelParserMock = new();
        FirstFormatLogParser firstFormatLogParser = new();

        bool result = firstFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.True(result);
        Assert.True(logRecord.HasValue);
    }

    [Fact]
    public void TryParse_IncorrectFormat_ReturnsFalseAndSetsLogRecordToNull()
    {
        string logRecordString = "obviously incorrect format";

        LogLevelParserMock logLevelParserMock = new();
        FirstFormatLogParser firstFormatLogParser = new();

        bool result = firstFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.False(result);
        Assert.False(logRecord.HasValue);
    }

    [Fact]
    public void TryParse_CorrectFormatButIncorrectDateValue_ReturnsFalseAndSetsLogRecordToNull()
    {
        string logRecordString = "10.99.2025 15:14:49.523 INFORMATION Âåðñèÿ ïðîãðàììû: '3.4.0.48729'";

        LogLevelParserMock logLevelParserMock = new();
        FirstFormatLogParser firstFormatLogParser = new();

        bool result = firstFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.False(result);
        Assert.False(logRecord.HasValue);
    }

    [Fact]
    public void TryParse_CorrectFormatButIncorrectTimeValue_ReturnsFalseAndSetsLogRecordToNull()
    {
        string logRecordString = "10.03.2025 99:14:49.523 INFORMATION Âåðñèÿ ïðîãðàììû: '3.4.0.48729'";

        LogLevelParserMock logLevelParserMock = new();
        FirstFormatLogParser firstFormatLogParser = new();

        bool result = firstFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.False(result);
        Assert.False(logRecord.HasValue);
    }
}
