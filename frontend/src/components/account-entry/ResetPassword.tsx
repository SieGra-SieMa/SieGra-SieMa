import { useState, FormEvent } from 'react';
import { Navigate, useNavigate, useSearchParams } from 'react-router-dom';
import { useApi } from '../api/ApiContext';
import Input from '../form/Input';
import Button from '../form/Button';
import Form from '../form/Form';
import { useAlert } from '../alert/AlertContext';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './AccountEntry.module.css';

export default function ResetPassword() {

    const [query] = useSearchParams();
    const token = decodeURIComponent(query.get('token')!);
    const userId = query.get('userid');

    const navigate = useNavigate();

    const alert = useAlert();
    const { accountsService } = useApi();

    const [password, setPassword] = useState<string>('');

    const resetPassword = (e: FormEvent) => {
        e.preventDefault();
        if (!token || !userId) return;
        return accountsService.resetPassword(userId!, token, password)
            .then((data) => {
                navigate('/entry');
                alert.success(data.message);
            });
    };

    if (!token || !userId) {
        return <Navigate to='/entry' />;;
    }

    return (
        <div className={[
            'container',
            styles.resetPasswordRoot,
        ].join(' ')}>
            <h1>Zmiana hasła</h1>
            <div className={styles.resetPasswordContainer}>
                <Form onSubmit={resetPassword} trigger={<>
                    <VerticalSpacing size={15} />
                    <Button value='Zresetuj' />
                </>}>
                    <Input
                        id='ResetPassword-password'
                        label='Nowe hasło'
                        type='password'
                        value={password}
                        required
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </Form>
            </div>
        </div>
    );
}
