import { FormEvent, useState } from "react";
import styles from "./AccountEntry.module.css";
import SyncLoader from "react-spinners/SyncLoader";
import { useAuth } from "../auth/AuthContext";
import Input from "../form/Input";
import { useNavigate } from "react-router-dom";
import { useApi } from "../api/ApiContext";
import Button, { ButtonStyle } from "../form/Button";
import VerticalSpacing from "../spacing/VerticalSpacing";
import Modal from "../modal/Modal";
import ForgetPassword from "./ForgetPassword";

export default function SignIn() {
	const { accountsService } = useApi();

	const navigate = useNavigate();

	const { setSession } = useAuth();

	const [email, setEmail] = useState("kapitan@gmail.com");
	const [password, setPassword] = useState("Haslo+123");
	const [loading, setLoading] = useState(false);

	const [isForget, setIsForget] = useState(false);

	const signIn = (e: FormEvent) => {
		e.preventDefault();
		setLoading(true);
		accountsService.authenticate(email, password).then(
			(session) => {
				setSession(session);
				navigate("/account");
			},
			() => setLoading(false)
		);
	};

	return (
		<div className={styles.signInRoot}>
			<form className={styles.block} onSubmit={signIn}>
				<h3>Zaloguj się</h3>
				<Input
					id="SignIn-email"
					label="Email"
					type="email"
					value={email}
					required
					onChange={(e) => setEmail(e.target.value)}
				/>
				<Input
					id="SignIn-password"
					label="Hasło"
					type="password"
					value={password}
					required
					onChange={(e) => setPassword(e.target.value)}
				/>
				<VerticalSpacing size={30} />
				{loading ? (
					<div className={styles.loader}>
						<SyncLoader
							loading={true}
							size={7}
							margin={20}
							color="#fff"
						/>
					</div>
				) : (
					<Button value="Zaloguj się" />
				)}
			</form>
            <Button
                    id={styles.forgotButton}
					onClick={() => setIsForget(true)}
					value="Zapomniałeś hasła?"
					style={ButtonStyle.Transparent}
				/>
			{isForget && (
				<Modal
					isClose
					title="Zresetuj hasło"
					close={() => setIsForget(false)}
				>
					<ForgetPassword confirm={() => setIsForget(false)} />
				</Modal>
			)}
		</div>
	);
}
