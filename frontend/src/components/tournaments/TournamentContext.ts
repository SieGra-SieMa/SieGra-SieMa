import { useContext, createContext } from 'react';
import { Tournament } from '../../_lib/_types/tournament';

interface TournamentContextType {
    tournament: Tournament;
    setTournament: (tournaments: Tournament) => void;
}

export const TournamentContext = createContext<TournamentContextType>(null!);

export function useTournament() {
    return useContext(TournamentContext);
}
