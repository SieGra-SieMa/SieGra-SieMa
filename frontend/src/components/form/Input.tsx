import { ChangeEventHandler } from 'react';
import styles from './Form.module.css';

type InputProp = {
    id?: string;
    label?: string;
    type?: string
    minLength?: number;
    maxLength?: number;
    placeholder?: string;
    value: string;
    required?: boolean;
    disabled?: boolean;
    onChange: ChangeEventHandler<HTMLInputElement>;
}

export default function Input({
    id,
    label,
    type = 'text',
    minLength,
    maxLength,
    placeholder,
    value,
    required = false,
    disabled = false,
    onChange,
}: InputProp) {
    return (
        <div className={styles.root}>
            {label && (<label htmlFor={id} className={styles.label}>{label}</label>)}
            <input
                id={id}
                className={styles.input}
                type={type}
                minLength={minLength}
                maxLength={maxLength}
                placeholder={placeholder}
                value={value}
                required={required}
                disabled={disabled}
                onChange={onChange}
            />
        </div>
    );
}