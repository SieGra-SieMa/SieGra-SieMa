import React, { useState } from 'react';
import styles from './AccountEntry.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import InputField from '../form/InputField';
import { useApi } from '../api/ApiContext';

export default function CreateAccount() {

    const { accountsService } = useApi();

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
        accountsService.register(name, surname, email, password)
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
            <h3>Create account</h3>
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
