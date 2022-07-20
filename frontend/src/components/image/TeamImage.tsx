import Config from "../../config.json";
import styles from "./TeamImage.module.css";
import ImageIcon from "@mui/icons-material/Image";
import EditIcon from "@mui/icons-material/Edit";

type Props = {
	className?: string;
	url?: string;
	size: number;
	placeholderSize: number;
	onClick?: () => void;
	isEditable?: boolean;
};

export default function TeamImage({
	className,
	url,
	size,
	placeholderSize,
	onClick,
	isEditable,
}: Props) {
	const sizes = {
		width: `${size}px`,
		height: `${size}px`,
	};

	return (
		<div
			className={[
				styles.picture,
				className,
				isEditable ? styles.editable : undefined,
			].filter((e) => e).join(" ")}
			style={
				url
					? {
						...sizes,
						backgroundImage: `url(${Config.HOST}${url})`,
					}
					: sizes
			}
			onClick={onClick}
		>
			{!url && (
				<div>
					<ImageIcon
						className={styles.placeholder}
						style={{
							fontSize: `${placeholderSize}px`,
						}}
					/>
					<EditIcon
						className={styles.editImage}
						style={{
							fontSize: `${placeholderSize}px`,
						}}
					/>
				</div>
			)}
		</div>
	);
}
