using AutoMapper;
using Goder.BL.DTO;
using Goder.DAL.Models;

namespace Goder.BL.MappingProfiles
{
    public class SolutionProfile : Profile
    {
        public SolutionProfile()
        {
            CreateMap<SolutionDTO, Solution>().ReverseMap();
            CreateMap<SolutionCreateDTO, Solution>();
        }
    }
}
