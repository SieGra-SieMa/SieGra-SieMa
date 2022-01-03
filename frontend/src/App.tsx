import './App.css';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import Home from './components/home/Home';
import PrivateRoute from './components/private-route/PrivateRoute';
import AccountPage from './components/account-page/AccountPage';

export default function App() {
    return (
        <BrowserRouter>
            <Header />
            <Routes>
                <Route index element={<Home />} />

                <Route path="account/*" element={<PrivateRoute />}>
                    <Route path="*" element={<AccountPage />}/>
                </Route>

                {/* <Route path="teams" element={<Outlet />}>
                    <Route index element={<TeamsPanel />} />
                    <Route path="manage" element={<TeamsList />} />
                </Route> */}
            </Routes>
            <Footer />
        </BrowserRouter>
    );
}
