import { useEffect, useState } from 'react';
import { Session } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import { AuthContext } from './AuthContext';

export default function AuthProvider({ children }: { children: React.ReactNode }) {

    const api = useApi();

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
        api.authState.logout = () => setSession(null);
        api.authState.update = (s) => setSession(s);
    }, [api]);

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
