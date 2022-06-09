import Config from '../config.json';
import { Album, TournamentWithAlbums } from '../_lib/types';
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

    getTournamentWithAlbums(id: string): Promise<TournamentWithAlbums> {
        return super.get(`${Config.HOST}/api/tournaments/${id}/albums`);
    };

    addAlbumToTournament(id: string, album: Album): Promise<Album> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/albums`, album);
    }

    addProfilePhoto(id: number, data: FormData): Promise<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/tournaments/${id}/add-profile-photo`, data, headers, false);
    };

    getTeamsInTournament(id: string): Promise<Tournament[]> {
        return super.get(`${Config.HOST}/api/tournaments/${id}/teams?filter=0`);
    };

    addTeam(tournamentId: number, teamId: number): Promise<any> {
        return super.post(`${Config.HOST}/api/Tournaments/${tournamentId}/teams/join?teamId=${teamId}`, {});
    };

    removeTeam(tournamentId: string, teamId: number): Promise<any> {
        return super.post(`${Config.HOST}/api/Tournaments/${tournamentId}/teams/leave?teamId=${teamId}`, {});
    };
}
