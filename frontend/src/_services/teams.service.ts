import Config from '../config.json';
import { Team } from '../_lib/types';
import Service from './service';

export default class TeamsService extends Service {

    joinTeam(code: string): Promise<{}> {
        return super.post(`${Config.HOST}/api/teams/join`, { code });
    };

    createTeam(name: string): Promise<{}> {
        return super.post(`${Config.HOST}/api/teams`, { name });
    };

    getTeams(): Promise<Team[]> {
        return super.get<Team[]>(`${Config.HOST}/api/teams`);
    };

    leaveTeam(id: number): Promise<{}> {
        return super.post(`${Config.HOST}/api/teams/leave`, { id });
    };
}
