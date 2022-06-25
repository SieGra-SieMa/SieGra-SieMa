import Config from '../config.json';
import { User, UserDetailsRequest, PasswordChange } from '../_lib/types';
import { Message } from '../_lib/_types/response';
import Service from './service';

export default class UsersService extends Service {

    updateUser(user: UserDetailsRequest): Promise<User> {
        return super.patch(`${Config.HOST}/api/users/change-details`, user);
    };

    currentUser(): Promise<User> {
        return super.get(`${Config.HOST}/api/users/current`);
    };

    joinNewsletter(): Promise<User> {
        return super.get(`${Config.HOST}/api/users/newsletter/join`);
    };

    leaveNewsletter(): Promise<User> {
        return super.get(`${Config.HOST}/api/users/newsletter/leave`);
    };

    changePassword(pass: PasswordChange): Promise<Message> {
        return super.post(`${Config.HOST}/api/users/change-password`, pass);
    };

    getUsers(): Promise<User[]> {
        return super.get(`${Config.HOST}/api/users`);
    };

    addUserRole(id: number, role: string[]): Promise<User> {
        return super.post(`${Config.HOST}/api/users/${id}/add-role`, role);
    };

    removeUserRole(id: number, role: string[]): Promise<User> {
        return super.post(`${Config.HOST}/api/users/${id}/remove-role`, role);
    };

    adminUpdateUser(id: number, user: UserDetailsRequest): Promise<User> {
        return super.patch(`${Config.HOST}/api/users/${id}/update-user`, user);
    };

    adminBanUser(id: number): Promise<User> {
        return super.del(`${Config.HOST}/api/users/admin/${id}`);
    };
}