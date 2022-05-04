import { useEffect, useState } from 'react';
import { Session } from '../../_lib/types';
import { AuthContext } from './AuthContext';

export const authState = {
    logout: () => {
        localStorage.removeItem('session');
    },
    update: (session: Session) => {
        localStorage.setItem('session', JSON.stringify(session));
    },
}

export default function AuthProvider({ children }: { children: React.ReactNode }) {

    const [session, setSessionState] = useState<Session | null>(() => {
        const session = localStorage.getItem('session');
        if (session) {
            return JSON.parse(session) as Session;
        }
        return null;
    });

    const setSession = (session: Session | null) => {
        if (session) {
            localStorage.setItem('session', JSON.stringify(session));
        } else {
            localStorage.removeItem('session');
        }
        setSessionState(session);
    }

    const value = { session, setSession };

    useEffect(() => {
        authState.logout = () => setSession(null);
        authState.update = (s) => setSession(s);
    }, []);

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
