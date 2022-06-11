import { useContext, createContext } from 'react';
import { Team } from '../../_lib/types';

interface TeamsContextType {
    teams: Team[] | null;
    setTeams: (teams: Team[] | null) => void;
}

export const TeamsContext = createContext<TeamsContextType>(null!);

export function useTeams() {
    return useContext(TeamsContext);
}
