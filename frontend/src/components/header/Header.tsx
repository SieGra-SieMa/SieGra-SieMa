import styles from './Header.module.css';
import { Link } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import { useUser } from '../user/UserContext';
import { useCallback, useState } from 'react';
import Button from '../form/Button';
import MenuIcon from '@mui/icons-material/Menu';
import CloseIcon from '@mui/icons-material/Close';

export default function Header() {

    const { session, setSession } = useAuth();
    const { user } = useUser();
    const [toggleMenu, setToggleMenu] = useState(false);
    const [navState, setNavState] = useState(`${styles.navClosed}`);

    const logout = useCallback(() => setSession(null), [setSession]);

    const toggleNav = () => {
        setToggleMenu(!toggleMenu);
        setNavState( !toggleMenu ? `${styles.navOpen}` : `${styles.navClosed}`);
    };

    return (
        <header className={styles.root}>
            <div className={`container ${styles.container}`}>
                <div className={styles.logo}>
                    <Link to="/">
                        <img src="/logo_w.png" alt="" />
                    </Link>
                </div>
                <nav className={styles.navigation} id={ navState }>
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
                <button className={styles.menu} onClick={toggleNav}>{ !toggleMenu ? <MenuIcon fontSize="large"/> : <CloseIcon fontSize="large"/> }</button>
            </div>
        </header >
    );
}
