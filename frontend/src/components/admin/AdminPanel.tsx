import { Outlet, useNavigate } from "react-router-dom";
import Button, { ButtonStyle } from "../form/Button";
import styles from "./AdminPanel.module.css";


export default function AdminPanel() {

	const navigate = useNavigate();

	return (
		<div className={[
			'container',
			styles.root,
		].join(' ')}>
			<div className={styles.navigation}>
				<Button
					value="Użytkownicy"
					onClick={() => navigate("users")}
					style={ButtonStyle.Transparent}
				/>
				<Button
					value="Zespoły"
					onClick={() => {
						navigate("teams");
					}}
					style={ButtonStyle.Transparent}
				/>
				<Button
					value="Newsletter"
					onClick={() => {
						navigate("");
					}}
					style={ButtonStyle.Transparent}
				/>
			</div>
			<Outlet />
		</div>
	);
}
