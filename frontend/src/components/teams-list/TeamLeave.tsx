import styles from './TeamLeave.module.css';

export default function TeamLeave({ cancel, confirm }: { cancel: () => void, confirm: () => void }) {
    return (
        <div className={styles.root}>
            <button className="button" onClick={cancel}>Cancel</button>
            <button className={styles.button} onClick={confirm}>Leave</button>
        </div>
    );
}