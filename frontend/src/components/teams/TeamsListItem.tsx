import styles from "./TeamsListItem.module.css";
import { Player, Team } from "../../_lib/types";
import { useCallback, useState } from "react";
import Modal from "../modal/Modal";
import Confirm from "../modal/Confirm";
import AddParticipant from "./controls/AddParticipant";
import { useApi } from "../api/ApiContext";
import Button, { ButtonStyle } from "../form/Button";
import { useUser } from "../user/UserContext";
import EditTeam from "./controls/EditTeam";
import { useTeams } from "./TeamsContext";
import PlayersToSetCapitan from "./controls/PlayersToSetCapitan";
import EditTeamPicture from "./controls/EditTeamPicture";
import TeamImage from "../image/TeamImage";
import EditIcon from "@mui/icons-material/Edit";
import AddIcon from "@mui/icons-material/Add";
import CloseIcon from "@mui/icons-material/Close";

type Props = {
	team: Team;
};

export default function TeamsListItem({ team }: Props) {
	const { teamsService } = useApi();
	const { user } = useUser();

	const { teams, setTeams } = useTeams();

	const [playerToRemove, setPlayerToRemove] = useState<Player | null>(null);

	const [isAdd, setIsAdd] = useState(false);
	const [isSetCapitan, setIsSetCapitan] = useState(false);
	const [isEdit, setIsEdit] = useState(false);
	const [isLeave, setIsLeave] = useState(false);
	const [isPicture, setIsPicture] = useState(false);

	const captain = team.players.find((player) => player.id === team.captainId);

	const leaveTeam = useCallback(() => {
		return teamsService.leaveTeam(team.id).then(() => {
			const data = teams ? [...teams] : [];
			const index = data.findIndex((e) => e.id === team.id) ?? -1;
			if (index >= 0) {
				data.splice(index, 1);
				setTeams(data);
			}
			setIsLeave(false);
		});
	}, [team.id, teams, setTeams, teamsService]);

	const removePlayer = useCallback(() => {
		if (!playerToRemove) return;
		return teamsService
			.removePlayer(team.id, playerToRemove.id)
			.then((team) => {
				const data = teams ? [...teams] : [];
				const index = data.findIndex((e) => e.id === team.id) ?? -1;
				if (index >= 0) {
					data[index] = team;
					setTeams(data);
				}
				setPlayerToRemove(null);
			});
	}, [playerToRemove, team.id, teams, setTeams, teamsService]);

	return (
		<div className={styles.root}>
			<div className={styles.content}>
				<TeamImage
					className={styles.image}
					url={team.profilePicture}
					size={150}
					placeholderSize={72}
					onClick={() => setIsPicture(true)}
				/>

				<div className={styles.titleContainer}>
					<span></span>
					<h4 className={styles.title}>{team.name}</h4>
					{user && team.captainId === user.id && (
						<EditIcon
							className="interactiveIcon"
							onClick={() => setIsEdit(true)}
						/>
					)}
				</div>
				<p>
					Kod: <b>{team.code}</b>
				</p>
				<p>
					Kapitan:{" "}
					<b>
						{captain
							? `${captain.name} ${captain.surname}`
							: "Użytkownik"}
					</b>
				</p>
				<div className={styles.playersAdd}>
					<p>Członkowie:</p>
					{user && team.captainId === user.id && (
						<AddIcon
							className="interactiveIcon"
							onClick={() => setIsAdd(true)}
						/>
					)}
				</div>

				<ul className={styles.players}>
					{team.players.length > 0 ? (
						team.players
							.filter((player) => player.id !== team.captainId)
							.map((player, index) => (
								<li key={index}>
									<p>{`${player.name} ${player.surname}`}</p>
									<CloseIcon
										className="interactiveIcon"
										onClick={() =>
											setPlayerToRemove(player)
										}
									/>
								</li>
							))
					) : (
						<div>Null</div>
					)}
				</ul>
				<div className={styles.controls}>
					{user && team.captainId === user.id && (
						<>
							<Button
								value="Zmień kapitana"
								onClick={() => setIsSetCapitan(true)}
								style={ButtonStyle.TransparentBorder}
							/>
						</>
					)}
					<Button
						value="Opuść zespół"
						onClick={() => setIsLeave(true)}
						style={ButtonStyle.Red}
					/>
				</div>
			</div>
			{isAdd && (
				<Modal
					close={() => setIsAdd(false)}
					isClose
					title={`Zespół "${team.name}" - Dodaj gracza`}
				>
					<AddParticipant
						team={team}
						confirm={() => {
							setIsAdd(false);
						}}
					/>
				</Modal>
			)}
			{isEdit && (
				<Modal
					isClose
					close={() => setIsEdit(false)}
					title={`Edytuj zespół - "${team.name}"`}
				>
					<EditTeam
						team={team}
						confirm={(team) => {
							setIsEdit(false);
							if (teams) {
								const index = teams.findIndex(
									(e) => e.id === team.id
								);
								const data = [...teams];
								data[index] = {
									...team,
									players: teams[index].players,
								};
								setTeams(data);
							}
						}}
					/>
				</Modal>
			)}
			{isPicture && (
				<Modal
					isClose
					close={() => setIsPicture(false)}
					title={`Zespół "${team.name}" - Wybierz zdjęcie profilowe`}
				>
					<EditTeamPicture
						team={team}
						confirm={(url) => {
							setIsPicture(false);
							if (teams) {
								const index = teams.findIndex(
									(e) => e.id === team.id
								);
								const data = [...teams];
								data[index] = {
									...team,
									profilePicture: url,
								};
								setTeams(data);
							}
						}}
					/>
				</Modal>
			)}
			{isLeave && (
				<Modal
					close={() => setIsLeave(false)}
					title={`Czy na pewno chcesz opuścić zespół - "${team.name}"?`}
				>
					<Confirm
						cancel={() => setIsLeave(false)}
						confirm={leaveTeam}
						label="Opuść"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
			{isSetCapitan && (
				<Modal
					isClose
					close={() => setIsSetCapitan(false)}
					title={`Zespół "${team.name}" -  Wybierz kapitana`}
				>
					<PlayersToSetCapitan
						team={team}
						confirm={() => setIsSetCapitan(false)}
					/>
				</Modal>
			)}
			{playerToRemove && (
				<Modal
					isClose
					close={() => setPlayerToRemove(null)}
					title={`Czy na pewno chcesz usunąć gracza ${playerToRemove.name} ${playerToRemove.surname}?`}
				>
					<Confirm
						cancel={() => setPlayerToRemove(null)}
						confirm={() => removePlayer()}
						label="Usuń"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
		</div>
	);
}
