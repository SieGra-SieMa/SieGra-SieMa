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
import TeamsList from '../teams-list/TeamsList';
import TournamentsPage from '../tournaments/TournamentsPage';
import { ROLES } from '../../_lib/roles';
import { ApiContext } from '../api/ApiContext';
import services from '../../_services';

export default function App() {
    return (
        <ApiContext.Provider value={services}>
            <AuthProvider>
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
                                <Route path="tournaments" element={<TournamentsPage />} />
                                <Route path="*" element={<h2>404 NOT FOUND</h2>} />
                            </Route>
                        </Route>
                    </Routes>
                    <Footer />
                </BrowserRouter>
            </AuthProvider>
        </ApiContext.Provider>
    );
}
