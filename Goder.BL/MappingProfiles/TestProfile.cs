using AutoMapper;
using Goder.BL.DTO;
using Goder.BL.DTO.CodeRunning;
using Goder.DAL.Models;
using System;

namespace Goder.BL.MappingProfiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<TestDTO, Test>().ReverseMap();
            CreateMap<TestData, TestDTO>().ReverseMap();
            CreateMap<TestCreateDTO, Test>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
