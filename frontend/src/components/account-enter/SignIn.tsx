import React, { useState } from 'react';
import styles from './AccountEnter.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import { useAuth } from '../auth/AuthContext';
import { accountService } from '../../_services/accounts.service';

export default function SignIn() {

    const { saveSession } = useAuth();

    const [email, setEmail] = useState('gracz@gmail.com');
    const [password, setPassword] = useState('haslo123');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const signIn = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        accountService.authenticate(email, password)
            .then(
                session => saveSession(session),
                e => {
                    setError(e);
                    setLoading(false);
                }
            );
    };

	return (
        <div className={styles.enterBlock}>
            <h3>SIGN IN</h3>
            {error && <div className={styles.failed}>FAILED: { error }</div>}
            <form onSubmit={signIn}>
                <div className="input-block">
                    <label htmlFor="SignIn-email">Email</label>
                    <input
                        id="SignIn-email" 
                        className="input-field" 
                        type="email"
                        value={email} 
                        onChange={e => setEmail(e.target.value)}
                    />
                </div>
                <div className="input-block">
                    <label htmlFor="SignIn-password">Password</label>
                    <input
                        id="SignIn-password"
                        className="input-field"
                        type="password"
                        value={password} 
                        onChange={e => setPassword(e.target.value)}
                    />
                </div>
                <div className="input-block">
                    {loading ? (
                        <div className={styles.loader}>
                            <SyncLoader loading={true} size={12} margin={20}/>
                        </div>
                    ) : (
                        <button
                            className={`${styles.enterButton} button`} 
                            type="submit"
                        >
                            Sign in
                        </button>
                    )}
                </div>
            </form>
        </div>
	);
}
