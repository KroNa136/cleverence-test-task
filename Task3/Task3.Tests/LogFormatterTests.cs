using Task3.Formatting;

namespace Task3.Tests;

public class LogFormatterTests
{
    public static IEnumerable<object?[]> AddTestData =>
    [
        [ new DateOnly(2026, 09, 10), new TimeOnly(16, 30, 45, 123, 400), LogLevel.Info, "TestClass.TestMethod", "Some message",
            "10-09-2026\t16:30:45.1234\tINFO\tTestClass.TestMethod\tSome message" ],
        [ new DateOnly(2026, 09, 11), new TimeOnly(16, 30, 46, 123), LogLevel.Warning, "TestClass.TestMethod", "Some message",
            "11-09-2026\t16:30:46.123\tWARN\tTestClass.TestMethod\tSome message" ],
        [ new DateOnly(2026, 09, 12), new TimeOnly(16, 30, 47, 123, 321), LogLevel.Error, "TestClass.TestMethod", "Some message",
            "12-09-2026\t16:30:47.123321\tERROR\tTestClass.TestMethod\tSome message" ],
        [ new DateOnly(2026, 09, 13), new TimeOnly(16, 30, 48, 456), LogLevel.Debug, "TestClass.TestMethod", "Some message",
            "13-09-2026\t16:30:48.456\tDEBUG\tTestClass.TestMethod\tSome message" ],
        [ new DateOnly(2026, 09, 14), new TimeOnly(16, 30, 49, 789), LogLevel.Info, null, "Some message",
            "14-09-2026\t16:30:49.789\tINFO\tDEFAULT\tSome message" ],
    ];

    [Theory]
    [MemberData(nameof(AddTestData))]
    public void FormatForOutput_ReturnsCorrectlyFormattedString(DateOnly date, TimeOnly time, LogLevel logLevel, string? callingMethod, string message, string expectedResult)
    {
        LogRecord logRecord = new(date, time, logLevel, callingMethod, message);

        LogFormatter logFormatter = new();
        string result = logFormatter.FormatForOutput(logRecord);

        Assert.Equal(expectedResult, result);
    }

    /*
    [Fact]
    public void FormatForOutput_InfoLogRecord_ReturnsCorrectlyFormattedString()
    {
        string callingMethod = "TestClass.TestMethod";
        string message = "Some message";

        LogRecord logRecord = new
        (
            Date: new DateOnly(2026, 09, 10),
            Time: new TimeOnly(16, 30, 45, 123, 400),
            LogLevel: LogLevel.Info,
            CallingMethod: callingMethod,
            Message: message
        );

        LogFormatter logFormatter = new();
        string result = logFormatter.FormatForOutput(logRecord);

        Assert.Equal($"10-09-2026\t16:30:45.1234\tINFO\t{callingMethod}\t{message}", result);
    }

    [Fact]
    public void FormatForOutput_WarningLogRecord_ReturnsCorrectlyFormattedString()
    {
        string callingMethod = "TestClass.TestMethod";
        string message = "Some message";

        LogRecord logRecord = new
        (
            Date: new DateOnly(2026, 09, 10),
            Time: new TimeOnly(16, 30, 45, 123, 400),
            LogLevel: LogLevel.Warning,
            CallingMethod: callingMethod,
            Message: message
        );

        LogFormatter logFormatter = new();
        string result = logFormatter.FormatForOutput(logRecord);

        Assert.Equal($"10-09-2026\t16:30:45.1234\tWARN\t{callingMethod}\t{message}", result);
    }

    [Fact]
    public void FormatForOutput_ErrorLogRecord_ReturnsCorrectlyFormattedString()
    {
        string callingMethod = "TestClass.TestMethod";
        string message = "Some message";

        LogRecord logRecord = new
        (
            Date: new DateOnly(2026, 09, 10),
            Time: new TimeOnly(16, 30, 45, 123, 400),
            LogLevel: LogLevel.Error,
            CallingMethod: callingMethod,
            Message: message
        );

        LogFormatter logFormatter = new();
        string result = logFormatter.FormatForOutput(logRecord);

        Assert.Equal($"10-09-2026\t16:30:45.1234\tERROR\t{callingMethod}\t{message}", result);
    }

    [Fact]
    public void FormatForOutput_DebugLogRecord_ReturnsCorrectlyFormattedString()
    {
        string callingMethod = "TestClass.TestMethod";
        string message = "Some message";

        LogRecord logRecord = new
        (
            Date: new DateOnly(2026, 09, 10),
            Time: new TimeOnly(16, 30, 45, 123, 400),
            LogLevel: LogLevel.Debug,
            CallingMethod: callingMethod,
            Message: message
        );

        LogFormatter logFormatter = new();
        string result = logFormatter.FormatForOutput(logRecord);

        Assert.Equal($"10-09-2026\t16:30:45.1234\tDEBUG\t{callingMethod}\t{message}", result);
    }

    [Fact]
    public void FormatForOutput_NullCallingMethod_ReturnsCorrectlyFormattedString()
    {
        string message = "Some message";

        LogRecord logRecord = new
        (
            Date: new DateOnly(2026, 09, 10),
            Time: new TimeOnly(16, 30, 45, 123, 400),
            LogLevel: LogLevel.Info,
            CallingMethod: null,
            Message: message
        );

        LogFormatter logFormatter = new();
        string result = logFormatter.FormatForOutput(logRecord);

        Assert.Equal($"10-09-2026\t16:30:45.1234\tINFO\tDEFAULT\t{message}", result);
    }
    */
}
