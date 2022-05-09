import { HOST } from '../config.json';
import { ROLES } from '../_lib/roles';
import { Session } from '../_lib/types';

import accountsService from './accounts.service';
import teamsService from './teams.service';
import tournamentsService from './tournaments.service';
import usersService from './users.service';

const authState = {
	logout: () => {
		localStorage.removeItem('session');
	},
	update: (session: Session) => {
		localStorage.setItem('session', JSON.stringify(session));
	},
}

let session: Session | null = null;

const services = {
	accountsService,
	teamsService,
	tournamentsService,
	usersService,
	authState,
	setSession: (value: Session | null) => session = value,
};

export default services;

interface WSResponse {
	error: string;
	data: any;
}

function handleResponse<T>(
	response: Response,
	options: RequestInit,
	retry: boolean = false
): Promise<T> {
	return response.text().then(async (text) => {
		if ([401, 403].indexOf(response.status) !== -1) {
			if (session === null) {
				authState.logout();
				return Promise.reject(response.statusText);
			}

			if (response.url !== `${HOST}/api/accounts/refresh-token` && !retry) {
				const refreshToken = session.refreshToken;
				if (refreshToken) {
					try {
						const tokens = await accountsService.refresh(refreshToken);
						authState.update({
							...tokens,
							accessToken: tokens.token,
							role: ROLES.User // TODO
						} as Session);
						(options.headers as Headers).set(
							'Authorization',
							`Bearer ${tokens.token}`
						);
						return await fetch(response.url, options)
							.then(res => handleResponse<T>(res, options, true));
					} catch (e) {
						authState.logout();
						return Promise.reject(e);
					}
				}
			}
		}

		if (!text) {
			if (response.ok) {
				return {} as T;
			}

			return Promise.reject(response.statusText);
		}

		const rawData = JSON.parse(text) as WSResponse;
		if (response.ok) {
			return rawData.data as T;
		}

		const error = rawData.error || response.statusText;
		return Promise.reject(error);
	});
}

export function api<T>(url: string, options: RequestInit): Promise<T> {
	const headers = new Headers();
	headers.set('Content-Type', 'application/json');
	if (session) {
		const token = session.accessToken;
		headers.set('Authorization', `Bearer ${token}`);
	}
	options.headers = headers;
	return fetch(url, options)
		.then(res => handleResponse<T>(res, options));
}

export function get<T>(url: string): Promise<T> {
	const options: RequestInit = {
		method: 'GET',
	}
	return api<T>(url, options);
}

export function post<T>(url: string, body: any): Promise<T> {
	const options: RequestInit = {
		method: 'POST',
		body: JSON.stringify(body),
	}
	return api<T>(url, options);
}

export function put<T>(url: string, body: any): Promise<T> {
	const options: RequestInit = {
		method: 'PUT',
		body: JSON.stringify(body),
	}
	return api<T>(url, options);
}

export function patch<T>(url: string, body: any): Promise<T> {
	const options: RequestInit = {
		method: 'PATCH',
		body: JSON.stringify(body),
	}
	return api<T>(url, options);
}

export function del<T>(url: string): Promise<T> {
	const options: RequestInit = {
		method: 'DELETE',
	}
	return api<T>(url, options);
}