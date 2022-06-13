import { FormEvent, useState } from 'react';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './CreateContest.module.css';

type CreateContestType = {
    tournamentId: string;
    confirm: (contest: any) => void;
};

export default function CreateContest({ tournamentId, confirm }: CreateContestType) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        tournamentsService.addContest(tournamentId, name)
            .then((data) => {
                confirm(data);
            });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='TournamentAdd-name'
                label='Nazwa'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </form>
    );
}
