export interface User {
    name: string;
    surname: string;
}

export interface Account extends User {
    email: string;
    token: string;
}

export interface Team {
    name: string;
    code: string;
    players: User[];
}