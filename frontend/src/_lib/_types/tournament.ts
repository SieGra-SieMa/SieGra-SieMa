export type TournamentRequest = {
    name: string;
    startDate: string,
    endDate: string,
    address: string;
};

export type TournamentListItem = {
    id: number;
    name: string;
    startDate: string,
    endDate: string,
    address: string;
    profilePicture: string;
    status: boolean;
};

export type Tournament = {
    id: number;
    name: string;
    startDate: string,
    endDate: string,
    address: string;
    profilePicture: string;
    status: boolean;
    groups: Group[];
    ladder: Phase[];
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

export type MatchResult = {
    tournamentId: number;
    phase: number;
    matchId: number;
    homeTeamPoints: number;
    awayTeamPoints: number;
}
