import React, { useState } from 'react';
import styles from './AccountEnter.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import { useAuth } from '../auth/AuthContext';
import { accountService } from '../../_services/accounts.service';
import InputField from '../form/InputField';

export default function SignIn() {

    const { setSession } = useAuth();

    const [email, setEmail] = useState('gracz@gmail.com');
    const [password, setPassword] = useState('haslo123');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const signIn = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        accountService.authenticate(email, password)
            .then(
                session => setSession(session),
                e => {
                    setError(e.message || e);
                    setLoading(false);
                }
            );
    };

    return (
        <div className={styles.enterBlock}>
            <h3>SIGN IN</h3>
            <form onSubmit={signIn}>
                {error && <div className={styles.failed}>FAILED: {error}</div>}
                <InputField
                    id='SignIn-email'
                    label='Email'
                    type='email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <InputField
                    id='SignIn-password'
                    label='Password'
                    type='password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                {loading ? (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={7} margin={20} />
                    </div>
                ) : (
                    <button
                        className="button"
                        type="submit"
                    >
                        Sign in
                    </button>
                )}
            </form>
        </div>
    );
}
