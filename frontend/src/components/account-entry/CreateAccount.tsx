import { FormEvent, useState } from 'react';
import styles from './AccountEntry.module.css';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useAlert } from '../alert/AlertContext';
import Loader from '../loader/Loader';
import Form from '../form/Form';

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
        <Form className={styles.block} onSubmit={createAccount}>
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
                <Loader />
            ) : (
                <Button value='Zarejestruj się' />
            )}
        </Form>
    );
}
