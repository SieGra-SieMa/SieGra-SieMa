import styles from './Header.module.css';
import { Link } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import GuardComponent from '../guard-components/GuardComponent';
import { useUser } from '../user/UserContext';
import { useCallback } from 'react';

export default function Header() {

    const { setSession } = useAuth();
    const { user } = useUser();

    const logout = useCallback(() => setSession(null), [setSession]);

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
                        <li>
                            <Link to="/">
                                HOME
                            </Link>
                        </li>
                        <GuardComponent
                            element={
                                <li>
                                    <Link to="/account/">
                                        ACCOUNT
                                    </Link>
                                </li>
                            }
                            placeholder={
                                <>
                                    <li>
                                        <Link to="/account/">
                                            ACCOUNT
                                        </Link>
                                    </li>
                                    <li>
                                        <button
                                            className={`button ${styles.logout}`}
                                            onClick={logout}
                                        >
                                            Logout
                                        </button>
                                    </li>
                                </>
                            }
                        >
                            <li>
                                <Link to='/account/'>
                                    {user?.name} {user?.surname}
                                </Link>
                            </li>
                            <li>
                                <button
                                    className={`button ${styles.logout}`}
                                    onClick={logout}
                                >
                                    Logout
                                </button>
                            </li>
                        </GuardComponent>
                    </ul>
                </nav>
            </div>
        </header >
    );
}
