import styles from './Header.module.css';
import { Link } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';

export default function Header() {

    const { session, setSession } = useAuth();

    const logout = () => setSession(null);

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
                        {session ? (<>
                            <Link to='/account/'>
                                <li>Username</li>
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
