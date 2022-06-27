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

    const signIn = (e: FormEvent) => {
        e.preventDefault();
        accountsService.forgetPassword(email)
            .then(() => {
                confirm();
            });
    };

    return (
        <form onSubmit={signIn}>
            <Input
                id='SignIn-email'
                label='Email'
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
