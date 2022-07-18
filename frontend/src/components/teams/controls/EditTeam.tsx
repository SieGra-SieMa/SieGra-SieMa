import { FormEvent, useState } from 'react';
import { Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';


type Props = {
    team: Team;
    confirm: (team: Team) => void;
    isAdmin?: boolean;
}

export default function EditTeam({ team, confirm, isAdmin = false }: Props) {

    const { teamsService } = useApi();

    const [name, setName] = useState(team.name);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();

        if (isAdmin) {
            return teamsService.updateTeamAdmin(team.id, name)
                .then((data) => {
                    confirm(data);
                });
        }

        return teamsService.updateTeam(team.id, name)
            .then((data) => {
                confirm(data);
            });
    };

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </>}>
            <Input
                id='TeamEdit-name'
                label='Nazwa'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
        </Form>
    );
}