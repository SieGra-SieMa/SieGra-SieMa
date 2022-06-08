import { FormEvent, useState } from 'react';
import { Team } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './TeamDataEdit.module.css';
type TeamDataEditProps = {
    parameter: number,
    confirm: (team: Team) => void;
}

export default function TeamDataEdit({ confirm }: TeamDataEditProps, parametr: number) {

    const [name, setName] = useState('');
    const { teamsService } = useApi();

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.updateTeam(1, name)
            .then((data) => {
                confirm(data);
            });
    };

    return (
        <form onSubmit={onSubmit}>  
        <div className={styles.root}>
            <Input
                id='TeamChange-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Add' />
        </div>
        </form>
    );
}