using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3;

/// <summary>
/// A class used to read logs from a file and write logs into a file.
/// </summary>
public class FileHelper : IFileHelper
{
    /// <summary>
    /// Reads log strings from a file one by one.
    /// </summary>
    /// <param name="filePath">The full path to the input log file.</param>
    /// <returns>A collection of strings representing log records from the file.</returns>
    /// <exception cref="ArgumentException">Thrown when the file path is an empty string.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the file path is null.</exception>
    /// <exception cref="FileNotFoundException">Thrown when a file with the specified path cannot be found.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown when one or more directories within the specified path cannot be found.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when a file with the specified path cannot be accessed.</exception>
    /// <exception cref="IOException">Thrown when another input-output error occurs.</exception>
    public IEnumerable<string> ReadLogsFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);

        string? line;

        while ((line = reader.ReadLine()) is not null)
            yield return line;
    }

    /// <summary>
    /// Writes a collection of log-representing strings into a file.
    /// </summary>
    /// <param name="filePath">The full path to the output log file.</param>
    /// <param name="logs">The collection of strings representing log records.</param>
    /// <exception cref="ArgumentException">Thrown when the file path is an empty string.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the file path is null.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown when one or more directories within the specified path cannot be found.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when a file with the specified path cannot be accessed.</exception>
    /// <exception cref="IOException">Thrown when another input-output error occurs.</exception>
    public void WriteLogsToFile(string filePath, IEnumerable<string> logs)
    {
        using var writer = new StreamWriter(filePath);

        foreach (var log in logs)
            writer.WriteLine(log);
    }
}
