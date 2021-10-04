import React from 'react';
import './TeamsList.css';
import TeamsListItem from '../../components/teams-list/TeamsListItem';
import { teamsService } from '../../_services/teams.service';
import { useState } from 'react';
import { useEffect } from 'react';
import { Team } from '../../_lib/types';
import SyncLoader from 'react-spinners/SyncLoader';

export default function TeamsList() {

    const [teams, setTeams] = useState<Team[] | null>(null);

    useEffect(() => {
        teamsService.get()
            .then(
                result => setTeams(result),
                error => alert(error)
            )
    }, [])

    return (
        <div className="content-section">
            <h1>My Teams</h1>
            <div className="container teams-list-container">
                {
                    teams ? teams.map((team, index) => (
                        <TeamsListItem key={index} team={team}/>
                    )) : 
                    <div className="loader">
                        <SyncLoader loading={true} size={20} margin={20}/>
                    </div>
                }
            </div>
        </div>
    );
}
