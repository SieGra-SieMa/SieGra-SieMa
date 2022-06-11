import { useContext, createContext } from 'react';
import { TeamInTournament, Tournament } from '../../_lib/_types/tournament';

interface TournamentContextType {
    tournament: Tournament | null;
    setTournament: (tournaments: Tournament | null) => void;
    teams: TeamInTournament[] | null;
    setTeams: (teams: TeamInTournament[] | null) => void;
}

export const TournamentContext = createContext<TournamentContextType>(null!);

export function useTournament() {
    return useContext(TournamentContext);
}
