import styles from './TeamsList.module.css';
import TeamsListItem from './TeamsListItem';
import { useState } from 'react';
import { useEffect } from 'react';
import { Team } from '../../_lib/types';
import SyncLoader from 'react-spinners/SyncLoader';
import { useApi } from '../api/ApiContext';

export default function TeamsList() {

    const { teamsService } = useApi();

    const [teams, setTeams] = useState<Team[] | null>(null);

    useEffect(() => {
        teamsService.getTeams()
            .then(
                result => setTeams(result),
                error => alert(error)
            )
    }, [teamsService]);

    const onRemove = (id: number) => {
        const data = teams ? [...teams] : [];
        const index = data.findIndex(e => e.id === id) ?? -1;
        if (index >= 0) {
            data.splice(index, 1);
            setTeams(data);
        }
    };

    return (
        <div className="container">
            <h2 className={styles.title}>My teams</h2>
            <div className={styles.content}>
                {teams ? teams.map((team, index) => (
                    <TeamsListItem key={index} team={team} onRemove={onRemove} />
                )) : (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={20} margin={20} />
                    </div>
                )}
            </div>
        </div>
    );
}
