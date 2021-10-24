import { useState } from 'react';
import { authenticationService } from '../../_services/authentication.service';
import './Account.css';

export default function Account() {

    const user = authenticationService.currentUserValue!;

    const [isEditing, setIsEditing] = useState(false);
    const [email, setEmail] = useState(user.email);
    const [name, setName] = useState(user.name);
    const [surname, setSurname] = useState(user.surname);

    const onSave = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsEditing(false);
    }

	return (
        <div className="container account-container">
            <h1>Account Details</h1>
            <div className="account-details">
                {isEditing ? (
                    <form onSubmit={onSave}>
                        <div className="input-block">
                            <label htmlFor="email-input">Email</label>
                            <input id="email-input" className="input-field" type="email"
                                value={email} onChange={e => setEmail(e.target.value)}/>
                        </div>
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
                                setEmail(user.email)
                                setName(user.name)
                                setSurname(user.surname)
                                setIsEditing(false)
                            }}>Cancel</button>
                            <button className="button account-button" type="submit">Save</button>
                        </div>
                    </form>
                ) : (<>
                    <p><b>Email</b></p>
                    <p>{email}</p>
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
