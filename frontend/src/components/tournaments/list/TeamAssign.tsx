import { FormEvent, useEffect, useState } from 'react';
import { Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './TeamAssign.module.css';

type TeamAssignProps = {
    id: number;
    confirm: () => void;
};

export default function TeamAssign({ id, confirm }: TeamAssignProps) {

    const { teamsService, tournamentsService } = useApi();

    const [teams, setTeams] = useState<Team[] | null>(null);

    const [selectedTeam, setSelectedTeam] = useState<Team | null>(null);

    useEffect(() => {
        teamsService.getTeamsIAmCaptain()
            .then((data) => {
                setTeams(data);
            });
    }, [teamsService]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedTeam) return;
        tournamentsService.addTeam(id, selectedTeam.id)
            .then(() => {
                confirm();
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <ul className={styles.list}>
                {teams && teams.map((team, index) => (
                    <li
                        key={index}
                        className={[
                            styles.item,
                            (selectedTeam === team ? styles.selected : undefined)
                        ].filter((e) => e).join(' ')}
                        onClick={() => setSelectedTeam(team)}
                    >
                        <p>
                            {team.name}
                        </p>
                    </li>
                ))}
            </ul>
            <VerticalSpacing size={15} />
            <Button value='Zapisz' disabled={selectedTeam === null} />
        </form>
    );
};
