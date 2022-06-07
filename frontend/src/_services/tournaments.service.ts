import Config from '../config.json';
import { Media, Message } from '../_lib/_types/response';
import { Tournament, TournamentListItem, TournamentRequest } from '../_lib/_types/tournament';
import Service from './service';

export default class TournamentsService extends Service {

    getTournaments(): Promise<Tournament[]> {
        return super.get(`${Config.HOST}/api/tournaments`);
    };

    createTournament(tournament: TournamentRequest): Promise<TournamentListItem> {
        return super.post(`${Config.HOST}/api/tournaments`, tournament);
    };

    getTournamentbyId(id: string): Promise<Tournament> {
        return super.get(`${Config.HOST}/api/tournaments/${id}`);
    };

    updateTournament(id: number, tournament: TournamentRequest): Promise<Tournament> {
        return super.patch(`${Config.HOST}/api/tournaments/${id}`, tournament);
    };

    deleteTournament(id: string): Promise<Message> {
        return super.del(`${Config.HOST}/api/tournaments/${id}`);
    };

    addProfilePhoto(id: number, data: FormData): Promise<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/tournaments/${id}/add-profile-photo`, data, headers, false);
    };
}
