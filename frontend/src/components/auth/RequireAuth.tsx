import AccountEnter from '../account-enter/AccountEnter';
import { useAuth } from './AuthContext';

export default function RequireAuth({ children }: { children: JSX.Element }) {

    const { session } = useAuth();

    if (!session) {
        return <AccountEnter />;
    }

    return children;
}
