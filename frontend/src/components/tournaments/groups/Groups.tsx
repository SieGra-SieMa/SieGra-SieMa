import { useState } from "react";
import { Group, Tournament } from "../../../_lib/_types/tournament";
import styles from "./Groups.module.css";

type GroupsProps = {
	tournament: Tournament;
};

export default function Groups({ tournament }: GroupsProps) {

	const [currentGroup, setCurrentGroup] = useState<Group>(tournament.groups[0]);
	const [opacity, setOpacity] = useState(1);

	return (
		<div className={styles.root}>
			<div className={styles.container}>
				<h4 className="underline" id={styles.caption} style={{ width: "fit-content" }}>Grupy</h4>
				<div className={styles.groups}>
					{tournament.groups.filter((group) => !group.ladder).map((group) => (
						<h6
							key={group.id}
							className={group === currentGroup ? styles.active : undefined}
							id={styles.selectable}
							onClick={() => {
								setOpacity(0);
								setTimeout(setCurrentGroup, 200, group);
								setTimeout(setOpacity, 200, 1);
							}}
						>
							Grupa {group.name}
						</h6>
					))}
				</div>
				<div className={styles.group}>
					<table style={{ opacity: `${opacity}` }}>
						<thead>
							<tr>
								<th>Miejsce</th>
								<th className={styles.sticky}>Zespół</th>
								<th>Mecze</th>
								<th>Wygrane</th>
								<th>Przegrane</th>
								<th>Remisy</th>
								<th>Zdobyte</th>
								<th>Stracone</th>
								<th>Wynik</th>
							</tr>
						</thead>
						<tbody>
							{currentGroup.teams &&
								currentGroup.teams
									.sort((a, b) => b.goalScored - a.goalScored)
									.sort((a, b) => b.points - a.points)
									.map((team, index) => (
										<tr key={index}>
											<td>{index + 1}</td>
											<td className={styles.sticky}>{tournament.teams.find((e) => e.teamId === team.idTeam)?.teamName}</td>
											<td>{team.playedMatches}</td>
											<td>{team.wonMatches}</td>
											<td>{team.lostMatches}</td>
											<td>{team.tiedMatches}</td>
											<td>{team.goalScored}</td>
											<td>{team.goalConceded}</td>
											<td>{team.points}</td>
										</tr>
									))}
						</tbody>
					</table>
				</div>
			</div>
		</div>
	);
}
