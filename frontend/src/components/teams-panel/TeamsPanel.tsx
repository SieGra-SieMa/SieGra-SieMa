import React from 'react';
import './TeamsPanel.css';
import JoinTeam from '../../components/teams-panel/JoinTeam';
import CreateTeam from '../../components/teams-panel/CreateTeam';
import ManageTeams from '../../components/teams-panel/ManageTeams';

export default function TeamsPanel() {
    return (
        <div className="content-section">
            <div className="container teams-panel-container">
                <JoinTeam />
                <CreateTeam />
                <ManageTeams />
            </div>
        </div>
    );
}
