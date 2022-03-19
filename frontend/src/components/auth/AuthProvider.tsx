import { useState } from 'react';
import { Session } from '../../_lib/types';
import { AuthContext } from './AuthContext';

export default function AuthProvider({ children }: { children: React.ReactNode }) {

    const [session, setSession] = useState<Session | null>(() => {
        const session = localStorage.getItem('session');
        if (session) {
            return JSON.parse(session) as any;
        }
        return null;
    });

    const saveSession = (session: Session | null) => {
        if (session) {
            localStorage.setItem('session', JSON.stringify(session));
        } else {
            localStorage.removeItem('session');
        }
        setSession(session);
    }

    const value = { session, saveSession };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
