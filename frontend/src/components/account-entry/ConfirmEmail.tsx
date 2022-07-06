import { useEffect } from 'react';
import { Navigate, useNavigate, useSearchParams } from 'react-router-dom';
import { useAlert } from '../alert/AlertContext';
import { useApi } from '../api/ApiContext';
import Loader from '../loader/Loader';

export default function ConfirmEmail() {

    const [query] = useSearchParams();
    const token = decodeURIComponent(query.get('token')!);
    const userId = query.get('userid');

    const navigate = useNavigate();

    const alert = useAlert();
    const { accountsService } = useApi();

    useEffect(() => {
        if (!token || !userId) return;
        accountsService.confirmEmail(userId, token)
            .then((data) => {
                navigate('/entry');
                alert.success(data.message);
            });
    }, [token, userId, accountsService, alert, navigate])

    if (!token || !userId) {
        return (<Navigate to='/entry' />);
    }

    return (<Loader size={20} margin={40} />);
}
