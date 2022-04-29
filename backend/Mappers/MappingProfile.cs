using AutoMapper;
using SieGraSieMa.DTOs;
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
            /*CreateMap<TeamDTO, Team>()
                .ForMember(u => u.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<Team, TeamDTO>();*/
            /*CreateMap<PlayerDTO, Player>()
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(x => x.Id))
                .ForPath(dest => dest.User.Name, opt => opt.MapFrom(x => x.Name))
                .ForPath(dest => dest.User.Surname, opt => opt.MapFrom(x => x.Surname));*/
            //CreateMap<PlayerDTO, User>();
            //CreateMap<List<PlayerDTO>, List<Player>>();
            //CreateMap<List<Player> ,List<PlayerDTO>>();
            //CreateMap<Player, PlayerDTO>();
        }
    }
}
