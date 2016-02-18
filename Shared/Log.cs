using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Log
    {
        public int Id { get; set; }
        public LogMessageType MessageType { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
