import { useState } from "react";
import { Tournament } from "../../../_lib/_types/tournament";
import Button, { ButtonStyle } from "../../form/Button";
import Modal from "../../modal/Modal";
import styles from "./Groups.module.css";


type Props = {
	tournament: Tournament;
};

export default function Groups({ tournament }: Props) {
	const [currentGroupId, setCurrentGroupId] = useState<number | null>(null);
	const [isSelectGroup, setIsSelectGroup] = useState(false);
	const [opacity, setOpacity] = useState(1);

	const currentGroup = tournament.groups.find((g) => currentGroupId === null || g.id === currentGroupId);

	return (
		<div className={styles.root}>
			<div className={styles.container}>
				<h4
					className="underline"
					id={styles.caption}
					style={{ width: "fit-content" }}
				>
					Grupy
				</h4>
				{currentGroup && (
					<Button
						id={styles.selectGroupButton}
						value={"Grupa " + currentGroup.name}
						onClick={() => setIsSelectGroup(true)}
						style={ButtonStyle.TransparentBorder}
					/>
				)}
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
							{currentGroup && currentGroup.teams &&
								currentGroup.teams
									.sort((a, b) => b.goalScored - a.goalScored)
									.sort((a, b) => b.points - a.points)
									.map((team, index) => (
										<tr key={index}>
											<td>{index + 1}</td>
											<td className={styles.sticky}>
												{
													tournament.teams.find(
														(e) =>
															e.teamId ===
															team.idTeam
													)?.teamName
												}
											</td>
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
			{tournament && isSelectGroup && (
				<Modal
					title="Wybierz grupę"
					isClose
					close={() => setIsSelectGroup(false)}
				>
					<div className={styles.selectGroup}>
						{tournament.groups.map((group) => (
							<Button
								value={"Grupa " + group.name}
								className={
									group === currentGroup
										? styles.active
										: ""
								}
								onClick={() => {
									setOpacity(0);
									setTimeout(setCurrentGroupId, 200, group.id);
									setTimeout(setOpacity, 200, 1);
									setIsSelectGroup(false);
								}}
								key={group.id}
								style={ButtonStyle.TransparentBorder}
							/>
						))}
					</div>
				</Modal>
			)}
		</div>
	);
}
