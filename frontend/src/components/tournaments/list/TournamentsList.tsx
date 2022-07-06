import { useEffect, useState } from "react";
import { ROLES } from "../../../_lib/roles";
import GuardComponent from "../../guard-components/GuardComponent";
import Modal from "../../modal/Modal";
import CreateTournament from "./CreateTournament";
import { useTournaments } from "../TournamentsContext";
import styles from "./TournamentsList.module.css";
import TournamentsListItem from "./TournamentsListItem";
import { SyncLoader } from "react-spinners";
import AddIcon from "@mui/icons-material/Add";
import { useApi } from "../../api/ApiContext";
import { Team } from "../../../_lib/types";
import { useAuth } from "../../auth/AuthContext";

export default function TournamentsList() {

	const { teamsService } = useApi();
	const { session } = useAuth();
	const { tournaments, setTournaments } = useTournaments();

	const [teams, setTeams] = useState<Team[] | null>(null);

	const [isAdd, setIsAdd] = useState(false);

	useEffect(() => {
		if (!session) return;
		teamsService.getTeamsIAmCaptain()
			.then((data) => {
				setTeams(data);
			});
	}, [session, teamsService]);

	return (
		<>
			<div className={styles.top}>
				<span></span>
				<h1>Turnieje</h1>
				<GuardComponent roles={[ROLES.Admin]}>
					<AddIcon
						className="interactiveIcon"
						onClick={() => setIsAdd(true)}
						fontSize="large"
					/>
				</GuardComponent>
			</div>
			<ul className={styles.content}>
				{tournaments ? (
					tournaments.map((tournament, index) => (
						<TournamentsListItem
							key={index}
							tournament={tournament}
							captainTeams={teams}
						/>
					))
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
			</ul>
			{isAdd && (
				<Modal
					close={() => setIsAdd(false)}
					isClose
					title={`Dodaj turniej`}
				>
					<CreateTournament
						confirm={(tournament) => {
							setIsAdd(false);
							setTournaments(
								tournaments
									? [...tournaments, tournament]
									: [tournament]
							);
						}}
					/>
				</Modal>
			)}
		</>
	);
}
