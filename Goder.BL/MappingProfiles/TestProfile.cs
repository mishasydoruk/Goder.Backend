using AutoMapper;
using Goder.BL.DTO;
using Goder.BL.DTO.CodeRunning;
using Goder.DAL.Models;

namespace Goder.BL.MappingProfiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<TestDTO, Test>().ReverseMap();
            CreateMap<TestData, TestDTO>().ReverseMap();
        }
    }
}
