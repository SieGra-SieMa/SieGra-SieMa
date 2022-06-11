import { Session, Tokens } from "../_lib/types";
import Config from '../config.json';
import jwtDecode, { JwtPayload } from "jwt-decode";

export type AuthState = {
    logout: () => void;
    update: (session: Session) => void;
}

type WSResponse = {
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
    };
    static refresh: Promise<Tokens> | null = null;

    handleResponse<T>(
        response: Response,
        options: RequestInit,
    ): Promise<T> {
        return response.text().then(async (text) => {
            if ([401, 403].indexOf(response.status) !== -1) {
                if (this.session === null) {
                    this.authState.logout();
                    return Promise.reject(response.statusText);
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

    async api<T>(url: string, options: RequestInit): Promise<T> {
        let headers = options.headers;
        if (!headers) {
            headers = new Headers();
            headers.set('Content-Type', 'application/json');
        }

        if (this.session && url !== `${Config.HOST}/api/accounts/refresh-token`) {
            let token = this.session.accessToken;
            const { exp } = jwtDecode<JwtPayload>(token);
            if ((((exp ?? 0) * 1000) - (3 * 60 * 1000)) < Date.now()) {
                const refreshToken = this.session.refreshToken;
                try {
                    if (!Service.refresh) {
                        Service.refresh = this.post<Tokens>(
                            `${Config.HOST}/api/accounts/refresh-token`,
                            { refreshToken }
                        );
                        const tokens = await Service.refresh;
                        token = tokens.token;
                        this.authState.update({
                            ...tokens,
                            accessToken: tokens.token,
                        } as Session);
                    } else {
                        const tokens = await Service.refresh;
                        token = tokens.token;
                    }
                } catch (e) {
                    return Promise.reject(e);
                } finally {
                    Service.refresh = null;
                }
            }

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
            .then(res => this.handleResponse<T>(res, options));
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
