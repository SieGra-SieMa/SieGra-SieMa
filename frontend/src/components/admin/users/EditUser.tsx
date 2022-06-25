import { FormEvent, useState } from 'react';
import { User, UserDetailsRequest } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './EditUser.module.css';

type EditUserProps = {
    user: User;
    confirm: (user: User) => void;
}

export default function EditUser({ user, confirm }: EditUserProps) {

    const { usersService } = useApi();

    const [name, setName] = useState(user.name);
    const [surname, setSurname] = useState(user.surname);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedUser: UserDetailsRequest = {
            name,
            surname
        };
        usersService.adminUpdateUser(user.id, updatedUser)
            .then((data) => {
                confirm(data);
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
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
        </form>
    );
}
