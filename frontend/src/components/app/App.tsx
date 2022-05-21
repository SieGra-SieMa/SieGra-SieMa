import './App.css';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import Header from '../header/Header';
import Footer from '../footer/Footer';
import Home from '../home/Home';
import GuardRoute from '../guard-components/GuardRoute';
import AccountPage from '../account-page/AccountPage';
import AuthProvider from '../auth/AuthProvider';
import AccountEntry from '../account-entry/AccountEntry';
import TeamOptions from '../team-options/TeamOptions';
import TeamsList from '../teams/TeamsList';
import TournamentsPage from '../tournaments/TournamentsPage';
import { ROLES } from '../../_lib/roles';
import { ApiContext } from '../api/ApiContext';
import ApiClient from '../../_services';
import UserProvider from '../user/UserProvider';
import TournamentPage from '../tournaments/TournamentPage';

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
                            <Route path="entry" element={<AccountEntry />} />
                            <Route path="access-denied" element={<div>ACCESS DENIED</div>} />
                            <Route path="account/*" element={<AccountPage />}>
                                <Route element={<GuardRoute roles={[ROLES.User, ROLES.Admin]} />}>
                                    <Route index element={<TeamOptions />} />
                                    <Route path="myteams" element={<TeamsList />} />
                                    <Route path="*" element={<h2>404 NOT FOUND</h2>} />
                                </Route>
                            </Route>
                            <Route path="tournaments">
                                <Route index element={<TournamentsPage />} />
                                <Route path=":id" element={<TournamentPage />} />
                            </Route>
                        </Routes>
                        <Footer />
                    </BrowserRouter>
                </UserProvider>
            </AuthProvider>
        </ApiContext.Provider>
    );
}
