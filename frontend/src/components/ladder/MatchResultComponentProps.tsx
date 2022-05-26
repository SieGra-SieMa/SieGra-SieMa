import { FormEvent, useState } from 'react';
import { Match, MatchResult } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './Ladder.module.css';

type MatchResultComponentProps = {
    match: Match;
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

export default function MatchResultComponent({ match, confirm }: MatchResultComponentProps) {

    const { matchService } = useApi();

    const [teamHomeScore, setTeamHomeScore] = useState(match.teamHomeScore ?? 0);
    const [teamAwayScore, setTeamAwayScore] = useState(match.teamAwayScore ?? 0);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const result: MatchResult = {
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
            <VerticalSpacing size={15} />
            {!disabled && <Button value='Save' />}
        </form>
    );
}