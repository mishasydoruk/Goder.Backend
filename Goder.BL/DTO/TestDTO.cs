using Goder.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.BL.DTO
{
    public class TestDTO
    {
        public Guid Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public Guid ProblemId { get; set; }
        public ProblemDTO Problem { get; set; }
    }
}
