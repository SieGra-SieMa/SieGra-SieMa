import styles from "./UsersListItem.module.css";
import { User } from "../../../_lib/types";
import { useState } from "react";
import Modal from "../../modal/Modal";
import Confirm from "../../modal/Confirm";
import { useApi } from "../../api/ApiContext";
import { ButtonStyle } from "../../form/Button";
import RoleAssign from "./RoleAssign";
import EditUser from "./EditUser";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import BlockIcon from "@mui/icons-material/Block";
import LockOpenIcon from '@mui/icons-material/LockOpen';

type UsersListItemProp = {
	user: User;
	onUserPropChange: (user: User) => void;
};

export default function UserListItem({
	user,
	onUserPropChange,
}: UsersListItemProp) {
	const { usersService } = useApi();

	const [isAdd, setIsAdd] = useState(false);
	const [isEdit, setIsEdit] = useState(false);
	const [isBan, setIsBan] = useState(false);
	const [isUnban, setIsUnban] = useState(false);
	const [roleToDelete, setRoleToDelete] = useState<string | null>(null);

	const removeRole = (role: string) => {
		return usersService.removeUserRole(user.id, [role]).then((data) => {
			onUserPropChange(data);
			setRoleToDelete(null);
		});
	};

	const banUser = () => {
		return usersService.adminBanUser(user.id).then((data) => {
			onUserPropChange(data);
			setIsBan(false);
		});
	};

	const unbanUser = () => {
		return usersService.adminUnbanUser(user.id)
			.then((data) => {
				onUserPropChange(data);
				setIsUnban(false);
			});
	};

	return (
		<div className={styles.root}>
			<h6 className={styles.title}>
				{user.name} {user.surname}
			</h6>
			<p>{user.email}</p>
			<p>Role:</p>
			<ul className={styles.roles}>
				{user.roles.map((role, index) => (
					<li key={index}>
						<p>{`${role}`}</p>
						<DeleteIcon
							className="interactiveIcon"
							onClick={() => setRoleToDelete(role)}
						/>
					</li>
				))}
			</ul>
			<div className={styles.controls}>
				<EditIcon
					className="interactiveIcon"
					onClick={() => setIsEdit(true)}
				/>
				<AddIcon
					className="interactiveIcon"
					onClick={() => setIsAdd(true)}
				/>
				{user && (
					user.isLocked ? <LockOpenIcon
						className="interactiveIcon"
						onClick={() => setIsUnban(true)}
					/> : <BlockIcon
						className="interactiveIcon"
						onClick={() => setIsBan(true)}
					/>)}

			</div>
			{(roleToDelete) && (
				<Modal
					isClose
					close={() => setRoleToDelete(null)}
					title="Czy na pewno chcesz usunąć rolę?"
				>
					<Confirm
						cancel={() => setRoleToDelete(null)}
						confirm={() => removeRole(roleToDelete)}
						label="Usuń"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
			{(isAdd) && (
				<Modal isClose title="Dodaj role" close={() => setIsAdd(false)}>
					<RoleAssign
						user={user}
						confirm={(user) => {
							onUserPropChange(user);
							setIsAdd(false);
						}}
					/>
				</Modal>
			)}
			{(isEdit) && (
				<Modal
					isClose
					title="Edutuj użytkownika"
					close={() => setIsEdit(false)}
				>
					<EditUser
						user={user}
						confirm={(user) => {
							onUserPropChange(user);
							setIsEdit(false);
						}}
					/>
				</Modal>
			)}
			{(isBan) && (
				<Modal
					isClose
					title="Czy na pewno chcesz zablokować użytkownika?"
					close={() => setIsBan(false)}
				>
					<Confirm
						cancel={() => setIsBan(false)}
						confirm={() => banUser()}
						label="Zablokuj"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
			{(isUnban) && (
				<Modal
					isClose
					title='Czy na pewno chcesz odblokować użytkownika?'
					close={() => setIsUnban(false)}
				>
					<Confirm
						cancel={() => setIsUnban(false)}
						confirm={unbanUser}
						label='Odblokuj'
						style={ButtonStyle.Yellow}
					/>
				</Modal>
			)}
		</div>
	);
}
