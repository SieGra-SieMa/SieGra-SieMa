import { useState } from 'react';
import InputField from '../form/InputField';
import styles from './TeamAdd.module.css';

export default function TeamAdd() {

    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');

    const [email, setEmail] = useState('');

    return (
        <div className={styles.root}>
            <div className={styles.container}>
                <InputField
                    id='TeamAdd-name'
                    label='Name'
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <InputField
                    id='TeamAdd-surname'
                    label='Surname'
                    value={surname}
                    onChange={(e) => setSurname(e.target.value)}
                />
                <button className="button">Add</button>
            </div>
            <div className={styles.container}>
                <InputField
                    id='TeamAdd-email'
                    label='Email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <button className="button">Add</button>
            </div>
        </div>
    );
}