using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.TestRunner.Configuartions
{
    public class ProcessOptions
    {
        public string WorkingDirectory { get; set; }
        public string PYTHONPATH { get; set; }
        public string PYTHONHOME { get; set; }
        public string FileName { get; set; }
    }
}
