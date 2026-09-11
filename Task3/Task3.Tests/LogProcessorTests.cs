using Task3.Formatting;
using Task3.Parsing;
using Task3.Tests.Mocks;

namespace Task3.Tests;

public class LogProcessorTests
{
    #region Mocks

    [Fact]
    public void ProcessLogs_CorrectPathsWithMocks_WritesOutput()
    {
        FileHelperMock fileHelperMock = new();
        LogLevelParserMock logLevelParserMock = new();
        LogFormatterMock logFormatterMock = new();
        LogParserMock logParserMock = new();

        string inputFilePath = "correct_file_path";
        string outputFilePath = "correct_file_path";
        string problemsFilePath = "correct_file_path";

        LogProcessor processor = new(fileHelperMock, logLevelParserMock, logFormatterMock, logParserMock);

        processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath);

        Assert.True(fileHelperMock.LastWrittenLogs.Count > 0);
    }

    [Fact]
    public void ProcessLogs_IncorrectInputFilePathWithMocks_ThrowsException()
    {
        FileHelperMock fileHelperMock = new();
        LogLevelParserMock logLevelParserMock = new();
        LogFormatterMock logFormatterMock = new();
        LogParserMock logParserMock = new();

        string inputFilePath = "";
        string outputFilePath = "correct_file_path";
        string problemsFilePath = "correct_file_path";

        LogProcessor processor = new(fileHelperMock, logLevelParserMock, logFormatterMock, logParserMock);

        Assert.Throws<FileLoadException>(() => processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath));
    }

    [Fact]
    public void ProcessLogs_IncorrectOutputFilePathWithMocks_ThrowsException()
    {
        FileHelperMock fileHelperMock = new();
        LogLevelParserMock logLevelParserMock = new();
        LogFormatterMock logFormatterMock = new();
        LogParserMock logParserMock = new();

        string inputFilePath = "correct_file_path";
        string outputFilePath = "";
        string problemsFilePath = "correct_file_path";

        LogProcessor processor = new(fileHelperMock, logLevelParserMock, logFormatterMock, logParserMock);

        Assert.Throws<FileLoadException>(() => processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath));
    }

    [Fact]
    public void ProcessLogs_IncorrectProblemsFilePathWithMocks_ThrowsException()
    {
        FileHelperMock fileHelperMock = new();
        LogLevelParserMock logLevelParserMock = new();
        LogFormatterMock logFormatterMock = new();
        LogParserMock logParserMock = new();

        string inputFilePath = "correct_file_path";
        string outputFilePath = "correct_file_path";
        string problemsFilePath = "";

        LogProcessor processor = new(fileHelperMock, logLevelParserMock, logFormatterMock, logParserMock);

        Assert.Throws<FileLoadException>(() => processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath));
    }

    #endregion

    #region Real Components

    [Fact]
    public void ProcessLogs_CorrectPaths_WritesCorrectOutputAndProblems()
    {
        FileHelper fileHelper = new();
        LogLevelParser logLevelParser = new();
        LogFormatter logFormatter = new();
        FirstFormatLogParser logParser = new();

        LogProcessor processor = new(fileHelper, logLevelParser, logFormatter, logParser);

        string appDirectory = AppContext.BaseDirectory;
        string inputFilePath = Path.Combine(appDirectory, "input_logs_for_logprocessor_processlogs_correctpaths_test.txt");
        string outputFilePath = Path.Combine(appDirectory, "output_logs.txt");
        string problemsFilePath = Path.Combine(appDirectory, "problems.txt");

        string[] correctLogs =
        [
            "10.03.2025 15:14:49.523 INFORMATION Âåðñèÿ ïðîãðàììû: '3.4.0.48729'",
            "11.04.2026 16:15:50.524 INFORMATION Âåðñèÿ ïðîãðàììû: '3.5.0.48735'"
        ];

        string[] problematicLogs =
        [
            "problematic log 1",
            "problematic log 2"
        ];

        string[] testLogs = [.. correctLogs, .. problematicLogs];

        File.WriteAllLines(inputFilePath, testLogs);

        processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath);

        Assert.True(File.Exists(outputFilePath));
        Assert.True(File.Exists(problemsFilePath));

        string[] writtenOutput = File.ReadAllLines(outputFilePath);
        string[] writtenProblems = File.ReadAllLines(problemsFilePath);

        File.Delete(inputFilePath);
        File.Delete(outputFilePath);
        File.Delete(problemsFilePath);

        Assert.Equal(correctLogs.Length, writtenOutput.Length);
        Assert.Equal(problematicLogs.Length, writtenProblems.Length);

        for (int i = 0; i < correctLogs.Length; i++)
        {
            if (logParser.TryParse(correctLogs[i], logLevelParser, out var logRecord))
            {
                string outputLog = logFormatter.FormatForOutput(logRecord!.Value);
                Assert.Equal(outputLog, writtenOutput[i]);
            }
        }

        for (int i = 0; i < problematicLogs.Length; i++)
            Assert.Equal(problematicLogs[i], writtenProblems[i]);
    }

    public static IEnumerable<object?[]> AddIncorrectInputFilePathData =>
    [
        [ "" ],
        [ null ],
        [ Path.Combine(AppContext.BaseDirectory, "this_file_does_not_exist.txt") ],
        [ "W:\\obviously;incorrect;path\\test.txt" ],
    ];

    [Theory]
    [MemberData(nameof(AddIncorrectInputFilePathData))]
    public void ProcessLogs_IncorrectInputFilePath_ThrowsException(string inputFilePath)
    {
        FileHelper fileHelper = new();
        LogLevelParser logLevelParser = new();
        LogFormatter logFormatter = new();
        FirstFormatLogParser logParser = new();

        LogProcessor processor = new(fileHelper, logLevelParser, logFormatter, logParser);

        string appDirectory = AppContext.BaseDirectory;
        string outputFilePath = Path.Combine(appDirectory, "output_logs.txt");
        string problemsFilePath = Path.Combine(appDirectory, "problems.txt");

        Assert.Throws<FileLoadException>(() => processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath));
    }

    public static IEnumerable<object?[]> AddIncorrectOutputFilePathData =>
    [
        [ "" ],
        [ null ],
        [ "W:\\obviously;incorrect;path" ],
    ];

    [Theory]
    [MemberData(nameof(AddIncorrectOutputFilePathData))]
    public void ProcessLogs_IncorrectOutputFilePath_DoesNotWriteAnythingAndThrowsException(string outputFilePath)
    {
        FileHelper fileHelper = new();
        LogLevelParser logLevelParser = new();
        LogFormatter logFormatter = new();
        FirstFormatLogParser logParser = new();

        LogProcessor processor = new(fileHelper, logLevelParser, logFormatter, logParser);

        string appDirectory = AppContext.BaseDirectory;
        string inputFilePath = Path.Combine(appDirectory, "input_logs_for_logprocessor_processlogs_incorrectoutputfilepath_test.txt");
        string problemsFilePath = Path.Combine(appDirectory, "problems.txt");

        string[] correctLogs =
        [
            "10.03.2025 15:14:49.523 INFORMATION Âåðñèÿ ïðîãðàììû: '3.4.0.48729'",
            "11.04.2026 16:15:50.524 INFORMATION Âåðñèÿ ïðîãðàììû: '3.5.0.48735'"
        ];

        string[] problematicLogs =
        [
            "problematic log 1",
            "problematic log 2"
        ];

        string[] testLogs = [.. correctLogs, .. problematicLogs];

        File.WriteAllLines(inputFilePath, testLogs);

        Assert.Throws<FileLoadException>(() => processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath));
        Assert.False(File.Exists(outputFilePath));
        Assert.False(File.Exists(problemsFilePath));

        File.Delete(inputFilePath);
    }

    [Theory]
    [MemberData(nameof(AddIncorrectOutputFilePathData))]
    public void ProcessLogs_IncorrectProblemsFilePath_WritesOutputThenThrowsException(string problemsFilePath)
    {
        FileHelper fileHelper = new();
        LogLevelParser logLevelParser = new();
        LogFormatter logFormatter = new();
        FirstFormatLogParser logParser = new();

        LogProcessor processor = new(fileHelper, logLevelParser, logFormatter, logParser);

        string appDirectory = AppContext.BaseDirectory;
        string inputFilePath = Path.Combine(appDirectory, "input_logs_for_logprocessor_processlogs_incorrectproblemsfilepath_test.txt");
        string outputFilePath = Path.Combine(appDirectory, "output_logs.txt");

        string[] correctLogs =
        [
            "10.03.2025 15:14:49.523 INFORMATION Âåðñèÿ ïðîãðàììû: '3.4.0.48729'",
            "11.04.2026 16:15:50.524 INFORMATION Âåðñèÿ ïðîãðàììû: '3.5.0.48735'"
        ];

        string[] problematicLogs =
        [
            "problematic log 1",
            "problematic log 2"
        ];

        string[] testLogs = [.. correctLogs, .. problematicLogs];

        File.WriteAllLines(inputFilePath, testLogs);

        Assert.Throws<FileLoadException>(() => processor.ProcessLogs(inputFilePath, outputFilePath, problemsFilePath));
        Assert.True(File.Exists(outputFilePath));
        Assert.False(File.Exists(problemsFilePath));

        string[] writtenOutput = File.ReadAllLines(outputFilePath);

        File.Delete(inputFilePath);
        File.Delete(outputFilePath);

        Assert.Equal(correctLogs.Length, writtenOutput.Length);

        for (int i = 0; i < correctLogs.Length; i++)
        {
            if (logParser.TryParse(correctLogs[i], logLevelParser, out var logRecord))
            {
                string outputLog = logFormatter.FormatForOutput(logRecord!.Value);
                Assert.Equal(outputLog, writtenOutput[i]);
            }
        }
    }

    #endregion
}
