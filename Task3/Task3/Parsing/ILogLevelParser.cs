using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Parsing;

public interface ILogLevelParser
{
    public LogLevel Parse(string logLevelString);
}
