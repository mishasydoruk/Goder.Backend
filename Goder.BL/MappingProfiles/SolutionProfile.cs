using AutoMapper;
using Goder.BL.DTO;
using Goder.DAL.Models;
using System;

namespace Goder.BL.MappingProfiles
{
    public class SolutionProfile : Profile
    {
        public SolutionProfile()
        {
            CreateMap<SolutionDTO, Solution>().ReverseMap();
            CreateMap<SolutionCreateDTO, Solution>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
