using System;
using System.Collections.Generic;

namespace Goder.BL.DTO.CodeRunning
{
    public class SolutionTestData
    {
        public Guid Id { get; set; }
        public string Script { get; set; }
        public ICollection<TestData> Tests { get; set; }
    }
}
