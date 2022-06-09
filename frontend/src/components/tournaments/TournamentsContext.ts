import { useContext, createContext } from 'react';
import { TournamentListItem } from '../../_lib/_types/tournament';

interface TournamentsContextType {
    tournaments: TournamentListItem[] | null;
    setTournaments: (tournaments: TournamentListItem[] | null) => void;
}

export const TournamentsContext = createContext<TournamentsContextType>(null!);

export function useTournaments() {
    return useContext(TournamentsContext);
}
