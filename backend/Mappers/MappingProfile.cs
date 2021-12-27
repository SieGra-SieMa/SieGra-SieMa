using AutoMapper;
using SieGraSieMa.DTOs.IdentityDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(u => u.Name, opt => opt.MapFrom(x => x.Email));
        }
    }
}
