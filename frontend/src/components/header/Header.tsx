import { useEffect, useState } from 'react';
import styles from './Header.module.css';
import { Link } from 'react-router-dom';
import { authenticationService } from '../../_services/authentication.service';
import { Account } from '../../_lib/types';

export default function Header() {

    const userObservable = authenticationService.currentUser;

    const [user, setUser] = useState<Account | null>(null);

    useEffect(() => {
        const subscription = userObservable.subscribe((user) => setUser(user));
        return () => subscription.unsubscribe();
    }, [userObservable]);

    const logout = () => {
        authenticationService.logout();
    };

    return (
        <header className={styles.root}>
            <div className={`container ${styles.container}`}>
                <div className={styles.logo}>
                    <Link to="/">
                        <img src="/logo.png" alt="" />
                    </Link>
                </div>
                <nav className={styles.navigation}>
                    <ul>
                        <Link to="/">
                            <li>HOME</li>
                        </Link>
                        {user ? (<>
                            <Link to='/account/'>
                                <li>{user.name} {user.surname}</li>
                            </Link>
                            <button className={`button ${styles.logout}`} onClick={logout}>Logout</button>
                        </>) : (
                            <Link to="/account/">
                                <li>ACCOUNT</li>
                            </Link>
                        )}
                    </ul>
                </nav>
            </div>
        </header>
    );
}
