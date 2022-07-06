import { FormEvent } from 'react';
import styles from './Form.module.css';


type Props = {
    className?: string;
    onSubmit: (e: FormEvent) => void;
    children: React.ReactNode;
}

export default function Form({ className, onSubmit, children }: Props) {
    return (
        <form className={[
            styles.root,
            className
        ].filter((e) => e).join(' ')} onSubmit={onSubmit}>
            {children}
        </form>
    );
}
