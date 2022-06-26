import styles from './AdminTeamsList.module.css';
import { useState, useEffect } from 'react';
import { Team } from '../../../_lib/types';
import SyncLoader from 'react-spinners/SyncLoader';
import { useApi } from '../../api/ApiContext';
import TeamsListItem from './TeamsListItem';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';

export default function AdminTeamsList() {

    const { teamsService } = useApi();

    const [teams, setTeams] = useState<Team[] | null>(null);
    const [search, setSearch] = useState('');

    useEffect(() => {
        teamsService.getAllTeams()
            .then(
                (result) => setTeams(result),
                (error) => alert(error)
            );
    }, [teamsService]);

    const onTeamChange = (team: Team) => {
        if (!teams) return;
        const index = teams.findIndex((e) => e.id === team.id);
        if (index >= 0) {
            const updatedTeams = [...teams];
            updatedTeams[index] = team;
            setTeams(updatedTeams);
        }
    }

    const onTeamDelete = (team: Team) => {
        if (!teams) return;
        const index = teams.findIndex((e) => e.id === team.id);
        if (index >= 0) {
            setTeams(teams.filter((e) => e !== team));
        }
    }

    return (
        <>
            <h1>Zespoły</h1>
            <Input
                placeholder='Wyszukaj...'
                value={search}
                onChange={(e) => setSearch(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <div className={styles.content}>
                {teams ? teams.filter((team) => {
                    return (
                        team.name.toLowerCase().includes(search.toLowerCase()) ||
                        team.code.toLowerCase().includes(search.toLowerCase())
                    )
                }).map((team, index) => (
                    <TeamsListItem
                        key={index}
                        team={team}
                        onTeamChange={onTeamChange}
                        onTeamDelete={onTeamDelete}
                    />
                )) : (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={20} margin={20} color='#fff' />
                    </div>
                )}
            </div>
        </>
    );
}
