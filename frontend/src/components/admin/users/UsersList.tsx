import styles from "./UsersList.module.css";
import UsersListItem from "./UsersListItem";
import { useState, useEffect } from "react";
import { User } from "../../../_lib/types";
import { useApi } from "../../api/ApiContext";
import { useUser } from "../../user/UserContext";
import Input from "../../form/Input";
import Loader from "../../loader/Loader";

export default function UsersList() {
	const { usersService } = useApi();
	const { user } = useUser();

	const [users, setUsers] = useState<User[] | null>(null);
	const [search, setSearch] = useState("");

	useEffect(() => {
		if (!user) return;
		return usersService.getUsers()
			.then((result) => setUsers(result.filter(
				(e) => user.id !== e.id
			)))
			.abort;
	}, [user, usersService]);

	const onUserPropChange = (user: User) => {
		const data = users ? [...users] : [];
		const index = data.findIndex((e) => e.id === user.id);
		console.log(data, index, user);
		if (index >= 0) {
			data[index] = user;
			setUsers(data);
		}
	};

	return (
		<div className={styles.root}>
			<div className={styles.top}>
				<h1>Użytkownicy</h1>
			</div>
			<Input
				placeholder="Wyszukaj..."
				value={search}
				onChange={(e) => setSearch(e.target.value)}
			/>
			{users ? (
				<div className={styles.content}>
					{users.filter((user) =>
						user.name.toLowerCase().includes(search.toLowerCase()) ||
						user.surname.toLowerCase().includes(search.toLowerCase()) ||
						user.email.toLowerCase().includes(search.toLowerCase())
					).map((user, index) => (
						<UsersListItem
							key={index}
							user={user}
							onUserPropChange={onUserPropChange}
						/>
					))}
				</div>
			) : (
				<Loader size={20} margin={40} />
			)}
		</div>
	);
}
