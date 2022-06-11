import { useEffect } from 'react';
import { createPortal } from 'react-dom';
import Button, { ButtonStyle } from '../form/Button';
import styles from './Modal.module.css';

export default function Modal({
    title, close, isClose = false, children
}: {
    title: string, close: () => void, isClose?: boolean, children: JSX.Element
}) {

    useEffect(() => {
        const handleEscKey = (e: KeyboardEvent) => e.key === 'Escape' && close();
        window.addEventListener('keydown', handleEscKey);
        return () => window.removeEventListener('keydown', handleEscKey);
    }, []);

    return createPortal((
        <div
            className={styles.root}
            onDoubleClick={close}
        >
            <div
                className={styles.container}
                onDoubleClick={(e) => e.stopPropagation()}
            >
                <div className={styles.header}>
                    <h3>{title}</h3>
                    {isClose && <Button className={styles.close} value='' onClick={close} style={ButtonStyle.Red} />}
                </div>
                <div>
                    {children}
                </div>
            </div>
        </div>
    ), document.getElementById('modal')!);
}
