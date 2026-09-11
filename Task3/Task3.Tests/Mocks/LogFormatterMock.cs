using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Formatting;

namespace Task3.Tests.Mocks;

internal class LogFormatterMock : ILogFormatter
{
    public string FormatForOutput(LogRecord logRecord) => "correct_output_format";
}
