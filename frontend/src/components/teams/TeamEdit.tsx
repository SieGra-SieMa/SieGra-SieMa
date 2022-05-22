import { FormEvent, useState } from 'react';
import { Team } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './TeamEdit.module.css';
import { useTeams } from './TeamsContext';

type TeamEditProps = {
    team: Team;
    confirm: () => void;
}

export default function TeamEdit({ team, confirm }: TeamEditProps) {

    const { teamsService } = useApi();
    const { teams, setTeams } = useTeams();

    const [name, setName] = useState(team.name);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.updateTeam(team.id, name)
            .then((data) => {
                confirm();
                setTeams([...teams!.filter((team) => team.id !== data.id), data]);
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='TeamEdit-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Save' />
        </form>
    );
}