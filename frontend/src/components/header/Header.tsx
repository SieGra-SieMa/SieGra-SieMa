import styles from "./Header.module.css";
import { NavLink } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";
import { useUser } from "../user/UserContext";
import { useCallback, useState } from "react";
import Button from "../form/Button";
import MenuIcon from "@mui/icons-material/Menu";
import CloseIcon from "@mui/icons-material/Close";

export default function Header() {
	const { session, setSession } = useAuth();
	const { user } = useUser();
	const [toggleMenu, setToggleMenu] = useState(false);
	const [navState, setNavState] = useState(`${styles.navClosed}`);

	const logout = useCallback(() => setSession(null), [setSession]);

	const toggleNav = () => {
		setToggleMenu(!toggleMenu);
		setNavState(!toggleMenu ? `${styles.navOpen}` : `${styles.navClosed}`);
	};

	return (
		<header className={styles.root}>
			<div className={styles.container}>
				<div className={styles.logo}>
					<NavLink to="/">
						<img src="/logo_w.png" alt="" />
					</NavLink>
				</div>
				<nav className={styles.navigation} id={navState}>
					<ul>
						<li>
							<NavLink className="navlink" to="/">
								Strona główna
							</NavLink>
						</li>
						<li>
							<NavLink className="navlink" to="/tournaments">
								Turnieje
							</NavLink>
						</li>
						<li>
							<NavLink className="navlink" to="/tournaments/gallery">
								Galeria
							</NavLink>
						</li>
						<li>
							<NavLink className="navlink" to="/about-us">
								O nas
							</NavLink>
						</li>
						{session ? (
							<>
								<li>
									<NavLink className="navlink" to="/account">
										{user
											? `${user.name} ${user.surname}`
											: "USERNAME"}
									</NavLink>
								</li>
								<li>
									<Button
										value="Wyloguj"
										type="button"
										onClick={logout}
									/>
								</li>
							</>
						) : (
							<li>
								<NavLink className="navlink" to="/account">
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
		</header>
	);
}
