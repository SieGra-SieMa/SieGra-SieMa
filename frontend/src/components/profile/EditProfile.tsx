import { FormEvent, useState } from 'react';
import { UserDetailsRequest } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useUser } from '../user/UserContext';
import styles from './EditProfile.module.css';

type EditProfileProps = {
    confirm: () => void;
}

export default function EditProfile({ confirm }: EditProfileProps) {

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
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='AccountData-name'
                label='Imię'
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
        </form>
    );
}
