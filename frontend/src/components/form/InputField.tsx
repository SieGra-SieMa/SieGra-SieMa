import { ChangeEventHandler } from 'react';
import styles from './InputField.module.css';

interface InputFieldProp {
    id?: string;
    label?: string;
    type?: string
    maxLength?: number;
    placeholder?: string;
    value: string;
    onChange: ChangeEventHandler<HTMLInputElement>;
}

export default function InputField({
    id,
    label,
    type = 'text',
    maxLength,
    placeholder,
    value,
    onChange,
}: InputFieldProp) {
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