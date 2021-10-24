import React, { useState } from 'react';
import './TeamsPanelBlock.css';
import { teamsService } from '../../_services/teams.service';

export default function CreateTeam() {

    const [name, setName] = useState<string>('');

    const onSubmit = () => {
        teamsService.create(name)
            .then(
                _ => alert("OK"),
                error => alert(error)
            )
    }

    return (
        <div className="teams-panel-block">
            <h3>Create a team</h3>
            <p>If you and your friends want to participate in our tournaments, you can create your own team.</p>
            <div>
                <label htmlFor="team-name-input">Team name:</label>
                <input id="team-name-input" type="text" value={name} onChange={e => setName(e.target.value)}/>
            </div>
            <button className="button" style={name.length === 0 ? { visibility: 'collapse'} : {}} onClick={onSubmit}>CREATE</button>
        </div>
    );
}
