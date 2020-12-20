using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.BL.DTO
{
    public class ProblemSimplifiedDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Result { get; set; }
        public int Solved { get; set; }
    }
}
