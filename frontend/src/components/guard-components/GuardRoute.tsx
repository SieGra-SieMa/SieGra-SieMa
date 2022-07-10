import { Navigate, Outlet } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { useAuth } from '../auth/AuthContext';
import Loader from '../loader/Loader';
import { useUser } from '../user/UserContext';

type GuardRouteProp = {
    roles: ROLES[];
}

export default function GuardRoute({
    roles
}: GuardRouteProp) {

    const { session } = useAuth();
    const { user } = useUser();

    if (!session) {
        return <Navigate to='/entry' />;
    }

    if (!user) {
        return <Loader size={20} margin={40} />;
    }

    if (!user.roles.some((role) => roles.includes(role))) {
        return <Navigate to='/access-denied' />;
    }

    return <Outlet />;
}
