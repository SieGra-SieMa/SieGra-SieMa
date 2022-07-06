import { Alert as AlertType, AlertTypeEnum } from '../../_lib/types';
import styles from './Alert.module.css';
import { useEffect, useState } from 'react';
import DeleteIcon from "@mui/icons-material/Delete";


type AlertProps = {
    alert: AlertType;
    timeout: number;
    close: () => void;
}

export default function Alert({ alert, timeout, close }: AlertProps) {

    const [seconds, setSeconds] = useState(timeout / 1000 - 1);

    useEffect(() => {
        const idTimeout = setTimeout(close, timeout);
        const idInterval = setInterval(() => setSeconds(seconds => seconds - 1), 1000);
        return () => {
            clearTimeout(idTimeout);
            clearInterval(idInterval);
        };
        // eslint-disable-next-line
    }, []);

    return (
        <div
            style={{
                animationDuration: `${timeout}ms`,
            }}
            className={[
                styles.alert,
                alert.type === AlertTypeEnum.success ? styles.success : undefined,
                alert.type === AlertTypeEnum.error ? styles.error : undefined,
            ].join(' ')}
        >
            {alert.message}
            <DeleteIcon
                className={styles.close}
                onClick={close}
                fontSize='medium'
            />
            {`(${seconds}s)`}
        </div>
    );
}
