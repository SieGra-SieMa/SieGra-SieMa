import Config from '../config.json';
import { Session } from '../_lib/types';
import { Message } from '../_lib/_types/response';
import Service from './service';

export default class AccountsService extends Service {

    register(name: string, surname: string, email: string, password: string): Promise<Message> {
        return super.post(`${Config.HOST}/api/accounts/register`, { name, surname, email, password });
    };

    authenticate(email: string, password: string): Promise<Session> {
        return super.post(`${Config.HOST}/api/accounts/authenticate`, { email, password })
    };

    forgetPassword(email: string): Promise<{}> {
        return super.get(`${Config.HOST}/api/accounts/forget-password?email=${email}`)
    };

    resetPassword(userId: string, token: string, password: string): Promise<Message> {
        return super.post(`${Config.HOST}/api/accounts/reset-password?userid=${userId}&token=${token}`, password)
    };

    confirmEmail(userId: string, token: string): Promise<Message> {
        return super.get(`${Config.HOST}/api/accounts/confirm-email?userid=${userId}&token=${token}`)
    };

    logout() {
        return Promise.resolve();
    };
};
