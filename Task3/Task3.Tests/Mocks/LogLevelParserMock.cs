using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Parsing;

namespace Task3.Tests.Mocks;

internal class LogLevelParserMock : ILogLevelParser
{
    public LogLevel Parse(string logLevelString) => LogLevel.Info;
}
