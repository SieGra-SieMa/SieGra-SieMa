import { FormEvent, useState } from 'react';
import { User, UserDetailsRequest, PasswordChange } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useUser } from '../user/UserContext';
import styles from './AccountPasswordChange.module.css';

type AccountDataEditProps = {
    confirm: () => void;
}

export default function AccountPasswordChange({ confirm }: AccountDataEditProps) {

    const { usersService } = useApi();
    const { user } = useUser();

    const [oldpassword, setOldPassword] = useState('');
    const [newpassword, setNewPassword] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedUser: PasswordChange = {
            oldpassword,
            newpassword
        };
        usersService.changePassword(updatedUser)
            .then((data) => {});
        // usersService.updateUser(updatedUser)
        //     .then((data) => {
        //         confirm({ ...user!, ...updatedUser });
        //     });
        //usersService.
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='AccountPasswordChange-pass1'
                label='Old password'
                value={oldpassword}
                onChange={(e) => setOldPassword(e.target.value)}
            />
            <Input
                id='AccountPasswordChange-pass2'
                label='New password'
                value={newpassword}
                onChange={(e) => setNewPassword(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Save' />
        </form>
    );
}
