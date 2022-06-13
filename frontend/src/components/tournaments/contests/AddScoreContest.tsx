import { FormEvent, useState } from 'react';
import { Contest } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './Contests.module.css';

type AddScoreContestProps = {
    contest: Contest;
    confirm: () => void;
}

export default function AddScoreContest({
    contest,
    confirm,
}: AddScoreContestProps) {

    const { tournamentsService } = useApi();

    const [email, setEmail] = useState('');
    const [points, setPoints] = useState(0);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        tournamentsService.addContestScore(contest.tournamentId, contest.id, email, points)
            .then((data) => {
                confirm();
            });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='TournamentAdd-name'
                label='Email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <Input
                id='TournamentAdd-points'
                label='Punkty'
                value={points.toString()}
                required
                onChange={(e) => {
                    const result = parseInt(e.target.value);
                    if (isNaN(result)) {
                        setPoints(0);
                        return;
                    }
                    setPoints(Math.abs(result));
                }}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj wynik' />
        </form>
    );
}