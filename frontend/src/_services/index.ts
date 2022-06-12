import { Session } from '../_lib/types';
import TeamsService from './teams.service';
import AccountsService from './accounts.service';
import TournamentsService from './tournaments.service';
import UsersService from './users.service';
import { AuthState } from './service';
import AlbumsService from './albums.service';
import MatchService from './match.service';
import MediaService from './media.service';

export default class ApiClient {

	accountsService = new AccountsService();
	teamsService = new TeamsService();
	tournamentsService = new TournamentsService();
	usersService = new UsersService();
	albumsService = new AlbumsService();
	matchService = new MatchService();
	mediaService = new MediaService();

	setSession(session: Session | null) {
		this.teamsService.session = session;
		this.accountsService.session = session;
		this.tournamentsService.session = session;
		this.usersService.session = session;
		this.albumsService.session = session;
		this.matchService.session = session;
		this.mediaService.session = session;
	}

	setAuthState(authState: AuthState) {
		this.teamsService.authState = authState;
		this.accountsService.authState = authState;
		this.tournamentsService.authState = authState;
		this.usersService.authState = authState;
		this.albumsService.authState = authState;
		this.matchService.authState = authState;
		this.mediaService.authState = authState;
	}
}
