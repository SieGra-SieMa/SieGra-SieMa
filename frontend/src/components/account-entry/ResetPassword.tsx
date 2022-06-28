import { useEffect, useState, FormEvent } from 'react';
import { Navigate, useSearchParams } from 'react-router-dom';
import { useApi } from '../api/ApiContext';
import Input from '../form/Input';
import Button from '../form/Button';


export default function ResetPassword() {

    const [query] = useSearchParams();

    const { accountsService } = useApi();

    const [isConfirmed, setIsConfirmed] = useState(false);
    const [password, setPassword] = useState<string>('');
    const [error, setError] = useState<string | null>(null);

    const token = decodeURIComponent(query.get('token')!);
    const userId = query.get('userid');

    useEffect(() => {
        if (!token || !userId) return;
    }, [token, userId, accountsService])

    const resetPassword = (e: FormEvent) => {
        e.preventDefault();
        console.log(token)
        if (!token || !userId) return;
        accountsService.resetPassword(userId!, token, password)
            .then((data) => {
                setIsConfirmed(true);
            },(e) => {
                setError(e);
            })
    };

    if (!token || !userId) {
        return <Navigate to='/entry' />;;
    }

    return (
        <div className='container'>
            <h1>{isConfirmed ? 'Hasło zostało zresetowane!' : 'Resetowanie...'}</h1>
            {error &&<h2>FAILED: {error}</h2>}
            <form onSubmit={resetPassword}>
            <Input
                id='Reset-password'
                label='Password'
                type='password'
                value={password}
                required
                onChange={(e) => setPassword(e.target.value)}
            />
            <Button value='Zresetuj hasło' />
            </form>
        </div>
    );
}
