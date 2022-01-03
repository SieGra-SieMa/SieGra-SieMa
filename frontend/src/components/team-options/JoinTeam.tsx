import { useState } from 'react';
import styles from './TeamOptions.module.css';
import { teamsService } from '../../_services/teams.service';

export default function JoinTeam() {

    const [code, setCode] = useState<string>('');

    const onSubmit = () => {
        teamsService.join(code)
            .then(
                _ => alert("OK"),
                error => alert(error)
            )
    }

    return (
        <div className={styles.root}>
            <h3>Join a team</h3>
            <p>Enter code, which your friend gives you.</p>
            <input
                id={styles.codeInput} 
                type="text" 
                maxLength={5} 
                placeholder="CODE"
                value={code} 
                onChange={e => setCode(e.target.value)}
            />
            <button
                className={code.length !== 5 ? styles.disabled : 'button'}
                disabled={code.length !== 5}
                onClick={onSubmit}
            >
                JOIN
            </button>
        </div>
    );
}