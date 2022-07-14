import Config from '../../../config.json';
import { useState } from "react";
import { Match as MatchType } from "../../../_lib/_types/tournament";
import Modal from "../../modal/Modal";
import { useTournament } from "../TournamentContext";
import styles from "./Matches.module.css";
import MatchResult from "./MatchResult";
import ImageIcon from '@mui/icons-material/Image';


type Props = {
	match: MatchType & { groupId: number };
};

export default function Match({ match }: Props) {

	const { tournament } = useTournament();

	const [isEdit, setIsEdit] = useState(false);
	const [homeScore, setHomeScore] = useState(match.teamHomeScore);
	const [awayScore, setAwayScore] = useState(match.teamAwayScore);

	const teamHome = tournament!.teams.find((team) => team.teamId === match.teamHomeId);
	const teamAway = tournament!.teams.find((team) => team.teamId === match.teamAwayId);

	return (
		<>
			<div className={styles.match} onClick={() => setIsEdit(true)}>
				<div className={styles.block}>
					<div className={styles.pictureBlock} style={teamHome?.teamProfileUrl ? {
						backgroundImage: `url(${Config.HOST}${teamHome.teamProfileUrl})`
					} : undefined}>
						{(!teamHome?.teamProfileUrl) && (<ImageIcon className={styles.picture} />)}
					</div>
					<div className={styles.teamBlock}>
						<h5 className={styles.teamName}>{teamHome?.teamName}</h5>
						<p>{homeScore ?? "-"}</p>
					</div>
				</div>
				<span className={styles.vs}>VS</span>
				<div className={styles.block}>
					<div className={styles.pictureBlock} style={teamAway?.teamProfileUrl ? {
						backgroundImage: `url(${Config.HOST}${teamAway.teamProfileUrl})`
					} : undefined}>
						{(!teamAway?.teamProfileUrl) && (<ImageIcon className={styles.picture} />)}
					</div>
					<div className={styles.teamBlock}>
						<h5 className={styles.teamName}>{teamAway?.teamName}</h5>
						<p>{awayScore ?? "-"}</p>
					</div>
				</div>
			</div>
			{isEdit && (
				<Modal
					isClose
					close={() => setIsEdit(false)}
					title='Wynik meczu'
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
