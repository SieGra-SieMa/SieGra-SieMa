import { ROLES } from "../../../_lib/roles";
import { Contest as ContestType } from "../../../_lib/_types/tournament";
import GuardComponent from "../../guard-components/GuardComponent";
import AddIcon from "@mui/icons-material/Add";
import styles from "./Contests.module.css";
import { useState } from "react";
import Contest from "./Contest";
import Modal from "../../modal/Modal";
import CreateContest from "./CreateContest";
import AddScoreContest from "./AddScoreContest";
import ScoreboardIcon from "@mui/icons-material/Scoreboard";
import Button, { ButtonStyle } from "../../form/Button";
import EditContest from "./EditContest";
import Confirm from "../../modal/Confirm";
import { useApi } from "../../api/ApiContext";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useTournament } from "../TournamentContext";
import { useAlert } from "../../alert/AlertContext";


type Props = {
	contests: ContestType[];
	tournamentId: string;
};

export default function Contests({ contests, tournamentId }: Props) {

	const alert = useAlert();
	const { tournamentsService } = useApi();
	const { tournament, setTournament } = useTournament();

	const [currentContestId, setCurrentContestId] = useState<number | null>(null);
	const [isAddContest, setIsAddContest] = useState(false);
	const [isAddScore, setIsAddScore] = useState(false);
	const [isEditContest, setIsEditContest] = useState(false);
	const [isDeleteContest, setIsDeleteContest] = useState(false);
	const [isSelectContest, setIsSelectContest] = useState(false);

	const currentContest = contests.find((c) => currentContestId === null || c.id === currentContestId);

	const onDelete = () => {
		if (!currentContest) return;
		return tournamentsService.deleteContest(currentContest.tournamentId, currentContest.id)
			.then((data) => {
				setIsDeleteContest(false);
				alert.success(data.message);
				setTournament({
					...tournament,
					contests: tournament.contests.filter((e) => currentContest.id !== e.id)
				})
				setCurrentContestId(null);
			});
	};

	return (
		<>
			<div className={styles.root}>
				<h4
					className="underline"
					style={{
						width: "fit-content",
						padding: 0,
						marginTop: "25px",
					}}
				>
					Konkursy
				</h4>
				<div className={styles.header}>
					<div style={{ width: '100%' }}>
						<GuardComponent roles={[ROLES.Admin]}>
							<div className={styles.manageButtons}>
								<AddIcon
									className="interactiveIcon"
									onClick={() => setIsAddContest(true)}
									fontSize="medium"
								/>
								<EditIcon
									className="interactiveIcon"
									onClick={() => setIsEditContest(true)}
									fontSize="medium"
								/>
								<DeleteIcon
									className="interactiveIcon"
									onClick={() => setIsDeleteContest(true)}
									fontSize="medium"
								/>
							</div>
						</GuardComponent>
					</div>
					{currentContest && (
						<>
							<Button
								id={styles.selectGroupButton}
								value={currentContest.name}
								onClick={() => setIsSelectContest(true)}
								style={ButtonStyle.TransparentBorder}
							/>
							<GuardComponent
								roles={[ROLES.Employee, ROLES.Admin]}
							>
								<ScoreboardIcon
									className="interactiveIcon"
									onClick={() => setIsAddScore(true)}
									fontSize="medium"
								/>
							</GuardComponent>
						</>
					)}
				</div>
				<div className={styles.contests}>
					{currentContest && <Contest contest={currentContest} />}
				</div>
			</div>

			<GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
				{isAddContest && (
					<Modal
						isClose
						title='Dodaj konkurs'
						close={() => setIsAddContest(false)}
					>
						<CreateContest
							tournamentId={tournamentId}
							confirm={(data) => {
								setTournament({
									...tournament,
									contests: tournament.contests.concat(data),
								})
								setIsAddContest(false);
							}}
						/>
					</Modal>
				)}
				{isAddScore && currentContest && (
					<Modal
						title={`Konkurs - "${currentContest.name}"`}
						isClose
						close={() => setIsAddScore(false)}
					>
						<AddScoreContest
							contest={currentContest}
							confirm={(data) => {
								const index = currentContest.contestants.findIndex((e) => e.userId === data.userId);
								const updatedContest = { ...currentContest };
								if (index >= 0) {
									updatedContest.contestants[index] = data;
								} else {
									updatedContest.contestants = updatedContest.contestants.concat(data);
								}
								if (data.points === 0) {
									updatedContest.contestants = updatedContest.contestants.filter((e) => e.userId !== data.userId)
								}
								updatedContest.contestants = updatedContest.contestants.sort((a, b) => b.points - a.points);
								const contestIndex = tournament.contests.findIndex((e) => e.id === updatedContest.id);
								const updatedContests = [
									...tournament.contests
								];
								updatedContests[contestIndex] = updatedContest;
								setTournament({
									...tournament,
									contests: updatedContests,
								})
								setIsAddScore(false);
							}}
						/>
					</Modal>
				)}
				{isEditContest && currentContest && (
					<Modal
						title={`Edytuj konkurs - "${currentContest.name}"`}
						isClose
						close={() => setIsEditContest(false)}
					>
						<EditContest
							contest={currentContest}
							confirm={() => {
								setIsEditContest(false);
							}}
						/>
					</Modal>
				)}
				{isDeleteContest && currentContest && (
					<Modal
						title={`Czy na pewno chcesz usunąć konkurs - "${currentContest.name}"?`}
						isClose
						close={() => setIsDeleteContest(false)}
					>
						<Confirm
							label="Usuń"
							cancel={() => setIsDeleteContest(false)}
							confirm={onDelete}
							style={ButtonStyle.Red}
						/>
					</Modal>
				)}
			</GuardComponent>
			{isSelectContest && (
				<Modal
					title="Wybierz konkurs"
					isClose
					close={() => setIsSelectContest(false)}
				>
					<div className={styles.selectContest}>
						{contests.map((contest) => (
							<Button
								value={contest.name}
								className={
									contest === currentContest
										? styles.active
										: ""
								}
								onClick={() => {
									setCurrentContestId(contest.id);
									setIsSelectContest(false);
								}}
								key={contest.id}
								style={ButtonStyle.TransparentBorder}
							/>
						))}
					</div>
				</Modal>
			)}
		</>
	);
}
