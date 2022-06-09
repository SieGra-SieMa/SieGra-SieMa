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

export interface TournamentWithAlbums {
    id?: number;
    name: string;
    startDate: string,
    endDate: string,
    description: string;
    address: string;
    profilePicture?: string;

    albums?: Album[];
}

export interface Album {
    id?: number;
    name: string;
    createDate: string;
    profilePicture?: string;
    mediaList?: Media[];
    tournamentId?: string;
}

export interface Media {
    id?: number;
    url: string;
}

