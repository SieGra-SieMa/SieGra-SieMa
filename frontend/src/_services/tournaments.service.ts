import Config from '../config.json';
import { Tournament } from '../_lib/types';
import { del, get, patch, post } from '../_lib/utils';

export const tournamentService = {
    getTournaments,
    create,
    getTournament,
    update,
    remove,
};

function getTournaments(): Promise<{}> {
    return get(`${Config.HOST}/api/tournaments`);
};

function create(tournament: Tournament): Promise<Tournament> {
    return post(`${Config.HOST}/api/tournaments`, tournament);
};

function getTournament(id: number): Promise<Tournament> {
    return get(`${Config.HOST}/api/tournaments/${id}`);
};

function update(tournament: Tournament): Promise<Tournament> {
    return patch(`${Config.HOST}/api/tournaments/${tournament.id}`, tournament);
};

function remove(id: number): Promise<Tournament> {
    return del(`${Config.HOST}/api/tournaments/${id}`);
};
