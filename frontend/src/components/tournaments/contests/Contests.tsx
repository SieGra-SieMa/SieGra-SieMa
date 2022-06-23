import { ROLES } from "../../../_lib/roles";
import { Contest as ContestType} from "../../../_lib/_types/tournament";
import GuardComponent from "../../guard-components/GuardComponent";
import AddIcon from "@mui/icons-material/Add";
import styles from "./Contests.module.css";
import { useState } from "react";
import Contest from "./Contest";
import Modal from "../../modal/Modal";
import CreateContest from "./CreateContest";

type ContestsProps = {
	contests: ContestType[],
    tournamentId: string;
};

export default function Contests({ contests, tournamentId }: ContestsProps) {
	const [isAddContest, setIsAddContest] = useState(false);

	return (
		<>
			<h4>Konkursy</h4>
			<GuardComponent roles={[ROLES.Admin]}>
				<AddIcon className="interactiveIcon" onClick={() => setIsAddContest(true)} />
			</GuardComponent>

			<div className={styles.contests}>
				{contests.map((contest) => (
					<Contest key={contest.id} contest={contest} />
				))}
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
		</>
	);
}
