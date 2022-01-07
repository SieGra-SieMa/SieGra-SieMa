import './App.css';
import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom';
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import Home from './components/home/Home';
import PrivateRoute from './components/auth/RequireAuth';
import AccountPage from './components/account-page/AccountPage';
import AuthProvider from './components/auth/AuthProvider';

export default function App() {
    return (
        <AuthProvider>
            <BrowserRouter>
                <Header />
                <Routes>
                    <Route index element={<Home />} />
                    <Route path="account/*" element={
                        <PrivateRoute>
                            <AccountPage />
                        </PrivateRoute>
                    }/>
                </Routes>
                <Footer />
            </BrowserRouter>
        </AuthProvider>
    );
}
