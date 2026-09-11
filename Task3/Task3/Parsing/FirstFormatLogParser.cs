using System.Globalization;
using System.Text.RegularExpressions;

namespace Task3.Parsing;

/// <summary>
/// A parser for the first log format, e.g. "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'".
/// </summary>
public class FirstFormatLogParser : LogParser
{
    protected override Regex LogFormatRegex => new
    (
        @"^(?<date>\d{2}\.\d{2}\.\d{4})\s+" +
        @"(?<time>\d{2}:\d{2}:\d{2}\.\d{3})\s+" +
        @"(?<level>[A-Z]+)\s+" +
        @"(?<message>.*)$",
        RegexOptions.Compiled
    );

    public override bool TryParse(string logRecordString, ILogLevelParser logLevelParser, out LogRecord? logRecord)
    {
        var match = LogFormatRegex.Match(logRecordString);

        logRecord = null;

        if (!match.Success)
            return false;

        if (!DateOnly.TryParseExact(match.Groups["date"].Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return false;

        if (!TimeOnly.TryParseExact(match.Groups["time"].Value, "HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            return false;

        var logLevel = logLevelParser.Parse(match.Groups["level"].Value);

        logRecord = new
        (
            Date: date,
            Time: time,
            LogLevel: logLevel,
            CallingMethod: null,
            Message: match.Groups["message"].Value
        );

        return true;
    }
}
