using AutoMapper;
using Goder.BL.DTO;
using Goder.BL.Services.Abstract;
using Goder.DAL.Context;
using Goder.DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Goder.BL.Services
{
    public class RegistrationService : BaseService
    {
        public RegistrationService(GoderContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task<UserDTO> RegisterUser(FirebaseUserDTO firebaseUser)
        {
            var user = _context.Users.FirstOrDefault(usr => usr.Email == firebaseUser.Email);

            if (user == null)
            {
                user = _mapper.Map<User>(firebaseUser);

                var splitedName = firebaseUser.DisplayName.Split(' ').ToArray();

                user.FirstName = splitedName[0];
                user.LastName = splitedName.Length < 2 ? "" : splitedName[1];

                user.CreatedAt = DateTime.Now;

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

            }
            return _mapper.Map<UserDTO>(user);
        }
    }
}
