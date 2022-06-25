import { MouseEventHandler } from "react";
import styles from "./Form.module.css";

export enum ButtonStyle {
	Yellow,
	Grey,
	DarkBlue,
	Red,
	Transparent,
	TransparentBorder
}

type ButtonProp = {
	id?: string;
	className?: string;
	value: string;
	type?: "submit" | "button" | "reset";
	disabled?: boolean;
	onClick?: MouseEventHandler<HTMLButtonElement>;
	style?: ButtonStyle;
};

const getStyleName = (style: ButtonStyle) => {
	switch (style) {
		case ButtonStyle.Yellow:
			return styles.yellowButton;
		case ButtonStyle.Grey:
			return styles.greyButton;
		case ButtonStyle.DarkBlue:
			return styles.greenButton;
		case ButtonStyle.Red:
			return styles.redButton;
		case ButtonStyle.Transparent:
			return styles.transparentButton;
		case ButtonStyle.TransparentBorder:
			return styles.transparentBorderButton;
		default:
			return styles.yellowButton;
	}
};

export default function Button({
	id,
	className,
	value,
	type = "submit",
	disabled = false,
	onClick,
	style = ButtonStyle.Yellow,
}: ButtonProp) {
	const styleName = getStyleName(style);

	return (
		<button
			id={id}
			type={type}
			className={[
				styleName,
				className,
				disabled ? styles.disabled : undefined,
			]
				.filter((c) => c)
				.join(" ")}
			disabled={disabled}
			onClick={onClick}
		>
			{value}
		</button>
	);
}
