import { useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { authenticationService } from '../../_services/authentication.service';
import AccountEnter from '../account-enter/AccountEnter';

export default function PrivateRoute() {

    const [user, setUser] = useState(authenticationService.currentUserValue);

    useEffect(() => {
        const subscription = authenticationService.currentUser.subscribe(e => setUser(e));
        return () => subscription.unsubscribe();
    }, []);

    if (!user) {
        return <AccountEnter />;
    }

    return <Outlet />;
}
