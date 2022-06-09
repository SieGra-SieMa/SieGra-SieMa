import Config from '../config.json';
import { User, UserDetailsRequest, PasswordChange } from '../_lib/types';
import Service from './service';

export default class UsersService extends Service {

    updateUser(user: UserDetailsRequest): Promise<User> {
        return super.patch(`${Config.HOST}/api/users/change-details`, user);
    };

    currentUser(): Promise<User> {
        return super.get(`${Config.HOST}/api/users/current`);
    };

    joinNewsletter(): Promise<{}> {
        return super.get(`${Config.HOST}/api/users/newsletter/join`);
    };

    leaveNewsletter(): Promise<{}> {
        return super.get(`${Config.HOST}/api/users/newsletter/leave`);
    };

    changePassword(pass: PasswordChange): Promise<{}> {
        return super.post(`${Config.HOST}/api/users/change-password`, pass);
    };
}
