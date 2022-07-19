import Config from '../config.json';
import { Team } from '../_lib/types';
import { Media, Message, Paginated } from '../_lib/_types/response';
import Service, { ApiResponse } from './service';

export default class TeamsService extends Service {

    joinTeam(code: string): ApiResponse<Team> {
        return super.post(`${Config.HOST}/api/teams/join`, { code });
    };

    createTeam(name: string): ApiResponse<Team> {
        return super.post(`${Config.HOST}/api/teams`, { name });
    };

    getTeams(): ApiResponse<Team[]> {
        return super.get<Team[]>(`${Config.HOST}/api/teams`);
    };

    getAllTeams(page: number, count: number, search: string): ApiResponse<Paginated<Team>> {
        return super.get(`${Config.HOST}/api/teams/admin?page=${page}&count=${count}&filter=${search}`);
    };

    getTeamsIAmCaptain(): ApiResponse<Team[]> {
        return super.get(`${Config.HOST}/api/teams/teamsiamcaptain`);
    };

    leaveTeam(id: number): ApiResponse<{}> {
        return super.post(`${Config.HOST}/api/teams/leave`, { id });
    };

    switchCaptain(teamId: number, userId: number): ApiResponse<Team> {
        return super.post(`${Config.HOST}/api/teams/${teamId}/switch-captain/${userId}`, null);
    };

    removePlayer(teamId: number, userId: number): ApiResponse<Team> {
        return super.post(`${Config.HOST}/api/teams/${teamId}/remove-user/${userId}`, {});
    };

    updateTeam(id: number, name: string): ApiResponse<Team> {
        return super.patch(`${Config.HOST}/api/teams/${id}`, { name });
    };

    addProfilePhoto(id: number, data: FormData): ApiResponse<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/teams/${id}/add-profile-photo`, data, headers, false);
    };

    addProfilePhotoAdmin(id: number, data: FormData): ApiResponse<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/teams/${id}/admin/add-profile-photo`, data, headers, false);
    };

    updateTeamAdmin(id: number, name: string): ApiResponse<Team> {
        return super.patch(`${Config.HOST}/api/teams/${id}/admin/change-details`, { name });
    };

    adminDeleteTeam(id: number): ApiResponse<Message> {
        return super.del(`${Config.HOST}/api/teams/admin/${id}`);
    };

    sendInvite(id: number, email: string): ApiResponse<Message> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/teams/${id}/send-invite?emailAdress=${email}`, {}, headers, false);
    }
}
