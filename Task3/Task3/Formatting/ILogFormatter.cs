using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Formatting;

public interface ILogFormatter
{
    public string FormatForOutput(LogRecord logRecord);
}
