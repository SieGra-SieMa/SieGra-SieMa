import { useContext, createContext } from 'react';
import { Tournament } from '../../_lib/types';

interface TournamentsContextType {
    tournaments: Tournament[] | null;
    setTournaments: (tournaments: Tournament[] | null) => void;
}

export const TournamentsContext = createContext<TournamentsContextType>(null!);

export function useTournaments() {
    return useContext(TournamentsContext);
}
