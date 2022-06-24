import { FormEvent, useState } from 'react';
import { Contest } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './Contests.module.css';

type EditContestProps = {
    contest: Contest;
    confirm: () => void;
}

export default function EditContest({
    contest,
    confirm,
}: EditContestProps) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState(contest.name);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        tournamentsService.updateContest(contest.tournamentId, contest.id, name)
            .then((data) => {
                confirm();
            });
    }

    return (
        <form className={styles.form} onSubmit={onSubmit}>
            <Input
                id='EditContest-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </form>
    );
}