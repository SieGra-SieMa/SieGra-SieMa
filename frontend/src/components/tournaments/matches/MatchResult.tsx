import { FormEvent, useState } from 'react';
import { ROLES } from '../../../_lib/roles';
import { Match as MatchType, MatchResult as MatchResultType } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Input from '../../form/Input';
import GuardComponent from '../../guard-components/GuardComponent';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import { useUser } from '../../user/UserContext';
import { useTournament } from '../TournamentContext';
import styles from './Matches.module.css';

type MatchResultProps = {
    match: MatchType & { groupId: number };
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

    const disabled = (
        tournament && user &&
        match.teamAway &&
        match.teamHome &&
        (!tournament.ladder[0].matches[0].teamAway) &&
        (!tournament.ladder[0].matches[0].teamHome) &&
        user.roles.some((role) => [ROLES.Emp, ROLES.Admin].includes(role))
    ) ? false : true;

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
