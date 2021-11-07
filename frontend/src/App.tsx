import React from 'react';
import './App.css';
import {
    BrowserRouter as Router,
    Switch,
    Route,
} from 'react-router-dom';
import CreateAccount from './components/account/CreateAccount';
import SignIn from './components/account/SignIn';
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import TeamsPanel from './components/teams-panel/TeamsPanel';
import TeamsList from './components/teams-list/TeamsList';
import Home from './components/home/Home';
import AccountProfile from './components/account/AccountProfile';

export default function App() {
    return (
        <Router>
            <Switch>
                <Header />
            </Switch>
            <Switch>
                <Route path="/account/register">
                    <CreateAccount />
                </Route>
                <Route path="/account/authorize">
                    <SignIn />
                </Route>
                <Route path="/account">
                    <AccountProfile />
                </Route>
                <Route path="/teams/manage">
                    <TeamsList />
                </Route>
                <Route path="/teams">
                    <TeamsPanel />
                </Route>
                <Route path="/">
                    <Home />
                </Route>
            </Switch>
            <Switch>
                <Footer />
            </Switch>
        </Router>
    );
}