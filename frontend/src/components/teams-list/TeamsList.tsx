import './TeamsList.css';
import TeamsListItem from './TeamsListItem';
import { teamsService } from '../../_services/teams.service';
import { useState } from 'react';
import { useEffect } from 'react';
import { Team } from '../../_lib/types';
import SyncLoader from 'react-spinners/SyncLoader';
import { authenticationService } from '../../_services/authentication.service';
// import { useHistory } from 'react-router';

export default function TeamsList({ reload } : { reload: boolean }) {

    // const history = useHistory();

    const user = authenticationService.currentUserValue!;

    const [teams, setTeams] = useState<Team[] | null>(null);

    const isFirstFetch = teams !== null;

    useEffect(() => {
        if (isFirstFetch) {
            if (!reload) return;
        }
        teamsService.getTeams()
            .then(
                result => setTeams(result),
                error => alert(error)
            )
    }, [reload, isFirstFetch])

    const onRemove = (id: number) => {
        const data = teams ? [...teams] : [];
        const index = data.findIndex(e => e.id === id) ?? -1;
        if (index >= 0) {
            data.splice(index, 1);
            setTeams(data);
        }
    };

    if (!user) {
        // history.push('/');
        return null;
    }

    return (
        <div>
            <h1>My teams</h1>
            <div className="teams-list-container">
                {
                    teams ? teams.map((team, index) => (
                        <TeamsListItem key={index} team={team} onRemove={onRemove}/>
                    )) : 
                    <div className="loader">
                        <SyncLoader loading={true} size={20} margin={20}/>
                    </div>
                }
            </div>
        </div>
    );
}
