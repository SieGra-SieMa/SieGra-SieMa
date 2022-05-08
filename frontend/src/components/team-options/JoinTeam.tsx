import { useState } from 'react';
import styles from './TeamOptions.module.css';
import InputField from '../form/InputField';
import { useApi } from '../api/ApiContext';

export default function JoinTeam() {

    const { teamsService } = useApi();

    const [code, setCode] = useState<string>('');

    const onSubmit = () => {
        teamsService.joinTeam(code)
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
