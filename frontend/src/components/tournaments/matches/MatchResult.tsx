import { FormEvent, useState } from "react";
import { ROLES } from "../../../_lib/roles";
import {
	Match as MatchType,
	MatchResult as MatchResultType,
} from "../../../_lib/_types/tournament";
import { useAlert } from "../../alert/AlertContext";
import { useApi } from "../../api/ApiContext";
import Button from "../../form/Button";
import Form from "../../form/Form";
import Input from "../../form/Input";
import GuardComponent from "../../guard-components/GuardComponent";
import TeamImage from "../../image/TeamImage";
import VerticalSpacing from "../../spacing/VerticalSpacing";
import { useUser } from "../../user/UserContext";
import { useTournament } from "../TournamentContext";
import styles from './Matches.module.css';


type Props = {
	match: MatchType & { groupId: number };
	confirm: () => void;
	callback: (homeScore: number, awayScore: number) => void;
};

const createFunction = (fn: (data: number) => void) => {
	return (data: string) => {
		const result = parseInt(data);
		if (isNaN(result)) {
			fn(0);
			return;
		}
		fn(Math.abs(result));
	};
};

export default function MatchResult({
	match,
	confirm,
	callback,
}: Props) {

	const alert = useAlert();
	const { matchService } = useApi();
	const { user } = useUser();
	const { tournament, setTournament } = useTournament();

	const [teamHomeScore, setTeamHomeScore] = useState(
		match.teamHomeScore ?? 0
	);
	const [teamAwayScore, setTeamAwayScore] = useState(
		match.teamAwayScore ?? 0
	);

	const onSubmit = (e: FormEvent) => {
		e.preventDefault();
		const result: MatchResultType = {
			tournamentId: match.tournamentId,
			phase: match.phase,
			matchId: match.matchId,
			homeTeamPoints: teamHomeScore,
			awayTeamPoints: teamAwayScore,
		};
		matchService.insertResults(result).then((data) => {
			setTournament(data);
			confirm();
			alert.success('Wynik został zapisany');
		});
		callback(result.homeTeamPoints, result.awayTeamPoints);
	};

	const disabled = user &&
		match.teamHomeId &&
		match.teamAwayId &&
		!tournament.ladder[0].matches[0].teamHomeId &&
		!tournament.ladder[0].matches[0].teamAwayId &&
		user.roles.some((role) => [ROLES.Employee, ROLES.Admin].includes(role))
		? false : true;

	const teamHome = tournament.teams.find((team) => team.teamId === match.teamHomeId);
	const teamAway = tournament.teams.find((team) => team.teamId === match.teamAwayId);

	return (
		<Form onSubmit={onSubmit}>
			<div className={styles.imageBlock}>
				<TeamImage
					url={teamHome?.teamProfileUrl}
					size={64}
					placeholderSize={36}
				/>
			</div>
			<Input
				id='MatchResult-teamHomeScore'
				label={teamHome?.teamName ?? '-----------'}
				value={`${teamHomeScore}`}
				disabled={disabled}
				onChange={(e) =>
					createFunction(setTeamHomeScore)(e.target.value)
				}
			/>
			<VerticalSpacing size={10} />
			<div className={styles.imageBlock}>
				<TeamImage
					url={teamAway?.teamProfileUrl}
					size={64}
					placeholderSize={36}
				/>
			</div>
			<Input
				id='MatchResult-teamAwayScore'
				label={teamAway?.teamName ?? '-----------'}
				value={`${teamAwayScore}`}
				disabled={disabled}
				onChange={(e) =>
					createFunction(setTeamAwayScore)(e.target.value)
				}
			/>
			<GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
				{!disabled && (
					<>
						<VerticalSpacing size={15} />
						<Button value='Zapisz' />
					</>
				)}
			</GuardComponent>
		</Form>
	);
}
