export interface User {
    name: string;
    surname: string;
}

export interface Account extends User {
    email: string;
    token: string;
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
