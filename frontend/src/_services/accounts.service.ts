import Config from '../config.json';
import { Session } from '../_lib/types';
import { Message } from '../_lib/_types/response';
import Service from './service';

export default class AccountsService extends Service {

    register(name: string, surname: string, email: string, password: string): Promise<Message> {
        return super.post(`${Config.HOST}/api/accounts/register`, { name, surname, email, password });
    };

    authenticate(email: string, password: string): Promise<Session> {
        return super.post<Session>(`${Config.HOST}/api/accounts/authenticate`, { email, password })
    };

    logout() {
        return Promise.resolve();
    };
};
