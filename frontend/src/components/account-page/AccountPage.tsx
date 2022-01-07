import { useEffect } from 'react';
import AccountData from '../account-data/AccountData';
import TeamsList from '../teams-list/TeamsList';
import styles from './AccountPage.module.css';
import { NavLink, Route, Routes } from 'react-router-dom';
import TeamOptions from '../team-options/TeamOptions';
import TournamentsPage from '../tournaments/TournamentsPage';

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
                                to="./" 
                                className={({isActive}) => 
                                    isActive ? styles.activeLink : ''
                                }
                            >
                                Create / Join a team
                            </NavLink>
                        </li>
                        <li>
                            <NavLink
                                to="myteams/" 
                                className={({isActive}) => 
                                    isActive ? styles.activeLink : ''
                                }
                            >
                                My teams
                            </NavLink>
                        </li>
                        <li>
                            <NavLink
                                to="tournaments/" 
                                className={({isActive}) => 
                                    isActive ? styles.activeLink : ''
                                }
                            >
                                Tournaments
                            </NavLink>
                        </li>
                    </ul>
                </nav>
                <div className={styles.content}>
                    <Routes>
                        <Route index element={<TeamOptions />}/>
                        <Route path="myteams" element={<TeamsList />}/>
                        <Route path="tournaments" element={<TournamentsPage />}/>
                        <Route path="*" element={<h2>404 NOT FOUND</h2>}/>
                    </Routes>
                </div>
            </div>
        </div>
    );
}
