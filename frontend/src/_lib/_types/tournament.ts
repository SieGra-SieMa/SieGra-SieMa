import { Team } from "../types";
import { Media } from "./response";

export type TournamentRequest = {
    name: string;
    startDate: string,
    endDate: string,
    address: string;
    description: string;
};

export type TournamentListItem = TournamentRequest & {
    id: number;
    profilePicture: string;
    isOpen: boolean;
    team: Team | null;
};

export type Tournament = TournamentListItem & {
    contests: Contest[];
    albums: Album[]
    groups: Group[];
    ladder: Phase[];
};

export type Group = {
    id: number;
    name: string;
    tournamentId: number;
    ladder: boolean;
    teams: {
        name: string;
        playedMatches: number;
        wonMatches: number;
        tiedMatches: number;
        lostMatches: number;
        goalScored: number;
        goalConceded: number;
        points: number;
    }[];

    matches: (Match & {
        groupId: number;
    })[] | null;
};

export type Match = {
    tournamentId: number;
    phase: number
    matchId: number;
    teamHome: string;
    teamAway: string;
    teamHomeScore: number | null;
    teamAwayScore: number | null;
};

export type Phase = {
    phase: number;
    matches: Match[];
};

export type MatchResult = {
    tournamentId: number;
    phase: number;
    matchId: number;
    homeTeamPoints: number;
    awayTeamPoints: number;
}

export type TeamInTournament = {
    teamId: number;
    tournamentId: number;
    teamName: string;
    teamProfileUrl: string;
    paid: boolean;
}

export type TournamentWithAlbums = {
    id: number;
    name: string;
    startDate: string,
    endDate: string,
    description: string;
    address: string;
    profilePicture?: string;
    albums: Album[];
};

export type AlbumRequest = {
    name: string;
    createDate: string;
}

export type Album = AlbumRequest & {
    id: number;
    tournamentId: string;
    profilePicture?: string;
    mediaList: Media[];
};


export type Contestant = {
    userId: number;
    name: string;
    surname: string;
    points: number;
};

export type Contest = {
    id: number;
    name: string;
    tournamnetId: number;
    contestants: Contestant[];
};
