import { FormEvent, useState } from 'react';
import { PasswordChange as PasswordChangeType } from '../../_lib/types';
import { useAlert } from '../alert/AlertContext';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './PasswordChange.module.css';

type PasswordChangeProps = {
    confirm: () => void;
}

export default function PasswordChange({ confirm }: PasswordChangeProps) {

    const alert = useAlert();
    const { usersService } = useApi();

    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedUser: PasswordChangeType = {
            oldPassword,
            newPassword
        };
        usersService.changePassword(updatedUser)
            .then((data) => {
                alert.success(data.message);
                confirm();
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='AccountPasswordChange-pass1'
                label='Stare hasło'
                type='password'
                value={oldPassword}
                required
                onChange={(e) => setOldPassword(e.target.value)}
            />
            <Input
                id='AccountPasswordChange-pass2'
                label='Nowe hasło'
                type='password'
                value={newPassword}
                required
                onChange={(e) => setNewPassword(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zmień' style={ButtonStyle.Red} />
        </form>
    );
}
