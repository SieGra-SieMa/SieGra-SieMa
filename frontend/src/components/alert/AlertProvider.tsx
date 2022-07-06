import { useCallback, useMemo, useRef, useState } from 'react';
import { AlertContext } from './AlertContext';
import { createPortal } from 'react-dom';
import styles from './Alert.module.css';
import { Alert as AlertType, AlertTypeEnum } from '../../_lib/types';
import Alert from './Alert';


type Props = {
    children: React.ReactNode;
    options: {
        errorTimeout: number;
        successTimeout: number;
    }
}

export default function AlertProvider({ children, options }: Props) {

    const [alerts, setAlerts] = useState<AlertType[]>([]);
    const uuid = useRef(0);

    const success = useCallback((message: string) => {
        setAlerts((alerts) => ([...alerts, {
            id: uuid.current++,
            message: message,
            type: AlertTypeEnum.success,
        }]));
    }, [setAlerts]);

    const error = useCallback((message: string) => {
        setAlerts((alerts) => ([...alerts, {
            id: uuid.current++,
            message: message,
            type: AlertTypeEnum.error,
        }]));
    }, [setAlerts]);

    const value = useMemo(() => ({
        success,
        error,
    }), [success, error]);

    return (
        <AlertContext.Provider value={value}>
            {children}
            {(alerts.length > 0) && createPortal(
                <div className={styles.root}>
                    {alerts.map((alert) => (
                        <Alert
                            key={alert.id}
                            alert={alert}
                            timeout={
                                alert.type === AlertTypeEnum.error ?
                                    options.errorTimeout :
                                    options.successTimeout
                            }
                            close={() => setAlerts(
                                (alerts) => alerts.filter(e => e !== alert)
                            )}
                        />
                    ))}
                </div>,
                document.getElementById('alerts')!
            )}
        </AlertContext.Provider>
    );
}
