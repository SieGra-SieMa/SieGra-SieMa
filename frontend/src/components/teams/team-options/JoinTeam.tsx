import { FormEvent, useState } from 'react';
import styles from './TeamOptions.module.css';
import Input from '../../form/Input';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import { useTeams } from '../TeamsContext';

export default function JoinTeam() {

    const { teamsService } = useApi();

    const { teams, setTeams } = useTeams();

    const [code, setCode] = useState<string>('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.joinTeam(code)
            .then(
                (team) => {
                    if (teams) {
                        setTeams([...teams, team]);
                    } else {
                        setTeams([team]);
                    }
                    setCode('');
                    alert(`Dołączyłeś do zespołu - ${team.name}`);
                },
                error => alert(error)
            );
    }

    return (
        <form className={styles.option} onSubmit={onSubmit}>
            <h4>Dołącz do zespołu</h4>
            <p>Wpisz kod, który otrzymałeś od znajomego.</p>
            <Input
                id={styles.codeInput}
                minLength={5}
                maxLength={5}
                placeholder="KOD"
                value={code}
                required
                onChange={(e) => setCode(e.target.value)}
            />
            <Button
                value='Dołącz'
                disabled={code.length !== 5}
            />
        </form>
    );
}
