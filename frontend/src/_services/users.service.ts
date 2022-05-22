import Config from '../config.json';
import { User, UserDetailsRequest } from '../_lib/types';
import Service from './service';

export default class UsersService extends Service {

    updateUser(user: UserDetailsRequest): Promise<User> {
        return super.patch(`${Config.HOST}/api/users/change-details`, user);
    };

    currentUser(): Promise<User> {
        return super.get(`${Config.HOST}/api/users/current`);
    };
}
