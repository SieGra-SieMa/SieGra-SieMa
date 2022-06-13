import { FormEvent, useState } from 'react';
import styles from './TeamOptions.module.css';
import Input from '../../form/Input';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';

export default function CreateTeam() {

    const { teamsService } = useApi();

    const [name, setName] = useState<string>('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        teamsService.createTeam(name)
            .then(
                _ => alert(`Utworzyłeś zespół - "${name}"`),
                error => alert(error)
            );
    }

    return (
        <form className={styles.option} onSubmit={onSubmit}>
            <h4>Utwórz zespół</h4>
            <p>Jeśli Ty i Twoi znajomi chcecie brać udział w naszych turniejach, możecie stworzyć własną drużynę.</p>
            <Input
                id="CreateTeam-name"
                label="Nazwa zespołu"
                type="text"
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <Button
                value='Utworzyć'
                disabled={name.length === 0}
            />
        </form>
    );
}