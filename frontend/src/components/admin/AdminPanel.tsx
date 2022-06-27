import { useEffect, useState } from "react";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import Button, { ButtonStyle } from "../form/Button";
import styles from "./AdminPanel.module.css";

export default function AdminPanel() {
	const navigate = useNavigate();
	const location = useLocation();
	const [currentPage, setCurrentPage] = useState("");

	useEffect(() => {
		switch (location.pathname) {
			case "/admin/users":
				setCurrentPage("Użytkownicy");
				break;
			case "/admin":
				setCurrentPage("Newsletter");
				break;
			case "/admin/teams":
				setCurrentPage("Zespoły");
				break;
		}
	}, [location]);

	return (
		<div className={styles.container}>
			<h1>{currentPage}</h1>
			<div className={styles.navigation}>
				<Button
					value="Użytkownicy"
					onClick={() => {
						navigate("users");
					}}
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
			<div className={styles.outlet}>
				<Outlet />
			</div>
		</div>
	);
}
