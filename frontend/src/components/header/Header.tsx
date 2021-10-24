import React from 'react';
import './Header.css';
import { Link, useHistory } from 'react-router-dom';
import { authenticationService } from '../../_services/authentication.service';

export default function Header() {

    const history = useHistory();
    const user = authenticationService.currentUserValue;


    const logout = () => {
        authenticationService.logout();
        history.push('/accounts/authorize');
    }

    return (
        <header id="header">
            <div className="container header-container">
                <div id="logo-block">
                    <Link to="/">
                        <img src="/logo.png" alt="" />
                    </Link>
                </div>
                <nav id="navigation">
                    <ul>
                        <Link to="/">
                            <li>HOME</li>
                        </Link>
                        <Link to="/teams">
                            <li>TEAMS</li>        
                        </Link>
                        {
                            user ? (
                                <>
                                    <Link to='/account'>
                                        <li>{user.name} {user.surname}</li>
                                    </Link>
                                    <button className="button logout-button" onClick={logout}>Logout</button>
                                </>
                            ) : (
                                <Link to="/account/authorize">
                                    <li>ACCOUNT</li>
                                </Link>
                            )
                        }
                    </ul>
                </nav>
            </div>
        </header>
    );
}