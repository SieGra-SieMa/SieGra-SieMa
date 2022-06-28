import './App.css';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import Header from '../header/Header';
import Footer from '../footer/Footer';
import Home from '../home/Home';
import AboutUs from '../about-us/AboutUs';
import GalleryList from '../gallery/GalleryList';
import AlbumsList from '../gallery/AlbumsList';
import GuardRoute from '../guard-components/GuardRoute';
import AuthProvider from '../auth/AuthProvider';
import AccountEntry from '../account-entry/AccountEntry';
import TeamsList from '../teams/TeamsList';
import Tournaments from '../tournaments/Tournaments';
import { ROLES } from '../../_lib/roles';
import { ApiContext } from '../api/ApiContext';
import ApiClient from '../../_services';
import UserProvider from '../user/UserProvider';
import TournamentsList from '../tournaments/list/TournamentsList';
import UsersList from '../admin/users/UsersList';
import Tournament from '../tournaments/page/Tournament';
import Album from '../gallery/Album';
import AccountData from '../profile/Profile';
import AdminPanel from '../admin/AdminPanel';
import AdminTeamsList from '../admin/teams/AdminTeamsList';
import Newsletter from '../admin/Newsletter';
import ConfirmEmail from '../account-entry/ConfirmEmail';
import ResetPassword from '../account-entry/ResetPassword'

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
                            <Route path="access-denied" element={
                                <div style={{ color: '#fff' }}>ACCESS DENIED</div>
                            } />
                            <Route path="account" element={
                                <GuardRoute roles={[ROLES.User, ROLES.Employee, ROLES.Admin]} />
                            }>
                                <Route index element={<AccountData />} />
                            </Route>
                            <Route path="myteams" element={
                                <GuardRoute roles={[ROLES.User, ROLES.Employee, ROLES.Admin]} />
                            }>
                                <Route index element={<TeamsList />} />
                            </Route>
                            <Route path="admin" element={
                                <GuardRoute roles={[ROLES.Admin]} />
                            }>
                                <Route path='' element={<AdminPanel />} >
                                    <Route index element={<Newsletter />} />
                                    <Route path="users" element={<UsersList />} />
                                    <Route path="teams" element={<AdminTeamsList />} />
                                </Route>
                            </Route>
                            <Route path="*" element={<Tournaments />}>
                                <Route path="tournaments">
                                    <Route index element={<TournamentsList />} />
                                    <Route path=":id" element={<Tournament />} />
                                </Route>
                                <Route path="gallery">
                                    <Route index element={<GalleryList />} />
                                    <Route path=":id/albums">
                                        <Route index element={<AlbumsList />} />
                                        <Route path=":albumId" element={<Album />} />
                                    </Route>
                                </Route>
                            </Route>
                            <Route path='email-confirmation' element={<ConfirmEmail />} />
                            <Route path='reset-password' element={<ResetPassword />} />
                        </Routes >
                        <Footer />
                    </BrowserRouter >
                </UserProvider >
            </AuthProvider >
        </ApiContext.Provider >
    );
};
