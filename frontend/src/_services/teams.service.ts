import Config from '../config.json';
import { Team } from '../_lib/types';
import { get, post } from '../_lib/_utils/utils';

export const teamsService = {
    join,
    create,
    getTeams,
    leave,
};

function join(code: string): Promise<{}> {
    return post(`${Config.HOST}/api/teams/join`, { code });
};

function create(name: string): Promise<{}> {
    return post(`${Config.HOST}/api/teams/`, { name });
};

function getTeams(): Promise<Team[]>  {
    return get<Team[]>(`${Config.HOST}/api/teams/`);
};

function leave(id: number): Promise<{}> {
    return post(`${Config.HOST}/api/teams/leave`, { id })
};
