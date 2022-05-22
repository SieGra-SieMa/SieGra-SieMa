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
        if (session) {
            usersService.currentUser()
                .then(
                    (user) => setUser(user),
                    (error) => {
                        alert(error); // TODO
                    }
                );
        } else {
            setUser(null);
        }
    }, [session, setUser, usersService]);

    const value = { user, setUser };

    return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
}
