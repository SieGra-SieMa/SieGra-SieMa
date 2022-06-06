import { useRef } from 'react';
import { createPortal } from 'react-dom';
import Button, { ButtonStyle } from '../form/Button';
import styles from './Modal.module.css';

export default function Modal({
    title, close, isClose = false, children
}: {
    title: string, close: () => void, isClose?: boolean, children: JSX.Element
}) {

    const isMouseDown = useRef(false);

    return createPortal((
        <div
            className={styles.root}
            onMouseDown={() => {
                isMouseDown.current = true;
            }}
            onClick={() => {
                if (isMouseDown.current) {
                    close();
                }
                isMouseDown.current = false;
            }}
        >
            <div
                className={styles.container}
                onClick={(e) => e.stopPropagation()}
                onMouseDown={(e) => e.stopPropagation()}
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
