using System;
using System.Collections.Generic;
using System.Text;
using Goder.DAL.Context;
using Goder.DAL.Models;
using AutoMapper;
using System.Linq;

namespace Goder.BL.Services
{
    public class UserService : Abstract.BaseService
    {
        public UserService(GoderContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public User GetUser(Guid id)
        {
            return _context.Users.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateUser(Guid id, User user)
        {
            User dbUser = _context.Users.FirstOrDefault(c => c.Id == id);
            dbUser.AvatarURL = user.AvatarURL;
            dbUser.Birthday = user.Birthday;
            dbUser.CreatedProblems = user.CreatedProblems;
            dbUser.CreatedSolutions = user.CreatedSolutions;
            dbUser.Email = user.Email;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
        }
    }
}
