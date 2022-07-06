import { useCallback, useEffect, useState } from 'react';
import { createPortal } from 'react-dom';
import styles from './Modal.module.css';
import CloseIcon from '@mui/icons-material/Close';

export default function Modal({
    title, close, isClose = false, children
}: {
    title: string, close: () => void, isClose?: boolean, children: JSX.Element
}) {

    const [isClosing, setIsClosing] = useState(false);

    const closeModal = useCallback(() => {
        setIsClosing(true);
        setTimeout(close, 300);
    }, [close]);

    useEffect(() => {
        const handleEscKey = (e: KeyboardEvent) => e.key === 'Escape' && closeModal();
        window.addEventListener('keydown', handleEscKey);
        return () => window.removeEventListener('keydown', handleEscKey);
    }, [closeModal]);

    return createPortal((
        <div
            className={[
                styles.root,
                isClosing ? styles.closed : undefined
            ].filter((e) => e).join(' ')}
            onDoubleClick={closeModal}
        >
            <div
                className={[
                    styles.container,
                    isClosing ? styles.closed : undefined
                ].filter((e) => e).join(' ')}
                onDoubleClick={(e) => e.stopPropagation()}
            >
                <div className={styles.header}>
                    <span></span>
                    <h5 className='underline'>{title}</h5>
                    {isClose && <CloseIcon className={styles.close} onClick={closeModal} />}
                </div>
                <div className={styles.content}>
                    {children}
                </div>
            </div>
        </div>
    ), document.getElementById('modal')!);
}
