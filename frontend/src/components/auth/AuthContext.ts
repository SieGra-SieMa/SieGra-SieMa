import { useContext, createContext } from 'react';
import { Session } from '../../_lib/types';

interface AuthContextType {
    session: Session | null;
    setSession: (session: Session | null) => void;
}

export const AuthContext = createContext<AuthContextType>(null!);

export function useAuth() {
    return useContext(AuthContext);
}
