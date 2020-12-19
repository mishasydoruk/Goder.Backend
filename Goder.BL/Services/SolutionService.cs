using AutoMapper;
using Goder.BL.DTO;
using Goder.BL.Exceptions;
using Goder.BL.Providers;
using Goder.DAL.Context;
using Goder.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Goder.BL.Services
{
    public class SolutionService : Abstract.BaseService
    {
        private readonly CodeRunnerProvider _codeRunnerProvider;
        public SolutionService(GoderContext context, IMapper mapper, CodeRunnerProvider codeRunnerProvider) : base(context, mapper)
        {
            _codeRunnerProvider = codeRunnerProvider;
        }

        public async Task AddSolution(SolutionCreateDTO solution)
        {
            var newSolution = _mapper.Map<Solution>(solution);
            var dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == solution.CreatorId);
            if (dbUser == null)
                throw new NotFoundException(nameof(User), solution.CreatorId.ToString());
            newSolution.CreatedAt = DateTimeOffset.Now;
            _context.Solutions.Add(newSolution);
            await _context.SaveChangesAsync();
            var dbSolution = await _context.Solutions
                .Include(solve => solve.Problem)
                    .ThenInclude(problem => problem.Tests)
                .FirstOrDefaultAsync(c => c.Id == newSolution.Id);
            var solutionDTO = _mapper.Map<SolutionDTO>(dbSolution);
            _codeRunnerProvider.SendCodeToExecute(solutionDTO);
        }
    }
}
