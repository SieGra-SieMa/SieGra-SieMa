import styles from "./AdminTeamsList.module.css";
import { useState, useEffect } from "react";
import { Team } from "../../../_lib/types";
import { useApi } from "../../api/ApiContext";
import TeamsListItem from "./TeamsListItem";
import Input from "../../form/Input";
import Loader from "../../loader/Loader";
import Pagination from "../../pagination/Pagination";
import { useSearchParams } from "react-router-dom";

export const COUNT = 12;

export default function AdminTeamsList() {

	const [searchParams, setSearchParams] = useSearchParams();

	const { teamsService } = useApi();

	const [teams, setTeams] = useState<Team[] | null>(null);
	const [search, setSearch] = useState("");
	const [totalCount, setTotalCount] = useState(0);

	const pageParam = parseInt(searchParams.get('page') || '1');
	const page = isNaN(pageParam) ? 1 : pageParam;

	const totalPages = Math.ceil(totalCount / COUNT);

	useEffect(() => {
		setTeams(null);
		return teamsService.getAllTeams(page, COUNT, search)
			.then((result) => {
				setTotalCount(result.totalCount);
				setTeams(result.items);
			}).abort;
	}, [search, page, teamsService]);

	const onTeamChange = (team: Team) => {
		if (!teams) return;
		const index = teams.findIndex((e) => e.id === team.id);
		if (index >= 0) {
			const updatedTeams = [...teams];
			updatedTeams[index] = team;
			setTeams(updatedTeams);
		}
	};

	return (
		<div className={styles.root}>
			<div className={styles.top}>
				<h1>Zespoły</h1>
			</div>
			<Input
				placeholder="Wyszukaj..."
				value={search}
				onChange={(e) => {
					setSearchParams({ page: '1' })
					setSearch(e.target.value);
				}}
			/>
			<Pagination totalPages={totalPages}>
				{(teams) ?
					((teams.length > 0) ? (
						<div className={styles.content}>
							{teams.filter((team) =>
								team.name.toLowerCase().includes(search.toLowerCase()) ||
								team.code.toLowerCase().includes(search.toLowerCase())
							).map((team, index) => (
								<TeamsListItem
									key={index}
									team={team}
									onTeamChange={onTeamChange}
								/>
							))}
						</div>
					) : (
						<h4 style={{
							justifySelf: 'center',
							alignSelf: 'center',
							flex: 1,
							display: 'flex',
							alignItems: 'center'
						}}>
							Nie znaleziono
						</h4>
					)
					) : (
						<Loader size={20} margin={40} />
					)}
			</Pagination>
		</div>
	);
}
