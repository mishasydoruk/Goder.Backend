using System;
using System.Collections.Generic;
using System.Text;
using Goder.DAL.Models;

namespace Goder.BL.DTO
{
    public class ProblemCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        
        public ICollection<TestDTO> Tests { get; set; }
    }
}