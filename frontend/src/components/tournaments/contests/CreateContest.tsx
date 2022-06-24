import { FormEvent, useState } from 'react';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './Contests.module.css';

type CreateContestProps = {
    tournamentId: string;
    confirm: (contest: any) => void;
};

export default function CreateContest({ tournamentId, confirm }: CreateContestProps) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        tournamentsService.createContest(tournamentId, name)
            .then((data) => {
                confirm(data);
            });
    }

    return (
        <form className={styles.form} onSubmit={onSubmit}>
            <Input
                id='CreateContest-name'
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
