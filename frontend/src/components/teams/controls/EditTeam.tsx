import { FormEvent, useState } from 'react';
import { Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './EditTeam.module.css';

type EditTeamProps = {
    team: Team;
    confirm: (team: Team) => void;
    checkCapt: boolean;
}

export default function EditTeam({ team, confirm, checkCapt }: EditTeamProps) {

    const { teamsService } = useApi();

    const [name, setName] = useState(team.name);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if(checkCapt){
            teamsService.updateTeam(team.id, name)
            .then((data) => {
                confirm(data);
            });
        }
        else{
            teamsService.updateTeamAdmin(team.id, name)
            .then((data) => {
                confirm(data);
            });
        }   
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='TeamEdit-name'
                label='Nazwa'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </form>
    );
}