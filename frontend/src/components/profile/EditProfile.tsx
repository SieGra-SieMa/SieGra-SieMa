import { FormEvent, useState } from 'react';
import { UserDetailsRequest } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Form from '../form/Form';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useUser } from '../user/UserContext';


type Props = {
    confirm: () => void;
};

export default function EditProfile({ confirm }: Props) {

    const { usersService } = useApi();
    const { user, setUser } = useUser();

    const [name, setName] = useState(user!.name);
    const [surname, setSurname] = useState(user!.surname);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedUser: UserDetailsRequest = {
            name,
            surname
        };
        usersService.updateUser(updatedUser)
            .then((data) => {
                setUser({ ...user!, ...updatedUser });
                confirm();
            });
    };

    return (
        <Form onSubmit={onSubmit}>
            <Input
                id='AccountData-name'
                label='Imie'
                value={name}
                onChange={(e) => setName(e.target.value)}
            />
            <Input
                id='AccountData-surname'
                label='Nazwisko'
                value={surname}
                onChange={(e) => setSurname(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </Form>
    );
}
