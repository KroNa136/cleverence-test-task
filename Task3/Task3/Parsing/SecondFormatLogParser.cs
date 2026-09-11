using System.Globalization;
using System.Text.RegularExpressions;

namespace Task3.Parsing;

/// <summary>
/// A parser for the second log format, e.g. "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'".
/// </summary>
public class SecondFormatLogParser : LogParser
{
    protected override Regex LogFormatRegex => new
    (
        @"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
        @"(?<time>\d{2}:\d{2}:\d{2}\.\d{4})\|" +
        @"\s*(?<level>[A-Za-z]+)\|" +
        @"\s*(?<id>\d+)\|" +
        @"(?<method>[^|]+)\|" +
        @"\s*(?<message>.*)$",
        RegexOptions.Compiled
    );

    public override bool TryParse(string logRecordString, ILogLevelParser logLevelParser, out LogRecord? logRecord)
    {
        var match = LogFormatRegex.Match(logRecordString);

        logRecord = null;

        if (!match.Success)
            return false;

        if (!DateOnly.TryParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return false;

        if (!TimeOnly.TryParseExact(match.Groups["time"].Value, "HH:mm:ss.ffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            return false;

        var logLevel = logLevelParser.Parse(match.Groups["level"].Value);

        logRecord = new
        (
            Date: date,
            Time: time,
            LogLevel: logLevel,
            CallingMethod: match.Groups["method"].Value,
            Message: match.Groups["message"].Value
        );

        return true;
    }
}
