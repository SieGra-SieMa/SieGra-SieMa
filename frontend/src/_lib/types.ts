import { ROLES } from './roles';

export type User = {
	id: number;
	name: string;
	surname: string;
	email: string;
	newsletter: boolean;
	isLocked: boolean;
	roles: ROLES[];
};

export type Player = {
	id: number;
	name: string;
	surname: string;
};

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
	error?: any;
};

export enum TeamPaidEnum {
	All, Paid, Unpaid
};

export type PasswordChange = {
	oldPassword: string;
	newPassword: string;
};




export enum AlertTypeEnum {
	success,
	error,
}

export type Alert = {
	id: number;
	message: string;
	type: AlertTypeEnum;
}