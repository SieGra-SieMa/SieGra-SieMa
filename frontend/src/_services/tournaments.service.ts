import Config from '../config.json';
import { TeamPaidEnum } from '../_lib/types';
import { Media, Message } from '../_lib/_types/response';
import { Album, AlbumRequest, TeamInTournament, Tournament, TournamentListItem, TournamentRequest, TournamentWithAlbums } from '../_lib/_types/tournament';
import Service from './service';

export default class TournamentsService extends Service {

    getTournaments(): Promise<Tournament[]> {
        return super.get(`${Config.HOST}/api/tournaments`);
    };

    createTournament(tournament: TournamentRequest): Promise<TournamentListItem> {
        return super.post(`${Config.HOST}/api/tournaments`, tournament);
    };

    getTournamentById(id: string): Promise<Tournament> {
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

    addAlbum(id: string, album: AlbumRequest): Promise<Album> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/albums`, album);
    }

    addProfilePhoto(id: number, data: FormData): Promise<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/tournaments/${id}/add-profile-photo`, data, headers, false);
    };

    getTeamsInTournament(id: string): Promise<TeamInTournament[]> {
        return super.get(`${Config.HOST}/api/tournaments/${id}/teams?filter=${TeamPaidEnum.All}`);
    };

    addTeam(tournamentId: number, teamId: number): Promise<any> {
        return super.post(`${Config.HOST}/api/tournaments/${tournamentId}/teams/join?teamId=${teamId}`, {});
    };

    removeTeam(tournamentId: string, teamId: number): Promise<any> {
        return super.post(`${Config.HOST}/api/tournaments/${tournamentId}/teams/leave?teamId=${teamId}`, {});
    };

    setTeamStatus(tournamentId: number, teamId: number, status: TeamPaidEnum): Promise<TeamInTournament> {
        return super.patch(`${Config.HOST}/api/tournaments/${tournamentId}/teams/${teamId}?filter=${status}`, {});
    };

    prepareTournamnet(id: string): Promise<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/prepareTournament`, {});
    }

    resetTournament(id: string): Promise<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/resetTournament`, {});
    }

    composeLadder(id: string): Promise<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/composeLadder`, {});
    }

    resetLadder(id: string): Promise<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/resetLadder`, {});
    }

    createContest(id: string, name: string): Promise<{}> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/contests`, { name });
    }

    updateContest(tournamentId: number, contestId: number, name: string): Promise<{}> {
        return super.patch(`${Config.HOST}/api/tournaments/${tournamentId}/contests/${contestId}`, { name });
    }

    addContestScore(tournamentId: number, contestId: number, email: string, points: number): Promise<{}> {
        return super.post(`${Config.HOST}/api/tournaments/${tournamentId}/contests/${contestId}/setScore`, { email, points });
    }

    deleteContest(tournamentId: number, contestId: number) {
        return super.del(`${Config.HOST}/api/tournaments/${tournamentId}/contests/${contestId}`);
    }
}
