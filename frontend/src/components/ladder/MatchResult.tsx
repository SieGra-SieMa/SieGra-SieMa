import { FormEvent, useState } from 'react';
import { ROLES } from '../../_lib/roles';
import { Match as MatchType, MatchResult as MatchResultType, Tournament } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import GuardComponent from '../guard-components/GuardComponent';
import VerticalSpacing from '../spacing/VerticalSpacing';
import { useTournament } from '../tournaments/TournamentContext';
import styles from './Ladder.module.css';

type MatchResultProps = {
    match: MatchType;
    confirm: () => void;
};

const createFunction = (fn: (data: number) => void) => {
    return (data: string) => {
        const result = parseInt(data);
        if (isNaN(result)) {
            fn(0);
            return;
        }
        fn(Math.abs(result));
    }
};

export default function MatchResult({ match, confirm }: MatchResultProps) {

    const { matchService } = useApi();
    const { tournament, setTournament } = useTournament();

    const [teamHomeScore, setTeamHomeScore] = useState(match.teamHomeScore ?? 0);
    const [teamAwayScore, setTeamAwayScore] = useState(match.teamAwayScore ?? 0);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const result: MatchResultType = {
            tournamentId: match.tournamentId,
            phase: match.phase,
            matchId: match.matchId,
            homeTeamPoints: teamHomeScore,
            awayTeamPoints: teamAwayScore
        }
        matchService.insertResults(result)
            .then(() => {
                const phase = tournament!.ladder.phases[result.phase - 1];
                const updatedMatch = phase.matches.find(
                    (match) => match.matchId === result.matchId
                )!;
                updatedMatch.teamAwayScore = result.awayTeamPoints;
                updatedMatch.teamHomeScore = result.homeTeamPoints;
                const updatedTournament: Tournament = {
                    ...tournament!
                };

                const teamWin = result.awayTeamPoints > result.homeTeamPoints ? match.teamAway : match.teamHome;
                const teamLose = result.awayTeamPoints < result.homeTeamPoints ? match.teamAway : match.teamHome;

                if (result.phase === tournament!.ladder.phases.length - 2) {
                    const thirdPlacePhase = tournament!.ladder.phases[result.phase];
                    const thirdPlaceMatch = thirdPlacePhase.matches[0];
                    const finalPhase = tournament!.ladder.phases[result.phase + 1];
                    const finalMatch = finalPhase.matches[0];
                    if (result.matchId % 2 === 0) {
                        thirdPlaceMatch.teamAway = teamLose;
                        finalMatch.teamAway = teamWin;
                    } else {
                        thirdPlaceMatch.teamHome = teamLose;
                        finalMatch.teamHome = teamWin;
                    }
                } else {
                    const nextPhase = tournament!.ladder.phases[result.phase];
                    const nextMatch = nextPhase.matches[Math.ceil(result.matchId / 2) - 1];
                    if (result.matchId % 2 === 0) {
                        nextMatch.teamAway = teamWin;
                    } else {
                        nextMatch.teamHome = teamWin;
                    }
                }
                setTournament(updatedTournament);
                confirm();
            });
    };

    const disabled = (match.teamAway && match.teamHome) ? false : true;

    return (
        <form className={styles.matchDetails} onSubmit={onSubmit}>
            <Input
                id='MatchResult-teamHomeScore'
                label={`${match.teamHome ?? '-----------'} - Team score`}
                value={`${teamHomeScore}`}
                disabled={disabled}
                onChange={(e) => createFunction(setTeamHomeScore)(e.target.value)}
            />
            <Input
                id='MatchResult-teamAwayScore'
                label={`${match.teamAway ?? '-----------'} - Team score`}
                value={`${teamAwayScore}`}
                disabled={disabled}
                onChange={(e) => createFunction(setTeamAwayScore)(e.target.value)}
            />
            <GuardComponent roles={[ROLES.Admin, ROLES.Emp]}>
                {!disabled && (<>
                    <VerticalSpacing size={15} />
                    <Button value='Save' />
                </>)}
            </GuardComponent>
        </form>
    );
};
