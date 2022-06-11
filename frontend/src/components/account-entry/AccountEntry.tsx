import { useEffect, useState } from "react";
import Button, { ButtonStyle } from "../form/Button";
import styles from "./AccountEntry.module.css";
import CreateAccount from "./CreateAccount";
import SignIn from "./SignIn";

export default function AccountEntry() {
	const [login, setLogin] = useState(true);

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	function toggleLogin() {
		setLogin(!login);
	}

	return (
		<div className={`container ${styles.root}`}>
			<div className={ login ? styles.form : styles.hidden }>
				<SignIn />
				<Button
					className={styles.button}
					onClick={toggleLogin}
					value={"Nie masz konta?"}
					style={ButtonStyle.DarkBlue}
				/>
			</div>
			<span id={styles.divider}>OR</span>
			<div className={ login ? styles.hidden : styles.form }>
				<CreateAccount />
				<Button
					className={styles.button}
					onClick={toggleLogin}
					value={"Nie masz konta?"}
					style={ButtonStyle.DarkBlue}
				/>
			</div>
		</div>
	);
}
