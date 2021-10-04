import React, { useState } from 'react';
import './TeamsPanelBlock.css';
import { teamsService } from '../../_services/teams.service';

export default function JoinTeam() {

    const [code, setCode] = useState<string>('');

    const onSubmit = () => {
        teamsService.join(code)
            .then(
                _ => alert("OK"),
                error => alert(error)
            )
    }

    return (
        <div className="teams-panel-block">
            <h3>Join a team</h3>
            <p>Enter code, which your friend gives you.</p>
            <input id="code-input" type="text" maxLength={5} placeholder="CODE"
                value={code} onChange={e => setCode(e.target.value)}/>
            <button style={code.length !== 5 ? { visibility: 'collapse'} : {}} onClick={onSubmit}>JOIN</button>
        </div>
    );
}
