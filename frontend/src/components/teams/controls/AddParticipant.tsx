import { useState, FormEvent } from 'react';
import { useApi } from '../../api/ApiContext';
import { Team } from '../../../_lib/types';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import Form from '../../form/Form';
import { useAlert } from '../../alert/AlertContext';


type Props = {
    team: Team;
    confirm: () => void;
}

export default function AddParticipant({ team, confirm }: Props) {

    const alert = useAlert();
    const { teamsService } = useApi();

    const [email, setEmail] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        return teamsService.sendInvite(team.id, email)
            .then((data) => {
                alert.success(data.message);
                confirm();
            });
    };

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </>}>
            <Input
                id='AddParticipant-email'
                label='Email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
        </Form>
    );
}