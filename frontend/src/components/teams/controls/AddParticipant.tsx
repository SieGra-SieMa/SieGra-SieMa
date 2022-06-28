import { useState, FormEvent } from 'react';
import { useApi } from '../../api/ApiContext';
import { Team } from '../../../_lib/types';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './AddParticipant.module.css';
type AddParticipantProps = {
    team: Team;
    confirm: (team: string) => void;
}

export default function AddParticipant({ team, confirm}: AddParticipantProps) {

    const [email, setEmail] = useState('');
    const { teamsService } = useApi();


    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.sendInvite(team.id, email)
            .then((data) => {
                confirm(data.message);
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='AddParticipant-email'
                label='Email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </form>
    );
}