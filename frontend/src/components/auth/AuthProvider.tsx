import { useCallback, useEffect, useMemo, useState } from 'react';
import { Session } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import { AuthContext } from './AuthContext';

export default function AuthProvider({ children }: { children: React.ReactNode }) {

    const api = useApi();

    const [session, setSessionState] = useState<Session | null>(() => {
        const data = localStorage.getItem('session');
        if (data) {
            const session = JSON.parse(data) as Session;
            api.setSession(session);
            return session;
        }
        return null;
    });

    const setSession = useCallback((session: Session | null) => {
        if (session) {
            localStorage.setItem('session', JSON.stringify(session));
        } else {
            localStorage.removeItem('session');
        }
        api.setSession(session);
        setSessionState(session);
    }, [setSessionState, api]);

    useEffect(() => {
        const authState = {
            logout: () => setSession(null),
            update: (s: Session | null) => setSession(s)
        }
        api.setAuthState(authState);
    }, [setSession, api]);

    const value = useMemo(() => ({ session, setSession }), [session, setSession]);

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
