import { HOST } from '../config.json';
import { authenticationService } from '../_services/authentication.service';
import { Account } from './types';

// export interface WSResponse {
// 	error: string | undefined;
// 	data: any;
// }

export function handleResponse<T>(
	response: Response, 
	options: RequestInit, 
	retry: boolean = false
): Promise<T> {
	return response.text().then(async (text) => {
		// const rawData = JSON.parse(text) as WSResponse;
		// if (response.ok) return rawData.data as T;
		const rawData = JSON.parse(text) as T;
		if (response.ok) return rawData;
		if ([401, 403].indexOf(response.status) !== -1) {
			const user = localStorage.getItem('user');
			if (user && response.url !== `${HOST}users/refresh` && !retry) {
				const parsedData = (JSON.parse(user));
				const refreshToken = parsedData.refreshToken;
				if (refreshToken) {
					try {
						const tokens = await authenticationService.refresh(refreshToken);
						(options.headers as Headers).set('Authorization', `Bearer ${tokens.accessToken}`);
						return await fetch(response.url, options)
							.then(res => handleResponse<T>(res, options, true))
					} catch (e) {
						authenticationService.logout()
						window.location.reload();
						return Promise.reject(e)
					}
				}
			}
			authenticationService.logout()
			window.location.reload();
		}
		// const error = rawData.error || response.statusText;
		const error = rawData || response.statusText;
		return Promise.reject(error)
	});
}

export function api<T>(url: string, options: RequestInit): Promise<T> {
	const user = localStorage.getItem('currentUser');
	const headers = new Headers();
	headers.set('Content-Type', 'application/json');
	if (user) {
		const token = (JSON.parse(user) as Account).accessToken;
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
