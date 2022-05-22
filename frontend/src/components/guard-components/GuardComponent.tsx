import { ROLES } from '../../_lib/roles';
import { useAuth } from '../auth/AuthContext';
import { useUser } from '../user/UserContext';

type GuardComponentProp = {
    roles: ROLES[];
    placeholder?: React.ReactNode;
    children: React.ReactNode;
}

export default function GuardComponent({
    roles,
    placeholder,
    children,
}: GuardComponentProp) {

    const { session } = useAuth();
    const { user } = useUser();

    if (!session || !user) {
        return <>{placeholder}</>;
    }

    if (!user.roles.some((role) => roles.includes(role))) {
        return <>{placeholder}</>;
    }

    return <>{children}</>;
}
