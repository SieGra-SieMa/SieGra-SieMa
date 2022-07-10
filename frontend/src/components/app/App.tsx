import './App.css';
import AuthProvider from '../auth/AuthProvider';
import ApiClient from '../../_services';
import UserProvider from '../user/UserProvider';
import AlertProvider from '../alert/AlertProvider';
import ApiProvider from '../api/ApiProvider';
import AppRoutes from './AppRoutes';

const apiClient = new ApiClient();

export default function App() {
    return (
        <AlertProvider options={{
            errorTimeout: 3000,
            successTimeout: 7000
        }}>
            <ApiProvider value={apiClient}>
                <AuthProvider>
                    <UserProvider>
                        <AppRoutes />
                    </UserProvider >
                </AuthProvider >
            </ApiProvider>
        </AlertProvider>
    );
};
