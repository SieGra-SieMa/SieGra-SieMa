import { ROLES } from '../../_lib/roles';
import { useAuth } from '../auth/AuthContext';
import { useUser } from '../user/UserContext';

type GuardComponentProp = {
    roles?: ROLES[];
    element?: React.ReactNode;
    placeholder?: React.ReactNode;
    children: React.ReactNode;
}

export default function GuardComponent({
    roles,
    element,
    placeholder,
    children,
}: GuardComponentProp) {

    const { session } = useAuth();
    const { user } = useUser();

    if (!session) {
        return <>{element}</>;
    }

    if (!user) {
        return <>{placeholder || element}</>;
    }

    if (roles && !user.roles.some((role) => roles.includes(role))) {
        return <>{element}</>;
    }

    return <>{children}</>;
}
