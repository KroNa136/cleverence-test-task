using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Formatting;
using Task3.Parsing;

namespace Task3;

/// <summary>
/// The processing pipeline for the standardization of logs.
/// </summary>
/// <param name="logParsers"></param>
public class LogProcessor(IFileHelper fileHelper, ILogLevelParser logLevelParser, ILogFormatter logFormatter, params LogParser[] logParsers)
{
    private readonly IFileHelper _fileHelper = fileHelper;
    private readonly ILogLevelParser _logLevelParser = logLevelParser;
    private readonly ILogFormatter _logFormatter = logFormatter;
    private readonly LogParser[] _logParsers = logParsers;

    /// <summary>
    /// Processes logs from the input file, writes correctly processed records into the output file and problematic records into the problems file.
    /// </summary>
    /// <param name="inputFilePath">The full path to the input file.</param>
    /// <param name="outputFilePath">The full path to the output file.</param>
    /// <param name="problemsFilePath">The full path to the problems file.</param>
    /// <exception cref="FileLoadException">Thrown when one of the specified files cannot be loaded.</exception>
    public void ProcessLogs(string inputFilePath, string outputFilePath, string problemsFilePath)
    {
        List<string> output = [];
        List<string> problems = [];

        try
        {
            foreach (string logRecordString in _fileHelper.ReadLogsFromFile(inputFilePath))
            {
                if (string.IsNullOrEmpty(logRecordString))
                    continue;

                bool success = false;

                foreach (var logParser in _logParsers)
                {
                    try
                    {
                        if (logParser.TryParse(logRecordString, _logLevelParser, out LogRecord? logRecord))
                        {
                            string outputString = _logFormatter.FormatForOutput(logRecord!.Value);
                            output.Add(outputString);

                            success = true;
                            break;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }
                }

                if (!success)
                    problems.Add(logRecordString);
            }
        }
        catch (Exception ex) when (ex is ArgumentException or
            ArgumentNullException or
            FileNotFoundException or
            DirectoryNotFoundException or
            UnauthorizedAccessException or
            IOException)
        {
            throw new FileLoadException($"Failed to load the input file.", inner: ex);
        }

        try
        {
            _fileHelper.WriteLogsToFile(outputFilePath, output);
        }
        catch (Exception ex) when (ex is ArgumentException or
            ArgumentNullException or
            DirectoryNotFoundException or
            UnauthorizedAccessException or
            IOException)
        {
            throw new FileLoadException($"Failed to load the output file.", inner: ex);
        }

        try
        {
            _fileHelper.WriteLogsToFile(problemsFilePath, problems);
        }
        catch (Exception ex) when (ex is ArgumentException or
            ArgumentNullException or
            DirectoryNotFoundException or
            UnauthorizedAccessException or
            IOException)
        {
            throw new FileLoadException($"Failed to load the problems file.", inner: ex);
        }
    }
}
