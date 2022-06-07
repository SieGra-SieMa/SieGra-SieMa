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

    const { teamsService } = useApi();

    const [teams, setTeams] = useState<Team[] | null>(null);

    useEffect(() => {
        teamsService.getTeamsIAmCaptain()
            .then((data) => {
                setTeams(data);
            });
    }, [teamsService]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();


        confirm();
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <ul>
                {teams && teams.map((team, index) => (
                    <li key={index}>
                        <label>
                            {team.name}
                            <input type="radio" name="TeamAssign-team" required />
                        </label>
                    </li>
                ))}
            </ul>
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </form>
    );
};
