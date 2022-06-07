import { FormEvent, useState } from 'react';
import { User, UserDetailsRequest } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useUser } from '../user/UserContext';
import styles from './AccountDataEdit.module.css';

type AccountDataEditProps = {
    confirm: (user: User) => void;
}

export default function AccountDataEdit({ confirm }: AccountDataEditProps) {

    const { usersService } = useApi();
    const { user } = useUser();

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
                confirm({ ...user!, ...updatedUser });
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='AccountData-name'
                label='Name'
                value={name}
                onChange={(e) => setName(e.target.value)}
            />
            <Input
                id='AccountData-surname'
                label='Surname'
                value={surname}
                onChange={(e) => setSurname(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Save' />
        </form>
    );
}
