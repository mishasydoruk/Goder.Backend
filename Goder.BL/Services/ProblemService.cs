using System;
using System.Collections.Generic;
using System.Text;
using Goder.DAL.Context;
using Goder.DAL.Models;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Goder.BL.DTO;
using Goder.BL.Exceptions;
using Goder.DAL.Enums;

namespace Goder.BL.Services
{
    public class ProblemService : Abstract.BaseService
    {
        public ProblemService(GoderContext context, IMapper mapper) : base(context, mapper)
        { }
        
        public async Task<ProblemDTO> GetProblem(Guid id, string userEmail)
        {
            Problem problem = await _context.Problems
                .Include(problm => problm.Creator)
                .Include(problm => problm.Tests)
                .Include(problm => problm.Solutions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (problem == null)
                throw new NotFoundException("Problem",id.ToString());

            problem.Tests = problem.Tests.Take(1).ToList();

            var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email == userEmail);
            if(user != null)
            {
                problem.Solutions = problem.Solutions.Where(solution => solution.CreatorId == user.Id).ToList();
                return _mapper.Map<ProblemDTO>(problem);
            }

            problem.Solutions = new List<Solution>();
            return _mapper.Map<ProblemDTO>(problem);
        }

        public async Task<ICollection<ProblemSimplifiedDTO>> GetProblems(int skip, int take, string userEmail)
        {
            ICollection<Problem> problems = await _context.Problems
                .Include(problem => problem.Solutions)
                .Skip(skip).Take(take).ToListAsync();
            if (problems.Count == 0)
                throw new NotFoundException("Problems");

            var simplifiedProblems = problems.Select(problem =>
            {
                var simplifiedProblem = _mapper.Map<ProblemSimplifiedDTO>(problem);
                simplifiedProblem.Solved = problem.Solutions.Where(solution => solution.Result == SolutionResults.Succeed).Count();
                simplifiedProblem.Result = 0;

                return simplifiedProblem;
            }).ToList();

            return simplifiedProblems;
        }

        public async Task<ProblemDTO> UpdateProblem(Guid id, ProblemDTO problem)
        {
            Problem toDbProblem = _mapper.Map<Problem>(problem);
            Problem dbProblem = await _context.Problems.FirstOrDefaultAsync(c => c.Id == id);
            if (dbProblem == null)
                throw new NotFoundException("Problem",id.ToString());
            toDbProblem.Id = dbProblem.Id;
            toDbProblem.CreatedAt = dbProblem.CreatedAt;
            _context.Problems.Update(toDbProblem);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProblemDTO>(toDbProblem);
        }
        
        public async Task DeleteProblem(Guid id)
        {
            Problem problem = await _context.Problems.FirstOrDefaultAsync(c => c.Id == id);
            if (problem == null)
                throw new NotFoundException("Problem",id.ToString());
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
        }
        
        
        public async Task<ProblemDTO> CreateProblem(ProblemCreateDTO problemDto, Guid userId)
        {
            Problem problem = _mapper.Map<Problem>(problemDto);
            problem.CreatedAt = DateTime.Now;
            problem.CreatorId = userId;
            await _context.Problems.AddAsync(problem);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProblemDTO>(problem);
        }
    }
}   