import { FormEvent, useState } from 'react';
import { PasswordChange } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './AccountPasswordChange.module.css';

type AccountDataEditProps = {
    confirm: () => void;
}

export default function AccountPasswordChange({ confirm }: AccountDataEditProps) {

    const { usersService } = useApi();

    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedUser: PasswordChange = {
            oldPassword,
            newPassword
        };
        usersService.changePassword(updatedUser)
            .then(
                (data) => {
                    alert(data.message);
                    confirm();
                }, (error) => {
                    alert(error);
                }
            );
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='AccountPasswordChange-pass1'
                label='Stare hasło'
                type='password'
                value={oldPassword}
                onChange={(e) => setOldPassword(e.target.value)}
            />
            <Input
                id='AccountPasswordChange-pass2'
                label='Nowe hasło'
                type='password'
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </form>
    );
}
