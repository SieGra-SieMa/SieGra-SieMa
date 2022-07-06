import { FormEvent, useState } from 'react';
import { Contest } from '../../../_lib/_types/tournament';
import { useAlert } from '../../alert/AlertContext';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';


type Props = {
    tournamentId: string;
    confirm: (contest: Contest) => void;
};

export default function CreateContest({ tournamentId, confirm }: Props) {

    const alert = useAlert();
    const { tournamentsService } = useApi();

    const [name, setName] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        tournamentsService.createContest(tournamentId, name)
            .then((data) => {
                confirm(data);
                alert.success('Konkurs został dodany');
            });
    }

    return (
        <Form onSubmit={onSubmit}>
            <Input
                id='CreateContest-name'
                label='Nazwa'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </Form>
    );
}
