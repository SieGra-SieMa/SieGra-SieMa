import Button, { ButtonStyle } from '../form/Button';
import styles from './CancelLeave.module.css';

export default function CancelLeave({ cancel, confirm }: { cancel: () => void, confirm: () => void }) {
    return (
        <div className={styles.root}>
            <Button
                value='Cancel'
                onClick={cancel}
                style={ButtonStyle.Grey}
            />
            <Button
                value='Leave'
                onClick={confirm}
                style={ButtonStyle.Red}
            />
        </div>
    );
}