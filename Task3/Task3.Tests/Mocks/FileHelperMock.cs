using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Tests.Mocks;

internal class FileHelperMock : IFileHelper
{
    public List<string> LastWrittenLogs { get; set; } = [];

    public IEnumerable<string> ReadLogsFromFile(string filePath) => filePath switch
    {
        "correct_file_path" => ["correct_format", "problematic_format"],
        "" => throw new ArgumentException("Empty file path."),
        null => throw new ArgumentNullException(nameof(filePath)),
        "correct_directory_incorrect_file_name" => throw new FileNotFoundException("File not found."),
        "incorrect_directory" => throw new DirectoryNotFoundException("Directory not found"),
        "unauthorized_access" => throw new UnauthorizedAccessException("File cannot be accessed"),
        _ => throw new IOException("Unknown IO error.")
    };

    public void WriteLogsToFile(string filePath, IEnumerable<string> logs)
    {
        LastWrittenLogs = filePath switch
        {
            "correct_file_path" => ["log1", "log2"],
            "" => throw new ArgumentException("Empty file path."),
            null => throw new ArgumentNullException(nameof(filePath)),
            "correct_directory_incorrect_file_name" => throw new FileNotFoundException("File not found."),
            "incorrect_directory" => throw new DirectoryNotFoundException("Directory not found"),
            "unauthorized_access" => throw new UnauthorizedAccessException("File cannot be accessed"),
            _ => throw new IOException("Unknown IO error."),
        };
    }
}
