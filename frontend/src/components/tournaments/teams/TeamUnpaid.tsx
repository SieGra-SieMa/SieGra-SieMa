import { TeamInTournament } from "../../../_lib/_types/tournament";
import styles from "./TeamsList.module.css";
import { useState } from "react";
import Modal from "../../modal/Modal";
import Confirm from "../../modal/Confirm";
import { useApi } from "../../api/ApiContext";
import { TeamPaidEnum } from "../../../_lib/types";
import { useTournament } from "../TournamentContext";
import GuardComponent from "../../guard-components/GuardComponent";
import { ROLES } from "../../../_lib/roles";
import AddIcon from "@mui/icons-material/Add";
import { ButtonStyle } from "../../form/Button";

type TeamUnpaidProps = {
	team: TeamInTournament;
};

export default function TeamUnpaid({ team }: TeamUnpaidProps) {

	const { tournamentsService } = useApi();
	const { tournament, setTournament } = useTournament();

	const [isChange, setIsChange] = useState(false);

	const updateTeamStatus = () => {
		tournamentsService
			.setTeamStatus(team.tournamentId, team.teamId, TeamPaidEnum.Paid)
			.then((data) => {
				const newTeams = tournament!.teams.filter((e) => e.teamId !== team.teamId);
				setTournament({
					...tournament!,
					teams: [...newTeams, data],
				});
				setIsChange(false);
			});
	};

	return (
		<li className={styles.team}>
			<p>{team.teamName}</p>
			<GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
				<AddIcon
					className={styles.interactiveIcon}
					onClick={() => setIsChange(true)}
					fontSize="medium"
					style={{ color: "var(--accent-color)" }}
				/>
				{isChange && (
					<Modal
						title={`Potwierdzenie opłaty - Zespół "${team.teamName}"`}
						isClose
						close={() => setIsChange(false)}
					>
						<Confirm
							cancel={() => setIsChange(false)}
							confirm={updateTeamStatus}
							label="Potwierdź"
							style={ButtonStyle.Yellow}
						/>
					</Modal>
				)}
			</GuardComponent>
		</li>
	);
}
