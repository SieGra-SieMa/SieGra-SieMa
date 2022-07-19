import Config from '../config.json';
import { User, UserDetailsRequest, PasswordChange } from '../_lib/types';
import { Message, Paginated } from '../_lib/_types/response';
import Service, { ApiResponse } from './service';

export default class UsersService extends Service {

    updateUser(user: UserDetailsRequest): ApiResponse<User> {
        return super.patch(`${Config.HOST}/api/users/change-details`, user);
    };

    currentUser(): ApiResponse<User> {
        return super.get(`${Config.HOST}/api/users/current`);
    };

    joinNewsletter(): ApiResponse<User> {
        return super.get(`${Config.HOST}/api/users/newsletter/join`);
    };

    leaveNewsletter(): ApiResponse<User> {
        return super.get(`${Config.HOST}/api/users/newsletter/leave`);
    };

    changePassword(pass: PasswordChange): ApiResponse<Message> {
        return super.post(`${Config.HOST}/api/users/change-password`, pass);
    };

    getUsers(page: number, count: number, search: string): ApiResponse<Paginated<User>> {
        return super.get(`${Config.HOST}/api/users?page=${page}&count=${count}&filter=${search}`);
    };

    addUserRole(id: number, role: string[]): ApiResponse<User> {
        return super.post(`${Config.HOST}/api/users/${id}/add-role`, role);
    };

    removeUserRole(id: number, role: string[]): ApiResponse<User> {
        return super.post(`${Config.HOST}/api/users/${id}/remove-role`, role);
    };

    adminUpdateUser(id: number, user: UserDetailsRequest): ApiResponse<User> {
        return super.patch(`${Config.HOST}/api/users/${id}/update-user`, user);
    };

    adminBanUser(id: number): ApiResponse<User> {
        return super.del(`${Config.HOST}/api/users/admin/${id}`);
    };

    adminUnbanUser(id: number): ApiResponse<User> {
        return super.patch(`${Config.HOST}/api/users/admin/${id}`, {});
    };

    sendNewsletter(title: string, body: string, tournamentId?: string): ApiResponse<Message> {
        return super.post(`${Config.HOST}/api/users/newsletter/send`, { title, body, tournamentId });
    };
}