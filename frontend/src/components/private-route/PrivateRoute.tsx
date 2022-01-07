import { useEffect, useState } from 'react';
import { authenticationService } from '../../_services/authentication.service';
import AccountEnter from '../account-enter/AccountEnter';

export default function PrivateRoute({ children }: { children: JSX.Element }) {

    const [user, setUser] = useState(authenticationService.currentUserValue);

    useEffect(() => {
        const subscription = authenticationService.currentUser.subscribe(e => setUser(e));
        return () => subscription.unsubscribe();
    }, []);

    if (!user) {
        return <AccountEnter />;
    }

    return children;
}
