import Config from '../config.json';
import { Session, Tokens } from '../_lib/types';
import { post } from './index';

const accountsService = {
    register,
    authenticate,
    logout,
    refresh,
};

export default accountsService;

function register(name: string, surname: string, email: string, password: string): Promise<{}> {
    return post(`${Config.HOST}/api/accounts/register`, { name, surname, email, password });
};

function authenticate(email: string, password: string): Promise<Session> {
    return post<Session>(`${Config.HOST}/api/accounts/authenticate`, { email, password })
};

function refresh(refreshToken: string): Promise<Tokens> {
    return post<Tokens>(`${Config.HOST}/api/accounts/refresh-token`, { refreshToken });
};

function logout() {
    return Promise.resolve();
};
