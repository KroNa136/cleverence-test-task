using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task3.Parsing;

namespace Task3.Tests.Mocks;

internal class LogParserMock : LogParser
{
    protected override Regex LogFormatRegex => null!;

    public override bool TryParse(string logRecordString, ILogLevelParser logLevelParser, out LogRecord? logRecord)
    {
        if (logRecordString.Equals("correct_format"))
        {
            logRecord = new();
            return true;
        }
        else
        {
            logRecord = null;
            return false;
        }
    }
}
