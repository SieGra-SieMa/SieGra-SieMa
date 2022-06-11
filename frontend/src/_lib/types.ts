import { ROLES } from "./roles";

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
    profilePicture: string;
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

export type FacebookPost = {
	created_time: string;
	full_picture: string;
	id: string;
	message: string;
	permalink_url: string;
};

export type FacebookFeed = {
	data: FacebookPost[];
	paging: {
		cursor: {
			after: string;
			before: string;
		}
	}
};

export enum TeamPaidEnum {
    All, Paid, Unpaid
};
