import { useState } from 'react';
import Button from '../form/Button';
import Input from '../form/Input';
import styles from './TeamAdd.module.css';

export default function TeamAdd() {

    const [email, setEmail] = useState('');

    return (
        <div className={styles.root}>
            <Input
                id='TeamAdd-email'
                label='Email'
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <Button value='Add' />
        </div>
    );
}