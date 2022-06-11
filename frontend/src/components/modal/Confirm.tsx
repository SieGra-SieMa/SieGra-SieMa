import Button, { ButtonStyle } from '../form/Button';
import styles from './Confirm.module.css';

type ConfirmProps = {
    cancel: () => void;
    confirm: () => void;
    label: string;
    style: ButtonStyle;
};

export default function Confirm({
    cancel,
    confirm,
    label,
    style,
}: ConfirmProps) {
    return (
        <div className={styles.root}>
            <Button
                value='Anuluj'
                onClick={cancel}
                style={ButtonStyle.Grey}
            />
            <Button
                value={label}
                onClick={confirm}
                style={style}
            />
        </div>
    );
};