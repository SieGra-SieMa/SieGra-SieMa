import { useHistory } from 'react-router';
import { authenticationService } from '../../_services/authentication.service';
import './Account.css';

export default function Account() {

    const history = useHistory();

    const user = authenticationService.currentUserValue;

    if (!user) {
        history.push('/');
        return null;
    }

    const logout = () => {
        authenticationService.logout();
        history.push('/');
    }

	return (
        <div className="container">
            <div className="account-container">
                <h2>Account Details</h2>

                <p><b>Name</b></p>
                <p>{user.name}</p>

                <br />

                <p><b>Surname</b></p>
                <p>{user.surname}</p>

                <button className="button logout-button" onClick={logout}>Logout</button>
            </div>
        </div>
	);
}
