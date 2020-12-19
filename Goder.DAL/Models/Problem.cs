using System;
using System.Collections.Generic;

namespace Goder.DAL.Models
{
    public class Problem
    {
        public Guid Id { get; set;  }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }

        public Guid CreatorId { get; set;  }
        public User Creator { get; set; } 

        public ICollection<Test> Tests { get; set; }
        public ICollection<Solution> Solutions { get; set; }

    }
}