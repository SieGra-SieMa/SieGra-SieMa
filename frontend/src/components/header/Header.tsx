import styles from "./Header.module.css";
import { NavLink } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";
import { useUser } from "../user/UserContext";
import { useCallback, useState } from "react";
import GuardComponent from "../guard-components/GuardComponent";
import { ROLES } from "../../_lib/roles";
import MenuIcon from "@mui/icons-material/Menu";
import CloseIcon from "@mui/icons-material/Close";
import LogoutIcon from "@mui/icons-material/Logout";
import { useApi } from "../api/ApiContext";
import EditProfile from "../profile/EditProfile";
import Modal from "../modal/Modal";
import AccountPasswordChange from "../profile/PasswordChange";
import Confirm from "../modal/Confirm";
import Button, { ButtonStyle } from "../form/Button";

export default function Header() {
	const { session, setSession } = useAuth();
	const { user, setUser } = useUser();
	const [toggleMenu, setToggleMenu] = useState(false);
	const [navState, setNavState] = useState(`${styles.navClosed}`);

	const { usersService } = useApi();

	const [isEdit, setIsEdit] = useState(false);
	const [isNewsletterJoin, setIsNewsletterJoin] = useState(false);
	const [isNewsletterLeave, setIsNewsletterLeave] = useState(false);
	const [isPasswordChange, setIsPasswordChange] = useState(false);

	const joinNewsletter = () => {
		usersService.joinNewsletter().then((data) => {
			setUser(data);
			setIsNewsletterJoin(false);
		});
	};

	const leaveNewsletter = () => {
		usersService.leaveNewsletter().then((data) => {
			setUser(data);
			setIsNewsletterLeave(false);
		});
	};

	const closeMenu = () => {
		setNavState(`${styles.navClosed}`);
		setToggleMenu(false);
	};

	const toggleNav = () => {
		setToggleMenu(!toggleMenu);
		setNavState(!toggleMenu ? `${styles.navOpen}` : `${styles.navClosed}`);
	};

	const logout = useCallback(() => {
		setSession(null);
		closeMenu();
	}, [setSession]);

	return (
		<header className={styles.root}>
			<div className={["container", styles.container].join(" ")}>
				<div className={styles.logo}>
					<NavLink to="/">
						<img src="/logo_w.png" alt="" />
					</NavLink>
				</div>
				<nav className={styles.navigation} id={navState}>
					<ul>
						<li>
							<NavLink to="/" onClick={closeMenu}>
								Strona główna
							</NavLink>
						</li>
						<li>
							<NavLink to="/tournaments" onClick={closeMenu}>
								Turnieje
							</NavLink>
						</li>
						<li>
							<NavLink to="/gallery" onClick={closeMenu}>
								Galeria
							</NavLink>
						</li>
						<li>
							<NavLink to="/about-us" onClick={closeMenu}>
								O nas
							</NavLink>
						</li>
						{session ? (
							<>
								<GuardComponent roles={[ROLES.Admin]}>
									<li>
										<NavLink
											to="/admin"
											onClick={closeMenu}
										>
											Panel administratora
										</NavLink>
									</li>
								</GuardComponent>
								<li>
									<NavLink to="/myteams" onClick={closeMenu}>
										Moje zespoły
									</NavLink>
								</li>
								<li id={styles.profile}>
									<h6 style={{ cursor: "pointer" }}>
										{user
											? `${user.name} ${user.surname}`
											: "Użytkownik"}
									</h6>
									<div className={styles.dropdown}>
										<div className={styles.dropdownTabs}>
											<Button
												value="Edytuj użytkownika"
												onClick={() => setIsEdit(true)}
												style={ButtonStyle.Transparent}
											/>
											{user &&
												(user.newsletter ? (
													<Button
														value="Zrezygnuj z newslettera"
														onClick={() =>
															setIsNewsletterLeave(
																true
															)
														}
														style={
															ButtonStyle.Transparent
														}
													/>
												) : (
													<Button
														value="Dolącz do newslettera"
														onClick={() =>
															setIsNewsletterJoin(
																true
															)
														}
														style={
															ButtonStyle.Transparent
														}
													/>
												))}

											<Button
												value="Zmień hasło"
												onClick={() =>
													setIsPasswordChange(true)
												}
												style={ButtonStyle.Transparent}
											/>
											<div
												id={styles.logout}
												onClick={logout}
											>
												<Button
													value="Wyloguj"
													onClick={() =>
														setIsPasswordChange(
															true
														)
													}
													style={
														ButtonStyle.Transparent
													}
												/>
												<LogoutIcon fontSize="small" />
											</div>
										</div>
									</div>
								</li>
							</>
						) : (
							<li>
								<NavLink
									className="navlink"
									to="/account"
									onClick={closeMenu}
								>
									Profil
								</NavLink>
							</li>
						)}
					</ul>
				</nav>
			</div>
			<button className={styles.menu} onClick={toggleNav}>
				{!toggleMenu ? (
					<MenuIcon fontSize="large" />
				) : (
					<CloseIcon fontSize="large" />
				)}
			</button>
			{user && isEdit && (
				<Modal
					isClose
					close={() => setIsEdit(false)}
					title={"Edytuj użytkownika"}
				>
					<EditProfile
						confirm={() => {
							setIsEdit(false);
						}}
					/>
				</Modal>
			)}
			{isPasswordChange && (
				<Modal
					isClose
					close={() => setIsPasswordChange(false)}
					title={`Zmiana hasła`}
				>
					<AccountPasswordChange
						confirm={() => {
							setIsPasswordChange(false);
							setSession(null);
						}}
					/>
				</Modal>
			)}
			{isNewsletterJoin && (
				<Modal
					close={() => setIsNewsletterJoin(false)}
					title={`Czy na pewno chcesz dołączyć do newslettera?`}
				>
					<Confirm
						cancel={() => setIsNewsletterJoin(false)}
						confirm={() => joinNewsletter()}
						label="Potwierdź"
						style={ButtonStyle.Yellow}
					/>
				</Modal>
			)}
			{isNewsletterLeave && (
				<Modal
					close={() => setIsNewsletterLeave(false)}
					title={`Czy na pewno chcesz zrezygnować z newslettera?`}
				>
					<Confirm
						cancel={() => setIsNewsletterLeave(false)}
						confirm={() => leaveNewsletter()}
						label="Potwierdź"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
		</header>
	);
}
