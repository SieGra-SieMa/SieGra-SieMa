import React, { FormEvent, useState } from 'react';
import styles from './AccountEntry.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';

export default function CreateAccount() {

    const { accountsService } = useApi();

    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const createAccount = (e: FormEvent) => {
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
        <form className={styles.block} onSubmit={createAccount}>
            <h3>Create account</h3>
            {error && <div className={styles.failed}>{error}</div>}
            <Input
                id='CreateAccount-email'
                label='Email'
                type='email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <Input
                id='CreateAccount-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <Input
                id='CreateAccount-surname'
                label='Surname'
                value={surname}
                required
                onChange={(e) => setSurname(e.target.value)}
            />
            <Input
                id='CreateAccount-password'
                label='Password'
                type='password'
                value={password}
                required
                onChange={(e) => setPassword(e.target.value)}
            />
            <div className={styles.spacing}></div>
            {loading ? (
                <div className={styles.loader}>
                    <SyncLoader loading={true} size={7} margin={20} />
                </div>
            ) : (
                <Button value='Create account' />
            )}
        </form>
    );
}
