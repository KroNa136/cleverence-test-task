namespace Task3.Parsing;

/// <summary>
/// A parser of log level string representations to corresponding enumerated values.
/// </summary>
public class LogLevelParser : ILogLevelParser
{
    /// <summary>
    /// Parses a string representation of a log level to the corresponding <see cref="LogLevel"/> value.
    /// </summary>
    /// <param name="logLevelString">The string representation of a log level.</param>
    /// <returns>The corresponding <see cref="LogLevel"/> value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the method encounters an unknown log level string representation.</exception>
    public LogLevel Parse(string logLevelString) => logLevelString switch
    {
        "INFORMATION" or "INFO" => LogLevel.Info,
        "WARNING" or "WARN" => LogLevel.Warning,
        "ERROR" => LogLevel.Error,
        "DEBUG" => LogLevel.Debug,
        _ => throw new InvalidOperationException("Unsupported log level value.")
    };
}
