import { useState } from 'react';
import Button from '../../form/Button';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './AddParticipant.module.css';


export default function AddParticipant() {

    const [email, setEmail] = useState('');

    return (
        <div className={styles.root}>
            <Input
                id='AddParticipant-email'
                label='Email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </div>
    );
}