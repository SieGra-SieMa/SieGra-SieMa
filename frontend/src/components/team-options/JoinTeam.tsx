import { useState } from 'react';
import styles from './TeamOptions.module.css';
import { teamsService } from '../../_services/teams.service';
import InputField from '../form/InputField';

export default function JoinTeam() {

    const [code, setCode] = useState<string>('');

    const onSubmit = () => {
        teamsService.join(code)
            .then(
                _ => alert(`You have joined the team`),
                error => alert(error)
            )
    }

    return (
        <div className={styles.option}>
            <h2>Join a team</h2>
            <p>Enter code, which your friend gives you.</p>
            <InputField
                id={styles.codeInput}
                type="text"
                maxLength={5}
                placeholder="CODE"
                value={code}
                onChange={(e) => setCode(e.target.value)}
            />
            <button
                className={code.length !== 5 ? ['button', styles.disabled].join(' ') : 'button'}
                disabled={code.length !== 5}
                onClick={onSubmit}
            >
                JOIN
            </button>
        </div>
    );
}
