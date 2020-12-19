using System;

namespace Goder.BL.DTO
{
    public class SolutionCreateDTO
    {
        public string Script { get; set; }
        public Guid CreatorId { get; set; }
        public Guid ProblemId { get; set; }
    }
}
