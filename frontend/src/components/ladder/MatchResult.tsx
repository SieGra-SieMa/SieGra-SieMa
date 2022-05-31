import { FormEvent, useState } from 'react';
import { ROLES } from '../../_lib/roles';
import { Match as MatchType, MatchResult as MatchResultType } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import GuardComponent from '../guard-components/GuardComponent';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './Ladder.module.css';

type MatchResultProps = {
    match: MatchType;
    confirm: () => void;
}

const createFunction = (fn: (data: number) => void) => {
    return (data: string) => {
        const result = parseInt(data);
        if (isNaN(result)) {
            fn(0);
            return;
        }
        fn(Math.abs(result));
    }
}

export default function MatchResult({ match, confirm }: MatchResultProps) {

    const { matchService } = useApi();

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
                confirm();
            })
    }

    const disabled = (match.teamAway.name && match.teamHome.name) ? false : true;

    return (
        <form className={styles.matchDetails} onSubmit={onSubmit}>
            <Input
                id='MatchResult-teamHomeScore'
                label={`${match.teamHome.name ?? '-----------'} - Team score`}
                value={`${teamHomeScore}`}
                disabled={disabled}
                onChange={(e) => createFunction(setTeamHomeScore)(e.target.value)}
            />
            <Input
                id='MatchResult-teamAwayScore'
                label={`${match.teamAway.name ?? '-----------'} - Team score`}
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
}