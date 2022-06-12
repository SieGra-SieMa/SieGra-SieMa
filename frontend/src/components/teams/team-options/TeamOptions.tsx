import { useState } from 'react';
import Button, { ButtonStyle } from '../../form/Button';
import CreateTeam from './CreateTeam';
import JoinTeam from './JoinTeam';
import styles from './TeamOptions.module.css';

export default function TeamOptions() {

    const [isCreate, setIsCreate] = useState(true);

    const changeView = () => {
        setIsCreate(state => !state);
    };

    return (
        <div className={styles.root}>
            <div className={isCreate ? styles.visible : styles.hidden}>
                <CreateTeam />
                <Button
                    className={styles.button}
                    onClick={changeView}
                    value='Dołącz do zespołu'
                    style={ButtonStyle.DarkBlue}
                />
            </div>
            <span className={styles.divider}>OR</span>
            <div className={isCreate ? styles.hidden : styles.visible}>
                <JoinTeam />
                <Button
                    className={styles.button}
                    onClick={changeView}
                    value='Utwórz zespół'
                    style={ButtonStyle.DarkBlue}
                />
            </div>
        </div>
    );
}
