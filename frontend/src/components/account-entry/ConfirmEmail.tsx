import { useEffect, useState } from 'react';
import { Navigate, useSearchParams } from 'react-router-dom';
import { useApi } from '../api/ApiContext';


export default function ConfirmEmail() {

    const [query] = useSearchParams();

    const { accountsService } = useApi();

    const [isConfirmed, setIsConfirmed] = useState(false);

    const token = query.get('token');
    const userId = query.get('userid');

    useEffect(() => {
        if (!token || !userId) return;
        accountsService.confirmEmail(userId, token)
            .then((data) => {
                setIsConfirmed(true);
            })
    }, [token, userId, accountsService])

    if (!token || !userId) {
        return <Navigate to='/entry' />;;
    }

    return (
        <div className='container'>
            <h1>{isConfirmed ? 'Konto zostało potwierdzone' : 'Potwierdzenie...'}</h1>
        </div>
    );
}
