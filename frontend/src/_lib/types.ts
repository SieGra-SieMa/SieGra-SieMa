import { ROLES } from './roles';

export interface User {
    name: string;
    surname: string;
}

export interface Account extends User {
    email: string;
    accessToken: string;
    refreshToken: string;
}

export interface Player {
    user: User;
}

export interface Team {
    id: number;
    name: string;
    code: string;
    players: Player[];
}

export interface UserDetailsRequest {
    name: string;
    surname: string;
}

export interface Tokens {
    token: string;
    refreshToken: string;
}

export interface Session {
    accessToken: string;
    refreshToken: string;
    role: ROLES;
}

// Tournaments

export interface Tournament {
    id: number;
    name: string;
    startDate: Date,
    endDate: Date,
    description: string;
    address: string;
}
