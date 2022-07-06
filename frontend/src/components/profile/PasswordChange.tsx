import { FormEvent, useState } from 'react';
import { PasswordChange as PasswordChangeType } from '../../_lib/types';
import { useAlert } from '../alert/AlertContext';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import Form from '../form/Form';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';


type Props = {
    confirm: () => void;
}

export default function PasswordChange({ confirm }: Props) {

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
        <Form onSubmit={onSubmit}>
            <Input
                id='PasswordChange-oldPassword'
                label='Stare hasło'
                type='password'
                value={oldPassword}
                required
                onChange={(e) => setOldPassword(e.target.value)}
            />
            <Input
                id='PasswordChange-newPassword'
                label='Nowe hasło'
                type='password'
                value={newPassword}
                required
                onChange={(e) => setNewPassword(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zmień' style={ButtonStyle.Red} />
        </Form>
    );
}
