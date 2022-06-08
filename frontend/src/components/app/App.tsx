import './App.css';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import Header from '../header/Header';
import Footer from '../footer/Footer';
import Home from '../home/Home';
import AboutUs from '../about-us/AboutUs'
import GuardRoute from '../guard-components/GuardRoute';
import AccountPage from '../account-page/AccountPage';
import AuthProvider from '../auth/AuthProvider';
import AccountEntry from '../account-entry/AccountEntry';
import TeamOptions from '../team-options/TeamOptions';
import TeamsList from '../teams/TeamsList';
import Tournaments from '../tournaments/Tournaments';
import { ROLES } from '../../_lib/roles';
import { ApiContext } from '../api/ApiContext';
import ApiClient from '../../_services';
import UserProvider from '../user/UserProvider';
import TournamentsList from '../tournaments/list/TournamentsList';
import Tournament from '../tournaments/page/Tournament';

const apiClient = new ApiClient();

export default function App() {
    return (
        <ApiContext.Provider value={apiClient}>
            <AuthProvider>
                <UserProvider>
                    <BrowserRouter>
                        <Header />
                        <Routes>
                            <Route index element={<Home />} />
                            <Route path="about-us" element={<AboutUs />} />
                            <Route path="entry" element={<AccountEntry />} />
                            <Route path="access-denied" element={<div>ACCESS DENIED</div>} />
                            <Route path="account/*" element={<AccountPage />}>
                                <Route element={<GuardRoute roles={[ROLES.User, ROLES.Admin]} />}>
                                    <Route index element={<TeamOptions />} />
                                    <Route path="myteams" element={<TeamsList />} />
                                    <Route path="*" element={<h2>404 NOT FOUND</h2>} />
                                </Route>
                            </Route>
                            <Route path="tournaments" element={<Tournaments />}>
                                <Route index element={<TournamentsList />} />
                                <Route path=":id" element={<Tournament />} />
                            </Route>
                        </Routes>
                        <Footer />
                    </BrowserRouter>
                </UserProvider>
            </AuthProvider>
        </ApiContext.Provider>
    );
};
