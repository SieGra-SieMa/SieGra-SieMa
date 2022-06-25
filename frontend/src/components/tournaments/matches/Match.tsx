import { useState } from "react";
import { Match as MatchType } from "../../../_lib/_types/tournament";
import Modal from "../../modal/Modal";
import styles from "./Matches.module.css";
import MatchResult from "./MatchResult";

type MatchProps = {
	match: MatchType & { groupId: number };
};

export default function Match({ match }: MatchProps) {
	const [isEdit, setIsEdit] = useState(false);

	const [homeScore, setHomeScore] = useState(match.teamHomeScore);
	const [awayScore, setAwayScore] = useState(match.teamAwayScore);

	return (
		<>
			<div className={styles.match} onClick={() => setIsEdit(true)}>
				<h6 className={styles.teamName}>{match.teamHome}</h6>
				<h6 className={styles.teamName}>{match.teamAway}</h6>
				<span className={styles.vs}>VS</span>
				<p>{homeScore ?? "-"}</p>
				<p>{awayScore ?? "-"}</p>
			</div>
			{isEdit && (
				<Modal
					isClose
					close={() => setIsEdit(false)}
					title={"Match result"}
				>
					<MatchResult
						match={match}
						confirm={() => setIsEdit(false)}
						callback={(homeScore: number, awayScore: number) => {
							setHomeScore(homeScore);
							setAwayScore(awayScore);
						}}
					/>
				</Modal>
			)}
		</>
	);
}
