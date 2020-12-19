﻿using AutoMapper;
using Goder.BL.DTO;
using Goder.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.BL.MappingProfiles
{
    public class ProblemProfile : Profile
    {
        public ProblemProfile()
        {
            CreateMap<ProblemDTO, Problem>().ReverseMap();
            CreateMap<ProblemCreateDTO, Problem>().ReverseMap();
        }
    }
}
