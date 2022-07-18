import { FormEvent, useState } from 'react';
import { ROLES } from '../../../_lib/roles';
import { Match as MatchType, MatchResult as MatchResultType } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import GuardComponent from '../../guard-components/GuardComponent';
import TeamImage from '../../image/TeamImage';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import { useUser } from '../../user/UserContext';
import { useTournament } from '../TournamentContext';
import styles from './Ladder.module.css';


type Props = {
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

export default function LadderMatchResult({ match, confirm }: Props) {

    const { matchService } = useApi();
    const { user } = useUser();
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
            .then((data) => {
                setTournament(data);
                confirm();
            });
    };

    const isEditable = () => {
        if (match.phase === tournament!.ladder.length - 2) {
            const thirdPlacePhase = tournament.ladder[match.phase];
            const thirdPlaceMatch = thirdPlacePhase.matches[0];
            const finalPhase = tournament.ladder[match.phase + 1];
            const finalMatch = finalPhase.matches[0];
            return (
                finalMatch.teamAwayScore === null &&
                finalMatch.teamHomeScore === null &&
                thirdPlaceMatch.teamAwayScore === null &&
                thirdPlaceMatch.teamHomeScore === null
            );
        } else if (match.phase < tournament!.ladder.length - 2) {
            const nextPhase = tournament.ladder[match.phase];
            const nextMatch = nextPhase.matches[Math.ceil(match.matchId / 2) - 1];
            return (
                nextMatch.teamAwayScore === null &&
                nextMatch.teamHomeScore === null
            );
        }
        return true;
    }

    const disabled = (
        user &&
        match.teamAwayId &&
        match.teamHomeId &&
        user.roles.some((role) => [ROLES.Employee, ROLES.Admin].includes(role)) &&
        isEditable()
    ) ? false : true;

    const teamHome = tournament.teams.find((team) => team.teamId === match.teamHomeId);
    const teamAway = tournament.teams.find((team) => team.teamId === match.teamAwayId);

    console.log(teamHome)

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
                id='LadderMatchResult-teamHomeScore'
                label={teamHome?.teamName ?? '-----------'}
                value={`${teamHomeScore}`}
                disabled={disabled}
                onChange={(e) => createFunction(setTeamHomeScore)(e.target.value)}
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
                id='LadderMatchResult-teamAwayScore'
                label={teamAway?.teamName ?? '-----------'}
                value={`${teamAwayScore}`}
                disabled={disabled}
                onChange={(e) => createFunction(setTeamAwayScore)(e.target.value)}
            />
            <GuardComponent roles={[ROLES.Admin, ROLES.Employee]}>
                {!disabled && (<>
                    <VerticalSpacing size={15} />
                    <Button value='Zapisz' disabled={teamAwayScore === teamHomeScore} />
                </>)}
            </GuardComponent>
        </Form>
    );
};
