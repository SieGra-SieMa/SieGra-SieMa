import Button, { ButtonStyle } from '../form/Button';
import styles from './TeamLeave.module.css';

export default function TeamLeave({ cancel, confirm }: { cancel: () => void, confirm: () => void }) {
    return (
        <div className={styles.root}>
            <Button
                value='Cancel'
                onClick={cancel}
            />
            <Button
                value='Leave'
                onClick={confirm}
                style={ButtonStyle.Secondary}
            />
        </div>
    );
}