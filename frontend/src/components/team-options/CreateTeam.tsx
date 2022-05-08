import { useState } from 'react';
import styles from './TeamOptions.module.css';
import InputField from '../form/InputField';
import { useApi } from '../api/ApiContext';

export default function CreateTeam() {

    const { teamsService } = useApi();

    const [name, setName] = useState<string>('');

    const onSubmit = () => {
        teamsService.createTeam(name)
            .then(
                _ => alert(`You have created team "${name}"`),
                error => alert(error)
            )
    }

    return (
        <div className={styles.option}>
            <h2>Create a team</h2>
            <p>If you and your friends want to participate in our tournaments, you can create your own team.</p>
            <InputField
                id="teamNameInput"
                label="Team name"
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />
            <button
                className={name.length === 0 ? ['button', styles.disabled].join(' ') : 'button'}
                disabled={name.length === 0}
                onClick={onSubmit}
            >
                CREATE
            </button>
        </div>
    );
}
