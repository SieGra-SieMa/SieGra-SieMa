import { FormEvent, useState } from 'react';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';

type ForgetPasswordProps = {
    confirm: () => void;
}

export default function ForgetPassword({ confirm }: ForgetPasswordProps) {

    const { accountsService } = useApi();

    const [email, setEmail] = useState('');

    const resetPassword = (e: FormEvent) => {
        e.preventDefault();
        accountsService.forgetPassword(email)
            .then(() => {
                confirm();
            });
    };

    return (
        <form onSubmit={resetPassword}>
            <Input
                id='Reset-email'
                label='E-mail'
                type='email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <VerticalSpacing size={30} />
            <Button value='Zresetuj' />
        </form>
    );
}
