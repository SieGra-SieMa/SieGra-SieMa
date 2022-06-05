export type TournamentRequest = {
    name: string;
    startDate: string,
    endDate: string,
    address: string;
};

export type TournamentList = {
    id: number;
    name: string;
    startDate: string,
    endDate: string,
    address: string;
};

export type Tournament = {
    id: number;
    name: string;
    startDate: string,
    endDate: string,
    address: string;
    groups: Group[];
    ladder: Ladder;
};

export type Group = {
    id: number;
    name: string;
    tournamentId: number;
    ladder: boolean;

    teams: {
        name: string;
        playedMatches: number;
        wonMatches: number;
        tiedMatches: number;
        lostMatches: number;
        goalScored: number;
        goalConceded: number;
        points: number;
    }[];
};

export type Match = {
    tournamentId: number;
    phase: number
    matchId: number;
    teamHome: string;
    teamAway: string;
    teamHomeScore: number | null;
    teamAwayScore: number | null;
};

export type Phase = {
    phase: number;
    matches: Match[];
};

export type Ladder = {
    phases: Phase[];
};

export type MatchResult = {
    tournamentId: number;
    phase: number;
    matchId: number;
    homeTeamPoints: number;
    awayTeamPoints: number;
}
