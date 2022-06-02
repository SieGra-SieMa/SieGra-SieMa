using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Match
    {
        public int TournamentId { get; set; }
        public int Phase { get; set; }
        public int MatchId { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public int? TeamHomeScore { get; set; }
        public int? TeamAwayScore { get; set; }
        //public string Referee { get; set; }
        public int? TeamHomeId { get; set; }
        public int? TeamAwayId { get; set; }

        public bool IsMatchPlayed => TeamHomeScore != null && TeamAwayScore != null;
        public bool IsDraw => TeamHomeScore == TeamAwayScore;
        public bool IsHomeTeamWon => TeamHomeScore > TeamAwayScore;
        //public Match nextMatch => Phase == 0 ? null : (new Match { TournamentId = TournamentId, Phase = Phase + 1, MatchId = (int)Math.Ceiling(MatchId / 2.0) });
        public bool IsNextTeamHome => MatchId % 2 == 1;
        public virtual TeamInGroup TGwinner => IsHomeTeamWon ? TeamHome : TeamAway;
        public virtual Team Winner => TGwinner.Team;
        public virtual TeamInGroup TGlooser => !IsHomeTeamWon ? TeamHome : TeamAway;
        public virtual Team Looser => TGlooser.Team;



        public virtual Tournament Tournament { get; set; }
        public virtual TeamInGroup TeamAway { get; set; }
        public virtual TeamInGroup TeamHome { get; set; }
    }
}
