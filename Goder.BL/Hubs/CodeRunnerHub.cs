using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Goder.BL.DTO;
using Goder.DAL.Context;
using Goder.DAL.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace Goder.BL.Hubs
{
    public sealed class CodeRunnerHub: Hub
    {
        private readonly GoderContext _ctx;

        public CodeRunnerHub(GoderContext context)
        {
            _ctx = context;
        }

        [HubMethodName("ProblemResponse")]
        public async Task SaveTestResult(TestResult result)
        {
            var solution = await _ctx.Solutions.FirstOrDefaultAsync(c => c.Id == result.Id);
            solution.Result = result.IsPassed ? SolutionResults.Succeed : SolutionResults.WrongResult;
            _ctx.Solutions.Update(solution);
            await _ctx.SaveChangesAsync();
        }
    }
}
