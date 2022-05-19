import Config from '../config.json';
import { Session } from '../_lib/types';
import Service from './service';

export default class AccountsService extends Service {

    register(name: string, surname: string, email: string, password: string): Promise<{}> {
        return super.post(`${Config.HOST}/api/accounts/register`, { name, surname, email, password });
    };

    authenticate(email: string, password: string): Promise<Session> {
        return super.post<Session>(`${Config.HOST}/api/accounts/authenticate`, { email, password })
    };

    // refresh(refreshToken: string): Promise<Tokens> {
    //     return super.post<Tokens>(`${Config.HOST}/api/accounts/refresh-token`, { refreshToken });
    // };

    logout() {
        return Promise.resolve();
    };
};
