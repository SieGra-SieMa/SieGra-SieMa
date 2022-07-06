import { FormEvent, useState } from 'react';
import { Contest } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import { useTournament } from '../TournamentContext';


type Props = {
    contest: Contest;
    confirm: () => void;
}

export default function EditContest({
    contest,
    confirm,
}: Props) {

    const { tournamentsService } = useApi();
    const { tournament, setTournament } = useTournament();

    const [name, setName] = useState(contest.name);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        tournamentsService.updateContest(contest.tournamentId, contest.id, name)
            .then((data) => {
                const id = tournament!.contests.findIndex((e) => e.id === contest.id);
                const updatedContest = [...tournament!.contests];
                updatedContest[id] = data;
                setTournament({
                    ...tournament!,
                    contests: updatedContest
                });
                confirm();
            });
    }

    return (
        <Form onSubmit={onSubmit}>
            <Input
                id='EditContest-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </Form>
    );
}