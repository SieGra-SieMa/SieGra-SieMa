import { useContext, createContext } from 'react';
import { User } from '../../_lib/types';

interface UserContextType {
    user: User | null;
    setUser: (user: User | null) => void;
}

export const UserContext = createContext<UserContextType>(null!);

export function useUser() {
    return useContext(UserContext);
}
