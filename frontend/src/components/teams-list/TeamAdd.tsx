import { useState } from 'react';
import InputField from '../form/InputField';
import styles from './TeamAdd.module.css';

export default function TeamAdd() {

    const [email, setEmail] = useState('');

    return (
        <div className={styles.root}>
            <InputField
                id='TeamAdd-email'
                label='Email'
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <button className="button">Add</button>
        </div>
    );
}