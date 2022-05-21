import { FormEvent, useState } from 'react';
import styles from './TeamOptions.module.css';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';

export default function JoinTeam() {

    const { teamsService } = useApi();

    const [code, setCode] = useState<string>('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.joinTeam(code)
            .then(
                _ => alert(`You have joined the team`),
                error => alert(error)
            );
    }

    return (
        <form className={styles.option} onSubmit={onSubmit}>
            <h2>Join a team</h2>
            <p>Enter code, which your friend gives you.</p>
            <Input
                id={styles.codeInput}
                type="text"
                maxLength={5}
                placeholder="CODE"
                value={code}
                onChange={(e) => setCode(e.target.value)}
            />
            <Button
                className={code.length !== 5 ? styles.disabled : undefined}
                value='JOIN'
                disabled={code.length !== 5}
            />
        </form>
    );
}
