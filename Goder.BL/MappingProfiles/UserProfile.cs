using AutoMapper;
using Goder.BL.DTO;
using Goder.DAL.Models;
using System;
using System.Linq;

namespace Goder.BL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<FirebaseUserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.AvatarURL, opt => opt.MapFrom(src => src.PhotoURL));
        }
    }
}
