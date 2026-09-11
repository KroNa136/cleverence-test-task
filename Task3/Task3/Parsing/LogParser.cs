using System.Text.RegularExpressions;

namespace Task3.Parsing;

public abstract class LogParser
{
    protected abstract Regex LogFormatRegex { get; }

    /// <summary>
    /// Tries to parse a string value from logs to a <see cref="LogRecord"/> object.
    /// </summary>
    /// <param name="logRecordString">A string value representing a log record.</param>
    /// <param name="logLevelParser">An instance of the <see cref="LogLevelParser"/> class used to process the log level.</param>
    /// <param name="logRecord">The resulting <see cref="LogRecord"/> object, if the parsing was successful.</param>
    /// <returns>Whether the parsing was successful or not.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the method encounters an unknown log level string representation.</exception>
    public abstract bool TryParse(string logRecordString, ILogLevelParser logLevelParser, out LogRecord? logRecord);
}
