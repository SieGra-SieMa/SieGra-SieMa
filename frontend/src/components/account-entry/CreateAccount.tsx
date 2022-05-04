import React, { useState } from 'react';
import styles from './AccountEntry.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import { accountService } from '../../_services/accounts.service';
import InputField from '../form/InputField';

export default function CreateAccount() {

    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const createAccount = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        accountService.register(name, surname, email, password)
            .then(
                () => { },
                (e) => {
                    setError(e.message || e);
                    setLoading(false);
                }
            );
    };

    return (
        <div className={styles.enterBlock}>
            <h3>CREATE ACCOUNT</h3>
            <form onSubmit={createAccount}>
                {error && <div className={styles.failed}>{error}</div>}
                <InputField
                    id='CreateAccount-email'
                    label='Email'
                    type='email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <InputField
                    id='CreateAccount-name'
                    label='Name'
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <InputField
                    id='CreateAccount-surname'
                    label='Surname'
                    value={surname}
                    onChange={(e) => setSurname(e.target.value)}
                />
                <InputField
                    id='CreateAccount-password'
                    label='Password'
                    type='password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <div className={styles.spacing}></div>
                {loading ? (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={7} margin={20} />
                    </div>
                ) : (
                    <button
                        className={`${styles.enterButton} button`}
                        type="submit"
                    >
                        Create account
                    </button>
                )}
            </form>
        </div>
    );
}
