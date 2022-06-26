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

type ContestsProps = {
	contests: ContestType[];
	tournamentId: string;
};

export default function Contests({ contests, tournamentId }: ContestsProps) {
	const { tournamentsService } = useApi();

	const [currentContest, setCurrentContest] = useState<
		ContestType | undefined
	>(contests[0]);
	const [isAddContest, setIsAddContest] = useState(false);
	const [isAddScore, setIsAddScore] = useState(false);
	const [isEditContest, setIsEditContest] = useState(false);
	const [isDeleteContest, setIsDeleteContest] = useState(false);
	const [isSelectContest, setIsSelectContest] = useState(false);

	const onDelete = () => {
		if (!currentContest) return;
		tournamentsService
			.deleteContest(currentContest.tournamentId, currentContest.id)
			.then(() => {
				setIsDeleteContest(false);
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
								console.log(data);
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
							confirm={() => {
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
									setCurrentContest(contest);
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
