using Task3;
using Task3.Formatting;
using Task3.Parsing;

string inputFilePath = string.Empty;

while (string.IsNullOrEmpty(inputFilePath))
{
    Console.Write("Enter the path to the input file: ");
    inputFilePath = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(inputFilePath))
        Console.WriteLine("The path must not be empty!");

    Console.WriteLine();
}

string outputDirectoryPath = string.Empty;

while (string.IsNullOrEmpty(outputDirectoryPath))
{
    Console.Write("Enter the path to the output directory: ");
    outputDirectoryPath = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(outputDirectoryPath))
        Console.WriteLine("The path must not be empty!");

    Console.WriteLine();
}

string outputFilePath = Path.Combine(outputDirectoryPath, "output_logs.txt");
string problemsFilePath = Path.Combine(outputDirectoryPath, "problems.txt");

FileHelper fileHelper = new();
LogLevelParser logLevelParser = new();
LogFormatter logFormatter = new();
FirstFormatLogParser firstFormatLogParser = new();
SecondFormatLogParser secondFormatLogParser = new();

LogProcessor processor = new(fileHelper, logLevelParser, logFormatter, firstFormatLogParser, secondFormatLogParser);

try
{
    processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath);
}
catch (FileLoadException ex)
{
    Console.WriteLine(ex.Message);

    if (ex.InnerException is not null)
    {
        string? innerMessage = ex.InnerException switch
        {
            ArgumentNullException => "The specified path was null.",
            ArgumentException => "The specified path was an empty string.",
            FileNotFoundException => "A file with the specified path cannot be found.",
            DirectoryNotFoundException => "A directory with the specified path cannot be found.",
            PathTooLongException => "The specified path exceeds the system character limit.",
            IOException => $"An input/output error occured: {ex.InnerException.Message}",
            UnauthorizedAccessException => "A file with the specified path cannot be accessed.",
            _ => ex.InnerException.Message
        };

        Console.WriteLine(innerMessage);
    }
}
