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
}

// Tournaments

export interface Tournament {
    id?: number;
    name: string;
    startDate: string,
    endDate: string,
    description: string;
    address: string;

    groups?: Group[];

    ladder?: {
        phases: {
            phase: number;
            matches: {
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
            }[];
        }[];
    }
}

export interface Group {
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
    }[]
}

export interface FacebookFeed {
    data: {
        created_time: string;
        full_picture: string;
        id: string;
        message: string;
        permalink_url: string;
    }[]
    paging: {
        cursor: {
            after: string;
            before: string;
        }
    }
}
};