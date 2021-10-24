import React from 'react';
import './TeamsPanelBlock.css';
import { Link } from 'react-router-dom';

export default function ManageTeams() {
    return (
        <div className="teams-panel-block">
            <h3>Manage your own teams</h3>
            <p>Edit your teams.</p>
            <Link className="button" to='/teams/manage'>MANAGE</Link>
        </div>
    );
}
