using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3;

public interface IFileHelper
{
    public IEnumerable<string> ReadLogsFromFile(string filePath);
    public void WriteLogsToFile(string filePath, IEnumerable<string> logs);
}
