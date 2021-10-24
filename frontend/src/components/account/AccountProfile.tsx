import { useState } from 'react';
import { useHistory } from 'react-router';
import { authenticationService } from '../../_services/authentication.service';
import { usersService } from '../../_services/users.service';
import './AccountProfile.css';

export default function AccountProfile() {

    const history = useHistory();

    const user = authenticationService.currentUserValue!;

    const [isEditing, setIsEditing] = useState(false);
    const [name, setName] = useState(user.name);
    const [surname, setSurname] = useState(user.surname);

    if (!user) {
        history.push('/account/authorize');
        return null;
    }

    const onSave = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        usersService.update({
            name, 
            surname,
        }).then(
            (result) => {
                setIsEditing(false);
                authenticationService.setCurrentUser({ ...user, name, surname });
            },
            (error) => alert(error)
        )
    }

	return (
        <div className="container account-container">
            <h1>Account Details</h1>
            <div className="account-details">
                <p><b>Email</b></p>
                <p>{user.email}</p>
                {isEditing ? (
                    <form onSubmit={onSave}>
                        <div className="input-block">
                            <label htmlFor="name-input">Name</label>
                            <input id="name-input" className="input-field"
                                value={name} onChange={e => setName(e.target.value)}/>
                        </div>
                        <div className="input-block">
                            <label htmlFor="surname-input">Surname</label>
                            <input id="surname-input" className="input-field"
                                value={surname} onChange={e => setSurname(e.target.value)}/>
                        </div>
                        <div className="input-controls">
                            <button className="button account-button" onClick={() => {
                                setName(user.name)
                                setSurname(user.surname)
                                setIsEditing(false)
                            }}>Cancel</button>
                            <button className="button account-button" type="submit">Save</button>
                        </div>
                    </form>
                ) : (<>
                    <br />
                    <p><b>Name</b></p>
                    <p>{name}</p>
                    <br />
                    <p><b>Surname</b></p>
                    <p>{surname}</p>
                    <button className="button account-button" onClick={() => setIsEditing(true)}>Edit</button>
                </>)}
            </div>
        </div>
	);
}
