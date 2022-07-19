import { FormEvent, useState } from 'react';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';
import Form from '../form/Form';
import { useAlert } from '../alert/AlertContext';


type Props = {
    confirm: () => void;
}

export default function ForgetPassword({ confirm }: Props) {

    const alert = useAlert();
    const { accountsService } = useApi();

    const [email, setEmail] = useState('');

    const resetPassword = (e: FormEvent) => {
        e.preventDefault();
        return accountsService.forgetPassword(email)
            .then(() => {
                confirm();
                alert.success('Link do resetowania hasła został wysłany');
            });
    };

    return (
        <Form onSubmit={resetPassword} trigger={<>
            <VerticalSpacing size={15} />
            <Button value='Zresetuj' />
        </>}>
            <Input
                id='ForgetPassword-email'
                label='Email'
                type='email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
        </Form>
    );
}
