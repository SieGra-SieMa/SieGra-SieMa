import Config from '../config.json';
import { Team } from '../_lib/types';
import { get, post } from '.';

const teamsService = {
    joinTeam,
    createTeam,
    getTeams,
    leaveTeam,
};

export default teamsService;

function joinTeam(code: string): Promise<{}> {
    return post(`${Config.HOST}/api/teams/join`, { code });
};

function createTeam(name: string): Promise<{}> {
    return post(`${Config.HOST}/api/teams`, { name });
};

function getTeams(): Promise<Team[]> {
    return get<Team[]>(`${Config.HOST}/api/teams`);
};

function leaveTeam(id: number): Promise<{}> {
    return post(`${Config.HOST}/api/teams/leave`, { id })
};
