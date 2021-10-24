import React, { useEffect, useState } from 'react';
import './Header.css';
import { Link, useHistory } from 'react-router-dom';
import { authenticationService } from '../../_services/authentication.service';
import { Account } from '../../_lib/types';

export default function Header() {

    const history = useHistory();
    const userObservable = authenticationService.currentUser;

    const [user, setUser] = useState<Account | null>(null);

    useEffect(() => {
        const subscription = userObservable.subscribe((user) => {
            console.log(user)
            setUser(user);
        });
        return () => { 
            subscription.unsubscribe();
        };
    }, [userObservable])

    const logout = () => {
        authenticationService.logout();
        history.push('/account/authorize');
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
                        {
                            user ? (
                                <>
                                    <Link to="/teams">
                                        <li>TEAMS</li>        
                                    </Link>
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