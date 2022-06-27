import { TeamInTournament } from "../../../_lib/_types/tournament";
import styles from "./TeamsList.module.css";
import { SyncLoader } from "react-spinners";
import { useEffect, useState } from "react";
import TeamUnpaid from "./TeamUnpaid";
import TeamPaid from "./TeamPaid";
import GuardComponent from "../../guard-components/GuardComponent";
import { ROLES } from "../../../_lib/roles";

type TeamsListProps = {
	teams: TeamInTournament[] | null;
};

export default function TeamsList({ teams }: TeamsListProps) {
	const [paidTeams, setPaidTeams] = useState(
		teams?.filter((team) => team.paid)
	);
	const [unpaidTeams, setUnpaidTeams] = useState(
		teams?.filter((team) => !team.paid)
	);

	useEffect(() => {
		setPaidTeams(teams?.filter((team) => team.paid));
		setUnpaidTeams(teams?.filter((team) => !team.paid));
	}, [teams]);

	return (
		<>
			<h4 className="underline" style={{ width: "fit-content" }}>
				Zespoły
			</h4>
			{paidTeams ? (
				<div className={styles.root}>
					<div>
						<div className={styles.teamsContainer}>
							<div>
								<h6
									className="underline"
									style={{ width: "fit-content" }}
								>
									Opłacone
								</h6>
								<ul className={styles.teams}>
									{paidTeams.map((team) => (
										<TeamPaid
											key={team.teamId}
											team={team}
										/>
									))}
								</ul>
							</div>

							<GuardComponent
								roles={[ROLES.Employee, ROLES.Admin]}
							>
								<div>
									<h6
										className="underline"
										style={{ width: "fit-content" }}
									>
										Oczekiwanie
									</h6>
									<ul className={styles.teams}>
										{unpaidTeams &&
											unpaidTeams.map((team) => (
												<TeamUnpaid
													key={team.teamId}
													team={team}
												/>
											))}
									</ul>
								</div>
							</GuardComponent>
						</div>
					</div>
				</div>
			) : (
				<div className={styles.loader}>
					<SyncLoader
						loading={true}
						size={20}
						margin={20}
						color="#fff"
					/>
				</div>
			)}
		</>
	);
}
