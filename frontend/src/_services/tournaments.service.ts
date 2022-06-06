import Config from '../config.json';
import { Tournament, TournamentWithAlbums } from '../_lib/types';
import Service from './service';

export default class TournamentsService extends Service {

    getTournaments(): Promise<Tournament[]> {
        return super.get(`${Config.HOST}/api/tournaments`);
    };

    createTournament(tournament: Tournament): Promise<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments`, tournament);
    };

    getTournamentbyId(id: string): Promise<Tournament> {
        return super.get(`${Config.HOST}/api/tournaments/${id}`);
    };

    updateTournament(tournament: Tournament): Promise<Tournament> {
        return super.patch(`${Config.HOST}/api/tournaments/${tournament.id}`, tournament);
    };

    deleteTournament(id: string): Promise<{}> {
        return super.del(`${Config.HOST}/api/tournaments/${id}`);
    };

    getTournamentWithAlbums(id: string): Promise<TournamentWithAlbums> {
        return super.get(`${Config.HOST}/api/tournaments/${id}/albums`);
    };
}
