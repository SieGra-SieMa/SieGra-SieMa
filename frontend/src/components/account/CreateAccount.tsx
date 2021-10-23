import React, { useState } from 'react';
import './AccountEnter.css';
import { Link, useHistory } from 'react-router-dom';
import { authenticationService } from '../../_services/authentication.service';
import SyncLoader from 'react-spinners/SyncLoader';

export default function CreateAccount() {

    const history = useHistory();

    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);

    const createAccount = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(false);
        setLoading(true);
        authenticationService.register(name, surname, email, password)
            .then(
                _ => history.push('/authorize'), 
                _ => {
                    setError(true);
                    setLoading(false);
                }
            )
    }

	return (
        <div className="account-enter-block">
            <h3>CREATE ACCOUNT</h3>
            { error && <div className="failed">FAILED</div> }
            <form onSubmit={createAccount}>
                <div className="input-block">
                    <label htmlFor="email-input">Email</label>
                    <input id="email-input" className="input-field" type="email"
                        value={email} onChange={e => setEmail(e.target.value)}/>
                </div>
                <div className="input-block">
                    <label htmlFor="name-input">Name</label>
                    <input id="name-input" className="input-field" type="text"
                        value={name} onChange={e => setName(e.target.value)}/>
                </div>
                <div className="input-block">
                    <label htmlFor="surname-input">Surname</label>
                    <input id="surname-input" className="input-field" type="text"
                        value={surname} onChange={e => setSurname(e.target.value)}/>
                </div>
                <div className="input-block">
                    <label htmlFor="password-input">Password</label>
                    <input id="password-input" className="input-field" type="password"
                        value={password} onChange={e => setPassword(e.target.value)}/>
                </div>
                <div className="input-block">
                {
                    loading ? 
                        <div id="account-loader">
                            <SyncLoader loading={true} size={12} margin={20}/>
                        </div> 
                        :
                        <button className="button account-enter-button" type="submit">Create account</button>
                }
                </div>
            </form>

            <span>OR</span>

            <div className="input-block">
                <Link to='/account/authorize' id="other-option">Sign in</Link>
            </div>
        </div>
	);
}
