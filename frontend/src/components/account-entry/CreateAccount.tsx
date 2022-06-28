import React, { FormEvent, useState } from 'react';
import styles from './AccountEntry.module.css';
import SyncLoader from 'react-spinners/SyncLoader';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useAlert } from '../alert/AlertContext';

export default function CreateAccount() {

    const alert = useAlert();
    const { accountsService } = useApi();

    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);

    const createAccount = (e: FormEvent) => {
        e.preventDefault();
        setLoading(true);
        accountsService.register(name, surname, email, password)
            .then(
                (data) => {
                    setLoading(false);
                    setEmail('');
                    setName('');
                    setSurname('');
                    setPassword('');
                    alert.success(data.message);
                },
                () => setLoading(false)
            );
    };

    return (
        <form className={styles.block} onSubmit={createAccount}>
            <h3>Stwórz konto</h3>
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
                label='Imię'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <Input
                id='CreateAccount-surname'
                label='Nazwisko'
                value={surname}
                required
                onChange={(e) => setSurname(e.target.value)}
            />
            <Input
                id='CreateAccount-password'
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
                <Button value='Zarejestruj się' />
            )}
        </form>
    );
}
