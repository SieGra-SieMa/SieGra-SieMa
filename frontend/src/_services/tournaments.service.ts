import Config from '../config.json';
import { Tournament } from '../_lib/types';
import { del, get, patch, post } from '.';

const tournamentsServices = {
    getTournaments,
    createTournament,
    getTournamentbyId,
    updateTournament,
    removeTournament,
};

export default tournamentsServices;

function getTournaments(): Promise<{}> {
    return get(`${Config.HOST}/api/tournaments`);
};

function createTournament(tournament: Tournament): Promise<Tournament> {
    return post(`${Config.HOST}/api/tournaments`, tournament);
};

function getTournamentbyId(id: number): Promise<Tournament> {
    return get(`${Config.HOST}/api/tournaments/${id}`);
};

function updateTournament(tournament: Tournament): Promise<Tournament> {
    return patch(`${Config.HOST}/api/tournaments/${tournament.id}`, tournament);
};

function removeTournament(id: number): Promise<Tournament> {
    return del(`${Config.HOST}/api/tournaments/${id}`);
};
