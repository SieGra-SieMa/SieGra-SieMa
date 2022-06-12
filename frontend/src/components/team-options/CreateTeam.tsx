import { FormEvent, useState } from 'react';
import styles from './TeamOptions.module.css';
import Input from '../form/Input';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';

export default function CreateTeam() {

    const { teamsService } = useApi();

    const [name, setName] = useState<string>('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.createTeam(name)
            .then(
                _ => alert(`You have created team "${name}"`),
                error => alert(error)
            );
    }

    return (
        <form className={styles.option} onSubmit={onSubmit}>
            <h2>Create a team</h2>
            <p>If you and your friends want to participate in our tournaments, you can create your own team.</p>
            <Input
                id="CreateTeam-name"
                label="Team name"
                type="text"
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <Button
                value='CREATE'
                disabled={name.length === 0}
            />
        </form>
    );
}
