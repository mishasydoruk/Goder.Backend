using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.TestRunner.Models
{
    public class ProblemTestsData
    {
        public Guid Id { get; set; }
        public string Script { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
