import { useEffect, useState } from "react";
import Button, { ButtonStyle } from "../form/Button";
import styles from "./AccountEntry.module.css";
import CreateAccount from "./CreateAccount";
import SignIn from "./SignIn";

export default function AccountEntry() {

	const [isLogin, setIsLogin] = useState(true);

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	const changeView = () => {
		setIsLogin(state => !state);
	};

	return (
		<div className={[
			'container',
			styles.root
		].join(' ')}>
			<div className={isLogin ? styles.visible : styles.hidden}>
				<SignIn />
				<Button
					className={styles.button}
					onClick={changeView}
					value='Nie masz konta?'
					style={ButtonStyle.DarkBlue}
				/>
			</div>
			<span className={styles.divider}>OR</span>
			<div className={isLogin ? styles.hidden : styles.visible}>
				<CreateAccount />
				<Button
					className={styles.button}
					onClick={changeView}
					value='Masz już konto?'
					style={ButtonStyle.DarkBlue}
				/>
			</div>
		</div >
	);
}
