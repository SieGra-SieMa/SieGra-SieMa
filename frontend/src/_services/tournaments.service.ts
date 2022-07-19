import Config from '../config.json';
import { TeamPaidEnum } from '../_lib/types';
import { Media, Message } from '../_lib/_types/response';
import { Album, AlbumRequest, Contest, Contestant, TeamInTournament, Tournament, TournamentListItem, TournamentRequest, TournamentWithAlbums } from '../_lib/_types/tournament';
import Service, { ApiResponse } from './service';

export default class TournamentsService extends Service {

    getTournaments(): ApiResponse<Tournament[]> {
        return super.get(`${Config.HOST}/api/tournaments`);
    };

    createTournament(tournament: TournamentRequest): ApiResponse<TournamentListItem> {
        return super.post(`${Config.HOST}/api/tournaments`, tournament);
    };

    getTournamentById(id: string): ApiResponse<Tournament> {
        return super.get(`${Config.HOST}/api/tournaments/${id}`);
    };

    updateTournament(id: number, tournament: TournamentRequest): ApiResponse<Tournament> {
        return super.patch(`${Config.HOST}/api/tournaments/${id}`, tournament);
    };

    deleteTournament(id: string): ApiResponse<Message> {
        return super.del(`${Config.HOST}/api/tournaments/${id}`);
    };

    getTournamentWithAlbums(id: string): ApiResponse<TournamentWithAlbums> {
        return super.get(`${Config.HOST}/api/tournaments/${id}/albums`);
    };

    addAlbum(id: string, album: AlbumRequest): ApiResponse<Album> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/albums`, album);
    }

    addProfilePhoto(id: number, data: FormData): ApiResponse<Media[]> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/tournaments/${id}/add-profile-photo`, data, headers, false);
    };

    addTeam(tournamentId: number, teamId: number): ApiResponse<TeamInTournament> {
        return super.post(`${Config.HOST}/api/tournaments/${tournamentId}/teams/join?teamId=${teamId}`, {});
    };

    removeTeam(tournamentId: string, teamId: number): ApiResponse<any> {
        return super.post(`${Config.HOST}/api/tournaments/${tournamentId}/teams/leave?teamId=${teamId}`, {});
    };

    setTeamStatus(tournamentId: number, teamId: number, status: TeamPaidEnum): ApiResponse<TeamInTournament> {
        return super.patch(`${Config.HOST}/api/tournaments/${tournamentId}/teams/${teamId}?filter=${status}`, {});
    };

    prepareTournamnet(id: string): ApiResponse<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/prepareTournament`, {});
    }

    resetTournament(id: string): ApiResponse<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/resetTournament`, {});
    }

    composeLadder(id: string): ApiResponse<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/composeLadder`, {});
    }

    resetLadder(id: string): ApiResponse<Tournament> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/resetLadder`, {});
    }

    createContest(id: string, name: string): ApiResponse<Contest> {
        return super.post(`${Config.HOST}/api/tournaments/${id}/contests`, { name });
    }

    updateContest(tournamentId: number, contestId: number, name: string): ApiResponse<Contest> {
        return super.patch(`${Config.HOST}/api/tournaments/${tournamentId}/contests/${contestId}`, { name });
    }

    addContestScore(tournamentId: number, contestId: number, email: string, points: number): ApiResponse<Contestant> {
        return super.post(`${Config.HOST}/api/tournaments/${tournamentId}/contests/${contestId}/setScore`, { email, points });
    }

    deleteContest(tournamentId: number, contestId: number): ApiResponse<Message> {
        return super.del(`${Config.HOST}/api/tournaments/${tournamentId}/contests/${contestId}`);
    }
}
