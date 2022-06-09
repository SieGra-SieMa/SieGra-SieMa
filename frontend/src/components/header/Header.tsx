import styles from './Header.module.css';
import { Link } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import { useUser } from '../user/UserContext';
import { useCallback } from 'react';
import Button from '../form/Button';

export default function Header() {

    const { session, setSession } = useAuth();
    const { user } = useUser();

    const logout = useCallback(() => setSession(null), [setSession]);

    return (
        <header className={styles.root}>
            <div className={`container ${styles.container}`}>
                <div className={styles.logo}>
                    <Link to="/">
                        <img src="/logo_w.png" alt="" />
                    </Link>
                </div>
                <nav className={styles.navigation}>
                    <ul>
                        <li>
                            <Link to="/">
                                Strona główna
                            </Link>
                        </li>
                        <li>
                            <Link to="/tournaments">
                                Turnieje
                            </Link>
                        </li>
                        <li>
                            <Link to="/about-us">
                                O nas
                            </Link>
                        </li>
                        {session ? (
                            <>
                                <li>
                                    <Link to='/account'>
                                        {user ? `${user.name} ${user.surname}` : 'USERNAME'}
                                    </Link>
                                </li>
                                <li>
                                    <Button value='Wyloguj' type='button' onClick={logout} />
                                </li>
                            </>
                        ) : (
                            <li>
                                <Link to="/account">
                                    Profil
                                </Link>
                            </li>
                        )}
                    </ul>
                </nav>
            </div>
        </header >
    );
}
