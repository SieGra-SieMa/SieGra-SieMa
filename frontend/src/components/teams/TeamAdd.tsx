import { useState } from 'react';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './TeamAdd.module.css';


// TODO
export default function TeamAdd() {

    const [email, setEmail] = useState('');

    return (
        <div className={styles.root}>
            <Input
                id='TeamAdd-email'
                label='Email'
                value={email}
                required
                onChange={(e) => setEmail(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Add' />
        </div>
    );
}