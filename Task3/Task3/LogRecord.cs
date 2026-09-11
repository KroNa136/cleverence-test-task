namespace Task3;

public record struct LogRecord(DateOnly Date, TimeOnly Time, LogLevel LogLevel, string? CallingMethod, string Message);
