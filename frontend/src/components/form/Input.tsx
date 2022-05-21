import { ChangeEventHandler } from 'react';
import styles from './Form.module.css';

type InputProp = {
    id?: string;
    label?: string;
    type?: string
    maxLength?: number;
    placeholder?: string;
    value: string;
    onChange: ChangeEventHandler<HTMLInputElement>;
}

export default function Input({
    id,
    label,
    type = 'text',
    maxLength,
    placeholder,
    value,
    onChange,
}: InputProp) {
    return (
        <div className={styles.root}>
            {label && (<label htmlFor={id} className={styles.label}>{label}</label>)}
            <input
                id={id}
                className={styles.input}
                type={type}
                maxLength={maxLength}
                placeholder={placeholder}
                value={value}
                onChange={onChange}
            />
        </div>
    );
}