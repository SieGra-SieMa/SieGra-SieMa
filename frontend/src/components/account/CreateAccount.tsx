import React, { useState } from 'react';
import styles from './AccountEnter.module.css';
import { useNavigate } from 'react-router-dom';
import { authenticationService } from '../../_services/authentication.service';
import SyncLoader from 'react-spinners/SyncLoader';

export default function CreateAccount() {

    const navigate = useNavigate();

    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);

    const createAccount = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(false);
        setLoading(true);
        authenticationService.register(name, surname, email, password)
            .then(
                _ => navigate('../authorize'), 
                _ => {
                    setError(true);
                    setLoading(false);
                }
            );
    };

	return (
        <div className={styles.enterBlock}>
            <h3>CREATE ACCOUNT</h3>
            {error && <div className={styles.failed}>FAILED</div>}
            <form onSubmit={createAccount}>
                <div className="input-block">
                    <label htmlFor="CreateAccount-email">Email</label>
                    <input 
                        id="CreateAccount-email" 
                        className="input-field" 
                        type="email"
                        value={email} 
                        onChange={e => setEmail(e.target.value)}
                    />
                </div>
                <div className="input-block">
                    <label htmlFor="CreateAccount-name">Name</label>
                    <input 
                        id="CreateAccount-name"
                        className="input-field"
                        type="text"
                        value={name} 
                        onChange={e => setName(e.target.value)}
                    />
                </div>
                <div className="input-block">
                    <label htmlFor="CreateAccount-surname">Surname</label>
                    <input 
                        id="CreateAccount-surname"
                        className="input-field"
                        type="text"
                        value={surname}
                        onChange={e => setSurname(e.target.value)}
                    />
                </div>
                <div className="input-block">
                    <label htmlFor="CreateAccount-password">Password</label>
                    <input
                        id="CreateAccount-password"
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
                            Create account
                        </button>
                    )}
                </div>
            </form>
        </div>
	);
}
