import Config from "../../../config.json";
import { useNavigate } from "react-router-dom";
import { TournamentListItem as TournamentListItemType } from "../../../_lib/_types/tournament";
import ImageIcon from "@mui/icons-material/Image";
import styles from "./TournamentsList.module.css";
import Button from "../../form/Button";
import { useState } from "react";
import Modal from "../../modal/Modal";
import TeamAssign from "./TeamAssign";
import { useTournaments } from "../TournamentsContext";
import { useAuth } from "../../auth/AuthContext";
import LocationOnOutlinedIcon from "@mui/icons-material/LocationOnOutlined";
import { Team } from "../../../_lib/types";

type TournamentsListItemProps = {
	tournament: TournamentListItemType;
	captainTeams: Team[] | null;
};

export default function TournamentsListItem({
	tournament,
	captainTeams
}: TournamentsListItemProps) {
	const navigate = useNavigate();

	const { session } = useAuth();
	const { tournaments, setTournaments } = useTournaments();

	const [isTeamAssign, setIsTeamAssign] = useState(false);

	return (
		<>
			<li
				className={styles.item}
				onClick={() => navigate(`${tournament.id}`)}
			>
				<div
					className={styles.pictureBlock}
					style={
						tournament.profilePicture
							? {
									backgroundImage: `url(${Config.HOST}${tournament.profilePicture})`,
							  }
							: undefined
					}
				>
					{!tournament.profilePicture && (
						<ImageIcon className={styles.picture} />
					)}
				</div>
				<div className={styles.itemContent}>
					<div className={styles.itemDetails}>
						<h4 className={styles.itemTitle}>{tournament.name}</h4>
						<p className={styles.dates}>
							{new Date(tournament.startDate).toLocaleString()}
							<span className={styles.line}> | </span>
							{new Date(tournament.endDate).toLocaleString()}
						</p>
						<div className={styles.address}>
							<LocationOnOutlinedIcon
								fontSize="medium"
								style={{ color: "var(--accent-color)" }}
							/>
							<p>{tournament.address}</p>
						</div>
						<div
							dangerouslySetInnerHTML={{
								__html: `${tournament.description}`,
							}}
						></div>
					</div>
					{session && tournament.isOpen && !tournament.team && captainTeams?.length !== 0 && (
						<>
							<Button
								value="Zapisz zespół"
								onClick={(e) => {
									e.stopPropagation();
									setIsTeamAssign(true);
								}}
							/>
						</>
					)}
				</div>
			</li>
			{session && tournament.isOpen && !tournament.team && isTeamAssign && (
				<Modal
					title={`Zapisz zespół - "${tournament.name}"`}
					isClose
					close={() => setIsTeamAssign(false)}
				>
					<TeamAssign
						id={tournament.id}
						confirm={(team) => {
							setIsTeamAssign(false);
							const updatedTournament = {
								...tournament,
								team,
							};
							const index = tournaments!.findIndex(
								(e) => e.id === updatedTournament.id
							);
							const data = [...tournaments!];
							data[index] = updatedTournament;
							setTournaments(data);
						}}
						teams={captainTeams}
					/>
				</Modal>
			)}
		</>
	);
}
