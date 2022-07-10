import { useEffect, useState } from 'react';
import { User } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import { useAuth } from '../auth/AuthContext';
import { UserContext } from './UserContext';

export default function UserProvider({ children }: { children: React.ReactNode }) {

    const { usersService } = useApi();
    const { session } = useAuth();

    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        if (!session) {
            setUser(null);
            return;
        }
        usersService.currentUser()
            .then((user) => setUser(user));
    }, [session, setUser, usersService]);

    const value = { user, setUser };

    return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
}
