import styles from "./AdminTeamsList.module.css";
import { useState, useEffect } from "react";
import { Team } from "../../../_lib/types";
import { useApi } from "../../api/ApiContext";
import TeamsListItem from "./TeamsListItem";
import Input from "../../form/Input";
import Loader from "../../loader/Loader";


export default function AdminTeamsList() {
	const { teamsService } = useApi();

	const [teams, setTeams] = useState<Team[] | null>(null);
	const [search, setSearch] = useState("");

	useEffect(() => {
		return teamsService.getAllTeams()
			.then((result) => setTeams(result))
			.abort;
	}, [teamsService]);

	const onTeamChange = (team: Team) => {
		if (!teams) return;
		const index = teams.findIndex((e) => e.id === team.id);
		if (index >= 0) {
			const updatedTeams = [...teams];
			updatedTeams[index] = team;
			setTeams(updatedTeams);
		}
	};

	const onTeamDelete = (team: Team) => {
		if (!teams) return;
		const index = teams.findIndex((e) => e.id === team.id);
		if (index >= 0) {
			setTeams(teams.filter((e) => e === team));
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
				onChange={(e) => setSearch(e.target.value)}
			/>
			{teams ? (
				<div className={styles.content}>
					{teams.filter((team) =>
						team.name.toLowerCase().includes(search.toLowerCase()) ||
						team.code.toLowerCase().includes(search.toLowerCase())
					).map((team, index) => (
						<TeamsListItem
							key={index}
							team={team}
							onTeamChange={onTeamChange}
							onTeamDelete={onTeamDelete}
						/>
					))}
				</div>
			) : (
				<Loader size={20} margin={40} />
			)}
		</div>
	);
}
