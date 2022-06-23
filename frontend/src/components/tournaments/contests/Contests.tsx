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

type ContestsProps = {
	contests: ContestType[];
	tournamentId: string;
};

export default function Contests({ contests, tournamentId }: ContestsProps) {
	const [isAddContest, setIsAddContest] = useState(false);
	const [isAddScore, setIsAddScore] = useState(false);

	const [currentContest, setCurrentContest] = useState(contests[0]);
	const [isSelectContest, setIsSelectContest] = useState(false);

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
						<AddIcon
							className="interactiveIcon"
							onClick={() => setIsAddContest(true)}
							fontSize="large"
						/>
					</GuardComponent>
					<Button
						id={styles.selectGroupButton}
						value={currentContest.name}
						onClick={() => setIsSelectContest(true)}
						style={ButtonStyle.TransparentBorder}
					/>
					<GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
						<ScoreboardIcon
							className="interactiveIcon"
							onClick={() => setIsAddScore(true)}
						/>
					</GuardComponent>
				</div>
				<div className={styles.contests}>
					<Contest key={currentContest.id} contest={currentContest} />
				</div>
			</div>

			{isAddContest && (
				<Modal
					title="Dodanie konkursu"
					isClose
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
			<GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
				{isAddScore && (
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
