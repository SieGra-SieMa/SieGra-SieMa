import styles from "./UsersList.module.css";
import UsersListItem from "./UsersListItem";
import { useState, useEffect } from "react";
import { User } from "../../../_lib/types";
import { useApi } from "../../api/ApiContext";
import { useUser } from "../../user/UserContext";
import Input from "../../form/Input";
import Loader from "../../loader/Loader";
import { useSearchParams } from "react-router-dom";
import Pagination from "../../pagination/Pagination";

export const COUNT = 12;

export default function UsersList() {

	const [searchParams, setSearchParams] = useSearchParams();

	const { usersService } = useApi();
	const { user } = useUser();

	const [users, setUsers] = useState<User[] | null>(null);
	const [search, setSearch] = useState("");
	const [totalCount, setTotalCount] = useState(0);

	const pageParam = parseInt(searchParams.get('page') || '1');
	const page = isNaN(pageParam) ? 1 : pageParam;

	const totalPages = Math.ceil(totalCount / COUNT);

	useEffect(() => {
		if (!user) return;
		setUsers(null);
		usersService.getUsers(page, COUNT, search)
			.then((result) => {
				setTotalCount(result.totalCount);
				setUsers(result.items.filter(
					(e) => user.id !== e.id
				));
			});
	}, [search, page, user, usersService]);

	const onUserPropChange = (user: User) => {
		const data = users ? [...users] : [];
		const index = data.findIndex((e) => e.id === user.id);
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
				onChange={(e) => {
					setSearchParams({ page: '1' });
					setSearch(e.target.value)
				}}
			/>
			<Pagination totalPages={totalPages}>
				{users ? (
					<div className={styles.content}>
						{users.map((user, index) => (
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
			</Pagination>
		</div>
	);
}
