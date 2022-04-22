import { ROLES } from '../../_lib/roles';
import { useAuth } from '../auth/AuthContext';

type GuardComponentProp = {
    roles?: ROLES[];
    element?: React.ReactNode;
    children: React.ReactNode;
}

export default function GuardComponent({
    roles,
    element,
    children,
}: GuardComponentProp) {

    const { session } = useAuth();

    if (!session) {
        return <>{element}</>;
    }

    if (roles && !roles.includes(session.role)) {
        return <>{element}</>;
    }

    return <>{children}</>;
}
