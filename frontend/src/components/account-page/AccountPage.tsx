import { useEffect } from 'react';
import AccountData from '../account-data/AccountData';
import TeamsList from '../teams-list/TeamsList';
import styles from './AccountPage.module.css';
import { NavLink, Route, Routes } from 'react-router-dom';
import TeamOptions from '../team-options/TeamOptions';

export default function AccountPage() {

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <div>
            <AccountData />
            <div>
                <nav className={styles.navigation}>
                    <ul>
                        <NavLink
                            to="./" 
                            className={({isActive}) => 
                                isActive ? styles.activeLink : ''
                            }
                        >
                            <li>Create / Join a team</li>
                        </NavLink>
                        <NavLink
                            to="myteams/"
                            className={({isActive}) => 
                                isActive ? styles.activeLink : ''
                            }
                        >
                            <li>My teams</li>
                        </NavLink>
                    </ul>
                </nav>
                <div className={styles.content}>
                    <Routes>
                        <Route index element={<TeamOptions />}/>
                        <Route path="myteams" element={<TeamsList />}/>
                        <Route path="*" element={<h2>404 NOT FOUND</h2>}/>
                    </Routes>
                </div>
            </div>
        </div>
    );
}
