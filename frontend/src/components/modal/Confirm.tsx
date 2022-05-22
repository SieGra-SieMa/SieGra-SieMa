import Button, { ButtonStyle } from '../form/Button';
import styles from './Confirm.module.css';

type ConfirmProps = {
    cancel: () => void,
    confirm: () => void,
    label: string
};

export default function Confirm({ cancel, confirm, label }: ConfirmProps) {
    return (
        <div className={styles.root}>
            <Button
                value='Cancel'
                onClick={cancel}
                style={ButtonStyle.Grey}
            />
            <Button
                value={label}
                onClick={confirm}
                style={ButtonStyle.Red}
            />
        </div>
    );
}