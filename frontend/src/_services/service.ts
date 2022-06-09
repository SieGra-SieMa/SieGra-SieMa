import { Session, Tokens } from "../_lib/types";
import { HOST } from '../config.json';

export interface AuthState {
    logout: () => void;
    update: (session: Session) => void;
}

interface WSResponse {
    error: string;
    data: any;
}

export default class Service {

    session: Session | null = null;
    authState: AuthState = {
        logout: () => {
            localStorage.removeItem('session');
        },
        update: (session: Session) => {
            localStorage.setItem('session', JSON.stringify(session));
        },
    }

    handleResponse<T>(
        response: Response,
        options: RequestInit,
        retry: boolean = false
    ): Promise<T> {
        return response.text().then(async (text) => {
            if ([401, 403].indexOf(response.status) !== -1) {
                if (this.session === null) {
                    this.authState.logout();
                    return Promise.reject(response.statusText);
                }

                if (response.url !== `${HOST}/api/accounts/refresh-token` && !retry) {
                    const refreshToken = this.session.refreshToken;
                    if (refreshToken) {
                        try {
                            const tokens = await this.post<Tokens>(
                                `${HOST}/api/accounts/refresh-token`,
                                { refreshToken }
                            );
                            this.authState.update({
                                ...tokens,
                                accessToken: tokens.token,
                            } as Session);
                            (options.headers as Headers).set(
                                'Authorization',
                                `Bearer ${tokens.token}`
                            );
                            return await fetch(response.url, options)
                                .then(res => this.handleResponse<T>(res, options, true));
                        } catch (e) {
                            this.authState.logout();
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

    api<T>(url: string, options: RequestInit): Promise<T> {
        let headers = options.headers;
        if (!headers) {
            headers = new Headers();
            headers.set('Content-Type', 'application/json');
        }
        if (this.session) {
            const token = this.session.accessToken;
            if (headers instanceof Headers) {
                headers.set('Authorization', `Bearer ${token}`);
            } else if (headers instanceof Array) {
                headers.push(['Authorization', `Bearer ${token}`])
            } else {
                headers['Authorization'] = `Bearer ${token}`;
            }
        }
        options.headers = headers;
        return fetch(url, options)
            .then(res => this.handleResponse<T>(res, options))
            .catch((e) => Promise.reject(e.message || e));

    }

    get<T>(url: string, headers?: Headers): Promise<T> {
        const options: RequestInit = {
            method: 'GET',
            headers,
        }
        return this.api<T>(url, options);
    }

    post<T>(url: string, body: any, headers?: Headers, isJSON: boolean = true): Promise<T> {
        const options: RequestInit = {
            method: 'POST',
            body: isJSON ? JSON.stringify(body) : body,
            headers,
        }
        return this.api<T>(url, options);
    }

    put<T>(url: string, body: any, headers?: Headers): Promise<T> {
        const options: RequestInit = {
            method: 'PUT',
            body: JSON.stringify(body),
            headers,
        }
        return this.api<T>(url, options);
    }

    patch<T>(url: string, body: any, headers?: Headers): Promise<T> {
        const options: RequestInit = {
            method: 'PATCH',
            body: JSON.stringify(body),
            headers,
        }
        return this.api<T>(url, options);
    }

    del<T>(url: string, headers?: Headers): Promise<T> {
        const options: RequestInit = {
            method: 'DELETE',
            headers,
        }
        return this.api<T>(url, options,);
    }
}
