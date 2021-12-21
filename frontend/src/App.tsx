import './App.css';
import {
    BrowserRouter,
    Routes,
    Route,
    Outlet,
} from 'react-router-dom';
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import TeamsPanel from './components/teams-panel/TeamsPanel';
import TeamsList from './components/teams-list/TeamsList';
import Home from './components/home/Home';
import AccountEnter from './components/account/AccountEnter';

export default function App() {
    return (
        <BrowserRouter>
            <Header />
            <Routes>
                <Route path="/" element={<Home />} />

                <Route path="account" element={<AccountEnter />} />

                <Route path="teams" element={<Outlet />}>
                    <Route index element={<TeamsPanel />} />
                    <Route path="manage" element={<TeamsList />} />
                </Route>
            </Routes>
            <Footer />
        </BrowserRouter>
    );
}
