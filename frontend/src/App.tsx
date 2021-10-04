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
import TeamsPanel from './containers/teams-panel/TeamsPanel';
import TeamsList from './containers/teams-list/TeamsList';
import Home from './containers/home/Home';

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
        </Router>
    );
}