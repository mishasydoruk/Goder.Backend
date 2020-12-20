using System;
using System.Collections.Generic;
using System.Text;
using Goder.DAL.Models;

namespace Goder.BL.DTO
{
    public class ProblemDTO
    {
        public Guid Id { get; set;  }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        
        public Guid CreatorId { get; set;  }
        public UserDTO Creator { get; set; } 

        public ICollection<TestDTO> Tests { get; set; }
        public ICollection<SolutionDTO> Solutions { get; set; }
    }
}