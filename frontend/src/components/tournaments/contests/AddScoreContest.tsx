import { FormEvent, useState } from 'react';
import { Contest, Contestant } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';


type Props = {
    contest: Contest;
    confirm: (data: Contestant) => void;
}

export default function AddScoreContest({
    contest,
    confirm,
}: Props) {

    const { tournamentsService } = useApi();

    const [email, setEmail] = useState('');
    const [points, setPoints] = useState(0);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        return tournamentsService.addContestScore(contest.tournamentId, contest.id, email, points)
            .then((data) => {
                confirm(data);
            });
    }

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button value='Dodaj wynik' />
        </>}>
            <Input
                id='AddScoreContest-name'
                label='Email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <Input
                id='AddScoreContest-points'
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
        </Form>
    );
}