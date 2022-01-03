import React, { useState } from 'react';
import styles from './AccountEnter.module.css';
import { authenticationService } from '../../_services/authentication.service';
import SyncLoader from 'react-spinners/SyncLoader';

export default function SignIn() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);

    const signIn = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(false);
        setLoading(true);
        authenticationService.authenticate(email, password)
            .then(
                _ => {},
                _ => {
                    setError(true);
                    setLoading(false);
                }
            );
    };

	return (
        <div className={styles.enterBlock}>
            <h3>SIGN IN</h3>
            {error && <div className={styles.failed}>FAILED</div>}
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
