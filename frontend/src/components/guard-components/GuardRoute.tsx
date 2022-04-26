import { Navigate, Outlet } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { useAuth } from '../auth/AuthContext';

type GuardRouteProp = {
    roles?: ROLES[];
}

export default function GuardRoute({
    roles
}: GuardRouteProp) {

    const { session } = useAuth();

    if (!session) {
        return <Navigate to='/entry' />;
    }

    if (!roles?.includes(session.role)) {
        return <Navigate to='/access-denied' />;
    }

    return <Outlet />;
}
