import { createPortal } from 'react-dom';
import styles from './Modal.module.css';

export default function Modal({
    title, close, isClose = false, children
}: {
    title: string, close: () => void, isClose?: boolean, children: JSX.Element
}) {
    return createPortal((
        <div className={styles.root} onClick={close}>
            <div className={styles.container} onClick={(e) => e.stopPropagation()}>
                <div className={styles.header}>
                    <h3>{title}</h3>
                    {isClose && <button className={styles.close} onClick={close}></button>}
                </div>
                <div>
                    {children}
                </div>
            </div>
        </div>
    ), document.getElementById('modal')!);
}
