using Task3.Parsing;
using Task3.Tests.Mocks;

namespace Task3.Tests;

public class SecondFormatLogParserTests
{
    [Fact]
    public void TryParse_CorrectFormat_ReturnsTrueAndCorrectlySetsLogRecord()
    {
        string logRecordString = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        LogLevelParserMock logLevelParserMock = new();
        SecondFormatLogParser secondFormatLogParser = new();

        bool result = secondFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.True(result);
        Assert.True(logRecord.HasValue);
    }

    [Fact]
    public void TryParse_IncorrectFormat_ReturnsFalseAndSetsLogRecordToNull()
    {
        string logRecordString = "obviously incorrect format";

        LogLevelParserMock logLevelParserMock = new();
        SecondFormatLogParser secondFormatLogParser = new();

        bool result = secondFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.False(result);
        Assert.False(logRecord.HasValue);
    }

    [Fact]
    public void TryParse_CorrectFormatButIncorrectDateValue_ReturnsFalseAndSetsLogRecordToNull()
    {
        string logRecordString = "2025-03-99 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        LogLevelParserMock logLevelParserMock = new();
        SecondFormatLogParser secondFormatLogParser = new();

        bool result = secondFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.False(result);
        Assert.False(logRecord.HasValue);
    }

    [Fact]
    public void TryParse_CorrectFormatButIncorrectTimeValue_ReturnsFalseAndSetsLogRecordToNull()
    {
        string logRecordString = "2025-03-10 99:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        LogLevelParserMock logLevelParserMock = new();
        SecondFormatLogParser secondFormatLogParser = new();

        bool result = secondFormatLogParser.TryParse(logRecordString, logLevelParserMock, out LogRecord? logRecord);

        Assert.False(result);
        Assert.False(logRecord.HasValue);
    }
}