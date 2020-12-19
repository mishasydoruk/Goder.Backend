using Goder.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.BL.DTO
{
    public class SolutionDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public SolutionResults Result { get; set; }
        public int ExecutionTime { get; set; }
        public int ExecutionMemory { get; set; }
        public int LastExecutedTest { get; set; }
        public string Script { get; set; }

        public Guid CreatorId { get; set; }
        public UserDTO Creator { get; set; }

        public Guid ProblemId { get; set; }
        public ProblemDTO Problem { get; set; }
    }
}
