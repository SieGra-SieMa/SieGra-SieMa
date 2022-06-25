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
			<h4 className="underline" style={{ width: "fit-content" }}>Zespoły</h4>
			{paidTeams ? (
				<div className={styles.root}>
					<div>
						<ul className={styles.teams}>
							{paidTeams.map((team) => (
								<TeamPaid key={team.teamId} team={team} />
							))}

							<GuardComponent
								roles={[ROLES.Employee, ROLES.Admin]}
							>
								<h6 className="underline" style={{ width: "fit-content" }}>Oczekiwanie</h6>
								{unpaidTeams &&
									unpaidTeams.map((team) => (
										<TeamUnpaid
											key={team.teamId}
											team={team}
										/>
									))}
							</GuardComponent>
						</ul>
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
