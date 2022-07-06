import { useState } from 'react';
import { Match as MatchType } from '../../../_lib/_types/tournament';
import Modal from '../../modal/Modal';
import { useTournament } from '../TournamentContext';
import styles from './Ladder.module.css';
import MatchResult from './MatchResult';


type Props = {
    match: MatchType;
}

export default function Match({ match }: Props) {

    const { tournament } = useTournament();

    const [isEdit, setIsEdit] = useState(false);

    const teamHome = tournament!.teams.find((team) => team.teamId === match.teamHomeId)?.teamName;
    const teamAway = tournament!.teams.find((team) => team.teamId === match.teamAwayId)?.teamName;

    return (
        <div className={styles.matchItem}>
            <div className={styles.match} onClick={() => setIsEdit(true)}>
                <div className={styles.team}>
                    <div>
                        {teamHome ?? '---------'}
                    </div>
                    <div>
                        {match.teamHomeScore ?? '-'}
                    </div>
                </div>
                <div className={styles.team}>
                    <div>
                        {teamAway ?? '---------'}
                    </div>
                    <div>
                        {match.teamAwayScore ?? '-'}
                    </div>
                </div>
            </div>
            {isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title='Wynik meczu'>
                    <MatchResult
                        match={match}
                        confirm={() => setIsEdit(false)}
                    />
                </Modal>
            )}
        </div>
    );
}