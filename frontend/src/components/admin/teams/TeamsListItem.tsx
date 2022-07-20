import styles from "./TeamsListItem.module.css";
import { Team } from "../../../_lib/types";
import { ButtonStyle } from "../../form/Button";
import { useState } from "react";
import Modal from "../../modal/Modal";
import EditTeamPicture from "../../teams/controls/EditTeamPicture";
import EditTeam from "../../teams/controls/EditTeam";
import Confirm from "../../modal/Confirm";
import { useApi } from "../../api/ApiContext";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useAlert } from "../../alert/AlertContext";
import TeamImage from "../../image/TeamImage";


type Props = {
	team: Team;
	onTeamChange: (team: Team) => void;
	onTeamDelete: () => void;
};

export default function TeamsListItem({
	team,
	onTeamChange,
	onTeamDelete,
}: Props) {

	const alert = useAlert();
	const { teamsService } = useApi();

	const [isPicture, setIsPicture] = useState(false);
	const [isEdit, setIsEdit] = useState(false);
	const [isDelete, setIsDelete] = useState(false);

	const capitan = team.players.find((player) => player.id === team.captainId);

	const onDelete = () => {
		return teamsService.adminDeleteTeam(team.id)
			.then((data) => {
				onTeamDelete();
				setIsDelete(false);
				alert.success(data.message);
			});
	};

	return (
		<div className={styles.root}>
			<TeamImage
				url={team.profilePicture}
				size={150}
				placeholderSize={72}
				onClick={() => setIsPicture(true)}
				isEditable
			/>
			<h6 className={styles.title}>{team.name}</h6>
			<p>Kod: {team.code}</p>
			{
				capitan && (
					<p>
						Kapitan: {capitan.name} {capitan.surname}
					</p>
				)
			}
			<div className={styles.manageButtons}>
				<EditIcon
					className="interactiveIcon"
					onClick={() => setIsEdit(true)}
				/>
				{(capitan) && (
					<DeleteIcon
						className="interactiveIcon"
						onClick={() => setIsDelete(true)}
					/>
				)}
			</div>
			{
				isPicture && (
					<Modal
						isClose
						title="Edytuj zdjęcie profilowe"
						close={() => setIsPicture(false)}
					>
						<EditTeamPicture
							isAdmin
							team={team}
							confirm={(url) => {
								onTeamChange({
									...team,
									profilePicture: url,
								});
								setIsPicture(false);
							}}
						/>
					</Modal>
				)
			}
			{
				isEdit && (
					<Modal
						isClose
						title="Edytuj zespół"
						close={() => setIsEdit(false)}
					>
						<EditTeam
							isAdmin
							team={team}
							confirm={(team) => {
								onTeamChange(team);
								setIsEdit(false);
							}}
						/>
					</Modal>
				)
			}
			{
				isDelete && (
					<Modal
						isClose
						title={`Czy na pewno chcesz usunąć zespół - "${team.name}"?`}
						close={() => setIsDelete(false)}
					>
						<Confirm
							cancel={() => setIsDelete(false)}
							confirm={onDelete}
							label="Usuń"
							style={ButtonStyle.Red}
						/>
					</Modal>
				)
			}
		</div >
	);
}
