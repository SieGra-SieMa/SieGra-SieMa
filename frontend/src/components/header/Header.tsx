import styles from './Header.module.css';
import { NavLink } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import { useUser } from '../user/UserContext';
import { useCallback, useState } from 'react';
import Button from '../form/Button';
import GuardComponent from '../guard-components/GuardComponent';
import { ROLES } from '../../_lib/roles';
import MenuIcon from '@mui/icons-material/Menu';
import CloseIcon from '@mui/icons-material/Close';

export default function Header() {
	const { session, setSession } = useAuth();
	const { user } = useUser();
	const [toggleMenu, setToggleMenu] = useState(false);
	const [navState, setNavState] = useState(`${styles.navClosed}`);

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
			<div className={[
				'container',
				styles.container,
			].join(' ')}>
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
										<NavLink to="/admin" onClick={closeMenu}>
											Panel administratora
										</NavLink>
									</li>
								</GuardComponent>
								<li>
									<NavLink to="/myteams" onClick={closeMenu}>
										Moje zespoły
									</NavLink>
								</li>
								<li>
									<NavLink to="/account" onClick={closeMenu}>
										{user
											? `${user.name} ${user.surname}`
											: "USERNAME"}
									</NavLink>
								</li>
								<li>
									<Button
										value='Wyloguj'
										type='button'
										onClick={logout}
									/>
								</li>
							</>
						) : (
							<li>
								<NavLink className="navlink" to="/account" onClick={closeMenu}>
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
