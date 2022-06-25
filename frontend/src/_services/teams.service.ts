import Config from '../config.json';
import { Team } from '../_lib/types';
import { Media, Message } from '../_lib/_types/response';
import Service from './service';

export default class TeamsService extends Service {

    joinTeam(code: string): Promise<Team> {
        return super.post(`${Config.HOST}/api/teams/join`, { code });
    };

    createTeam(name: string): Promise<Team> {
        return super.post(`${Config.HOST}/api/teams`, { name });
    };

    getTeams(): Promise<Team[]> {
        return super.get<Team[]>(`${Config.HOST}/api/teams`);
    };

    getAllTeams(): Promise<Team[]> {
        return super.get(`${Config.HOST}/api/teams/admin`);
    };

    getTeamsIAmCaptain(): Promise<Team[]> {
        return super.get(`${Config.HOST}/api/teams/teamsiamcaptain`);
    };

    leaveTeam(id: number): Promise<{}> {
        return super.post(`${Config.HOST}/api/teams/leave`, { id });
    };

    switchCaptain(teamId: number, userId: number): Promise<Team> {
        return super.post(`${Config.HOST}/api/teams/${teamId}/switch-captain/${userId}`, null);
    };

    removePlayer(teamId: number, userId: number): Promise<Team> {
        return super.post(`${Config.HOST}/api/teams/${teamId}/remove-user/${userId}`, {});
    };

    updateTeam(id: number, name: string): Promise<Team> {
        return super.patch(`${Config.HOST}/api/teams/${id}`, { name });
    };

    addProfilePhoto(id: number, data: FormData): Promise<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/teams/${id}/add-profile-photo`, data, headers, false);
    };

    admindDeleteTeam(id: number): Promise<Message> {
        return super.del(`${Config.HOST}/api/teams/admin/${id}`);
    };
}
