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

namespace Goder.BL.Services
{
    public class UserService : Abstract.BaseService
    {
        public UserService(GoderContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (user == null)
                throw new Exception("User not found");
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(Guid id, UserDTO user)
        {
            User toDbUser = _mapper.Map<User>(user);
            User dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (dbUser == null)
                throw new Exception("User not found");
            toDbUser.Id = dbUser.Id;
            toDbUser.CreatedAt = dbUser.CreatedAt;
            _context.Users.Update(toDbUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(toDbUser);
        }
    }
}
