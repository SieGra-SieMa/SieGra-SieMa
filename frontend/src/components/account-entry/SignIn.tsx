import { FormEvent, useState } from 'react';
import styles from './AccountEntry.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import { useAuth } from '../auth/AuthContext';
import Input from '../form/Input';
import { useNavigate } from 'react-router-dom';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';
import Modal from '../modal/Modal';
import ForgetPassword from './ForgetPassword';

export default function SignIn() {

    const { accountsService } = useApi();

    const navigate = useNavigate();

    const { setSession } = useAuth();

    const [email, setEmail] = useState('kapitan@gmail.com');
    const [password, setPassword] = useState('Haslo+123');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const [isForget, setIsForget] = useState(false);


    const signIn = (e: FormEvent) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        accountsService.authenticate(email, password)
            .then(
                (session) => {
                    setSession(session);
                    navigate('/account');
                },
                (e) => {
                    setError(e);
                    setLoading(false);
                }
            );
    };


    return (
        <form className={styles.block} onSubmit={signIn}>
            <h3>Zaloguj się</h3>
            {error && <div className={styles.failed}>FAILED: {error}</div>}
            <Input
                id='SignIn-email'
                label='Email'
                type='email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <Input
                id='SignIn-password'
                label='Hasło'
                type='password'
                value={password}
                required
                onChange={(e) => setPassword(e.target.value)}
            />
            <VerticalSpacing size={30} />
            {loading ? (
                <div className={styles.loader}>
                    <SyncLoader loading={true} size={7} margin={20} color='#fff' />
                </div>
            ) : (
                <Button value='Zaloguj się' />
            )}
            <Button
                onClick={() => setIsForget(true)}
                type='button'
                value='Zapomniałeś hasło?'
                style={ButtonStyle.DarkBlue}
            />
            {(isForget) && (
                <Modal
                    isClose
                    title='Zresetuj hasło'
                    close={() => setIsForget(false)}
                >
                    <ForgetPassword
                        confirm={() => setIsForget(false)}
                    />
                </Modal>
            )}
        </form>
    );
}
