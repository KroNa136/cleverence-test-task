namespace Task3.Tests;

public class FileHelperTests
{
    #region ReadLogsFromFile

    [Fact]
    public void ReadLogsFromFile_CorrectPath_ReadsLogsCorrectly()
    {
        string appDirectory = AppContext.BaseDirectory;
        string inputFilePath = Path.Combine(appDirectory, "input_logs_for_filehelper_readlogs_correctpaths_test.txt");

        string[] testLogs =
        [
            "qwerty",
            "1234",
            "ABCDE"
        ];

        File.WriteAllLines(inputFilePath, testLogs);

        FileHelper fileHelper = new();
        string[] readLogs = [.. fileHelper.ReadLogsFromFile(inputFilePath)];

        File.Delete(inputFilePath);

        Assert.Equal(testLogs.Length, readLogs.Length);

        for (int i = 0; i < testLogs.Length; i++)
            Assert.Equal(testLogs[i], readLogs[i]);
    }

    [Fact]
    public void ReadLogsFromFile_NullPath_ThrowsException()
    {
        string? inputFilePath = null;
        FileHelper fileHelper = new();

        var logs = fileHelper.ReadLogsFromFile(inputFilePath!);
        Assert.Throws<ArgumentNullException>(() => logs.Count());
    }

    [Fact]
    public void ReadLogsFromFile_EmptyPath_ThrowsException()
    {
        string inputFilePath = "";
        FileHelper fileHelper = new();

        var logs = fileHelper.ReadLogsFromFile(inputFilePath);
        Assert.Throws<ArgumentException>(() => logs.Count());
    }

    [Fact]
    public void ReadLogsFromFile_CorrectDirectoryIncorrectFileName_ThrowsException()
    {
        string appDirectory = AppContext.BaseDirectory;
        string inputFilePath = Path.Combine(appDirectory, "this_file_does_not_exist.txt");

        FileHelper fileHelper = new();

        var logs = fileHelper.ReadLogsFromFile(inputFilePath);
        Assert.Throws<FileNotFoundException>(() => logs.Count());
    }

    [Fact]
    public void ReadLogsFromFile_IncorrectPath_ThrowsException()
    {
        string inputFilePath = "W:\\obviously;incorrect;path\\test.txt";
        FileHelper fileHelper = new();

        var logs = fileHelper.ReadLogsFromFile(inputFilePath);
        Assert.Throws<DirectoryNotFoundException>(() => logs.Count());
    }

    #endregion

    #region WriteLogsToFile

    [Fact]
    public void WriteLogsToFile_CorrectPath_ReadsLogsCorrectly()
    {
        string appDirectory = AppContext.BaseDirectory;
        string outputFilePath = Path.Combine(appDirectory, "output_logs.txt");

        string[] testLogs =
        [
            "qwerty",
            "1234",
            "ABCDE"
        ];

        FileHelper fileHelper = new();
        fileHelper.WriteLogsToFile(outputFilePath, testLogs);

        Assert.True(File.Exists(outputFilePath));

        string[] writtenLogs = File.ReadAllLines(outputFilePath);

        File.Delete(outputFilePath);

        Assert.Equal(testLogs.Length, writtenLogs.Length);

        for (int i = 0; i < testLogs.Length; i++)
            Assert.Equal(testLogs[i], writtenLogs[i]);
    }

    [Fact]
    public void WriteLogsToFile_NullPath_ThrowsException()
    {
        string? outputFilePath = null;
        FileHelper fileHelper = new();

        Assert.Throws<ArgumentNullException>(() => fileHelper.WriteLogsToFile(outputFilePath!, []));
    }

    [Fact]
    public void WriteLogsToFile_EmptyPath_ThrowsException()
    {
        string outputFilePath = "";
        FileHelper fileHelper = new();

        Assert.Throws<ArgumentException>(() => fileHelper.WriteLogsToFile(outputFilePath, []));
    }

    [Fact]
    public void WriteLogsToFile_IncorrectPath_ThrowsException()
    {
        string outputFilePath = "W:\\obviously;incorrect;path";
        FileHelper fileHelper = new();

        Assert.Throws<DirectoryNotFoundException>(() => fileHelper.WriteLogsToFile(outputFilePath, []));
    }

    #endregion
}
