import { Outlet, useNavigate } from 'react-router-dom';
import Button from '../form/Button';
import styles from './AdminPanel.module.css';


export default function AdminPanel() {

    const navigate = useNavigate();

    return (
        <div className="container">
            <div className={styles.navigation}>
                <Button value='Użytkownicy' onClick={() => navigate('users')} />
                <Button value='Zespoły' onClick={() => navigate('teams')} />
            </div>
            <Outlet />
        </div>
    );
}
