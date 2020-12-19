using System;
using System.Collections.Generic;
using System.Text;
using Goder.DAL.Models;

namespace Goder.BL.DTO
{
    public class ProblemCreateDTO
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        
        public ICollection<Test> Tests { get; set; }
    }
}