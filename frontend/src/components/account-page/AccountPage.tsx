import { useEffect } from 'react';
import AccountData from '../account-data/AccountData';
import styles from './AccountPage.module.css';
import { NavLink, Outlet } from 'react-router-dom';

export default function AccountPage() {

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <div>
            <AccountData />
            <div>
                <nav className={styles.navigation}>
                    <ul className="container">
                        <li>
                            <NavLink
                                to="."
                                end
                                className={({ isActive }) => isActive ? styles.activeLink : ''}
                            >
                                Create / Join a team
                            </NavLink>
                        </li>
                        <li>
                            <NavLink
                                to="myteams"
                                className={({ isActive }) => isActive ? styles.activeLink : ''}
                            >
                                My teams
                            </NavLink>
                        </li>
                    </ul>
                </nav>
                <div className={styles.content}>
                    <Outlet />
                </div>
            </div>
        </div>
    );
}
