﻿using SieGraSieMa.DTOs.TeamsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.TournamentDTO
{
    public class TournamentListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsOpen { get; set; }
        public GetTeamsDTO Team { get; set; }
    }
}
