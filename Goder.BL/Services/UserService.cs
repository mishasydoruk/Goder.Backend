using System;
using System.Collections.Generic;
using System.Text;
using Goder.DAL.Context;
using Goder.DAL.Models;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Goder.BL.DTO;
using Goder.BL.Exceptions;
using Goder.BL.Helpers;

namespace Goder.BL.Services
{
    public class UserService : Abstract.BaseService
    {
        public UserService(GoderContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task<UserDTO> GetUser(Guid id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (user == null)
                throw new NotFoundException("User",id.ToString());
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserAvatar(Guid id, IFormFile photo)
        {
            User dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            string photoAsBase64String = await ImageHelper.ConvertImageToBase64String(photo);
            if (dbUser == null)
                throw new NotFoundException("User",id.ToString());
            dbUser.AvatarURL = photoAsBase64String;
            _context.Users.Update(dbUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(dbUser);
        }

        public async Task<UserDTO> UpdateUser(Guid id, UserDTO user)
        {
            User toDbUser = _mapper.Map<User>(user);
            User dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (dbUser == null)
                throw new NotFoundException("User",id.ToString());
            toDbUser.Id = dbUser.Id;
            toDbUser.CreatedAt = dbUser.CreatedAt;
            _context.Users.Update(toDbUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(toDbUser);
        }
    }
}
