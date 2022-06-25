import { useEffect } from 'react';
import { createPortal } from 'react-dom';
import styles from './Modal.module.css';
import CloseIcon from '@mui/icons-material/Close';

export default function Modal({
    title, close, isClose = false, children
}: {
    title: string, close: () => void, isClose?: boolean, children: JSX.Element
}) {

    useEffect(() => {
        const handleEscKey = (e: KeyboardEvent) => e.key === 'Escape' && close();
        window.addEventListener('keydown', handleEscKey);
        return () => window.removeEventListener('keydown', handleEscKey);
    }, [close]);

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
                    <span></span>
                    <h5 className='underline'>{title}</h5>
                    {isClose && <CloseIcon className={styles.close} onClick={close} />}
                </div>
                <div className={styles.content}>
                    {children}
                </div>
            </div>
        </div>
    ), document.getElementById('modal')!);
}
