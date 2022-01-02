import { useState } from 'react';
import styles from './TeamOptions.module.css';
import { teamsService } from '../../_services/teams.service';

export default function CreateTeam() {

    const [name, setName] = useState<string>('');

    const onSubmit = () => {
        teamsService.create(name)
            .then(
                _ => alert("OK"),
                error => alert(error)
            )
    }

    return (
        <div className={styles.root}>
            <h3>Create a team</h3>
            <p>If you and your friends want to participate in our tournaments, you can create your own team.</p>
            <div>
                <label htmlFor={styles.teamNameInput}>Team name:</label>
                <input
                    id={styles.teamNameInput}
                    type="text"
                    value={name}
                    onChange={e => setName(e.target.value)}
                />
            </div>
            <button
                className={name.length === 0 ? styles.disabled : 'button'}
                disabled={name.length === 0}
                onClick={onSubmit}
            >
                CREATE
            </button>
        </div>
    );
}
