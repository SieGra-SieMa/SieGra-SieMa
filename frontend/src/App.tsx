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
import TeamsPanel from './containers/teams-panel/TeamsPanel';
import TeamsList from './containers/teams-list/TeamsList';
import Home from './containers/home/Home';
import Account from './components/account/Account';

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
                    <Account />
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