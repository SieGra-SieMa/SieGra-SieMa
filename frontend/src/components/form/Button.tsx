import { MouseEventHandler } from 'react';
import styles from './Form.module.css';

export enum ButtonStyle {
    Primary,
    Secondary,
    Tertiary,
}

type ButtonProp = {
    id?: string;
    className?: string
    value: string;
    type?: "submit" | "button" | "reset";
    disabled?: boolean;
    onClick?: MouseEventHandler<HTMLButtonElement>;
    style?: ButtonStyle;
}

const getStyleName = (style: ButtonStyle) => {
    switch (style) {
        case ButtonStyle.Primary:
            return styles.primaryButton;
        case ButtonStyle.Secondary:
            return styles.secondaryButton;
        case ButtonStyle.Tertiary:
            return styles.tertiaryButton;
        default:
            return styles.primaryButton;
    }
};

export default function Button({
    id,
    className,
    value,
    type = 'submit',
    disabled = false,
    onClick,
    style = ButtonStyle.Primary,
}: ButtonProp) {

    const styleName = getStyleName(style);

    return (
        <button
            id={id}
            type={type}
            className={[styleName, className].filter((c) => c).join(' ')}
            disabled={disabled}
            onClick={onClick}
        >
            {value}
        </button>
    );
}