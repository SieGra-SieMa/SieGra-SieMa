import { ROLES } from './roles';

export type User = {
    id: number;
    name: string;
    surname: string;
    roles: ROLES[];
};

export type Account = User & {
    email: string;
    accessToken: string;
    refreshToken: string;
};

export type Player = User;

export type Team = {
    id: number;
    name: string;
    code: string;
    captainId: number;
    players: Player[];
};

export type UserDetailsRequest = {
    name: string;
    surname: string;
};

export type Tokens = {
    token: string;
    refreshToken: string;
};

export type Session = {
    accessToken: string;
    refreshToken: string;
};

// Tournaments

export type Tournament = {
    id?: number;
    name: string;
    startDate: string,
    endDate: string,
    description: string;
    address: string;

    groups?: Group[];

    ladder?: Ladder
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
    teamHome: {
        name: string;
    }
    teamAway: {
        name: string;
    }
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