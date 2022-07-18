import { FormEvent, useState } from 'react';
import { User, UserDetailsRequest } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';


type Props = {
    user: User;
    confirm: (user: User) => void;
}

export default function EditUser({ user, confirm }: Props) {

    const { usersService } = useApi();

    const [name, setName] = useState(user.name);
    const [surname, setSurname] = useState(user.surname);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedUser: UserDetailsRequest = {
            name,
            surname
        };
        return usersService.adminUpdateUser(user.id, updatedUser)
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
                id='EditUser-name'
                label='Imie'
                value={name}
                onChange={(e) => setName(e.target.value)}
            />
            <Input
                id='EditUser-surname'
                label='Nazwisko'
                value={surname}
                onChange={(e) => setSurname(e.target.value)}
            />
        </Form>
    );
}
