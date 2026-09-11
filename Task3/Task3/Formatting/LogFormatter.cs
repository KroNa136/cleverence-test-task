using System.Text;

namespace Task3.Formatting;

/// <summary>
/// A formatter for <see cref="LogRecord"/> objects.
/// </summary>
public class LogFormatter : ILogFormatter
{
    /// <summary>
    /// Transforms a <see cref="LogRecord"/> object into a string in the output format.
    /// </summary>
    /// <param name="logRecord">The original <see cref="LogRecord"/> object.</param>
    /// <returns>The string representation of the log record, formatted according to the output format.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the method encounters an unknown <see cref="LogLevel"/> value in the record.</exception>
    public string FormatForOutput(LogRecord logRecord)
    {
        StringBuilder sb = new();

        string separator = "\t";

        sb.Append(logRecord.Date.ToString("dd-MM-yyyy"));
        sb.Append(separator);

        sb.Append(logRecord.Time.ToString("HH:mm:ss.fffffff").TrimEnd('0'));
        sb.Append(separator);

        string logLevel = logRecord.LogLevel switch
        {
            LogLevel.Info => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "ERROR",
            LogLevel.Debug => "DEBUG",
            _ => throw new InvalidOperationException("Unsupported log level value.")
        };

        sb.Append(logLevel);
        sb.Append(separator);

        string callingMethod = string.IsNullOrEmpty(logRecord.CallingMethod) ? "DEFAULT" : logRecord.CallingMethod;

        sb.Append(callingMethod);
        sb.Append(separator);

        sb.Append(logRecord.Message);

        return sb.ToString();
    }
}
