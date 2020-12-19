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

namespace Goder.BL.Services
{
    public class ProblemService : Abstract.BaseService
    {
        public ProblemService(GoderContext context, IMapper mapper) : base(context, mapper)
        {

        }
        
        public async Task<ProblemDTO> GetProblem(Guid id)
        {
            Problem problem = await _context.Problems.FirstOrDefaultAsync(c => c.Id == id);
            if (problem == null)
                throw new NotFoundException("Problem",id.ToString());
            return _mapper.Map<ProblemDTO>(problem);
        }
        
        public async Task<ICollection<ProblemDTO>> GetProblems(int skip, int take)
        {
            ICollection<Problem> problems = await _context.Problems.Skip(skip).Take(take).ToListAsync();
            if (problems.Count==0)
                throw new NotFoundException("Problems");
            return _mapper.Map<ICollection<ProblemDTO>>(problems);
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
        
        
        public async Task<ProblemDTO> CreateProblem(ProblemDTO problemDto, Guid userId)
        {
            Problem problem = _mapper.Map<Problem>(problemDto);
            problem.CreatedAt = DateTime.Now;
            problem.CreatorId = userId;
            problem.Creator = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
            await _context.Problems.AddAsync(problem);
            return _mapper.Map<ProblemDTO>(problem);
        }
        
    }
}