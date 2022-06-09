import { useState } from 'react';
import { Match as MatchType } from '../../../_lib/_types/tournament';
import Modal from '../../modal/Modal';
import styles from './Ladder.module.css';
import MatchResult from './MatchResult';

type MatchProps = {
    match: MatchType;
}

export default function Match({ match }: MatchProps) {

    const [isEdit, setIsEdit] = useState(false);

    return (
        <div className={styles.matchItem}>
            <div className={styles.match} onClick={() => setIsEdit(true)}>
                <div className={styles.team}>
                    <div>
                        {match.teamHome ?? '---------'}
                    </div>
                    <div>
                        {match.teamHomeScore ?? '-'}
                    </div>
                </div>
                <div className={styles.team}>
                    <div>
                        {match.teamAway ?? '---------'}
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
                    title={'Match result'}>
                    <MatchResult
                        match={match}
                        confirm={() => setIsEdit(false)}
                    />
                </Modal>
            )}
        </div>
    );
}