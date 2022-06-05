import { useContext, createContext } from 'react';
import { TournamentList } from '../../_lib/_types/tournament';

interface TournamentsContextType {
    tournaments: TournamentList[] | null;
    setTournaments: (tournaments: TournamentList[] | null) => void;
}

export const TournamentsContext = createContext<TournamentsContextType>(null!);

export function useTournaments() {
    return useContext(TournamentsContext);
}
