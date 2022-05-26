import { useState } from 'react';
import { Match } from '../../_lib/types';
import Modal from '../modal/Modal';
import styles from './Ladder.module.css';
import MatchResultComponentProps from './MatchResultComponentProps';

type MatchComponentProps = {
    match: Match
}

export default function MatchComponent({ match }: MatchComponentProps) {

    const [isEdit, setIsEdit] = useState(false);

    return (
        <div className={styles.matchItem}>
            <div className={styles.match} onClick={() => setIsEdit(true)}>
                <div className={styles.team}>
                    <div>
                        {match.teamHome.name ?? '---------'}
                    </div>
                    <div>
                        {match.teamHomeScore ?? '-'}
                    </div>
                </div>
                <div className={styles.team}>
                    <div>
                        {match.teamAway.name ?? '---------'}
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
                    <MatchResultComponentProps
                        match={match}
                        confirm={() => setIsEdit(false)}
                    />
                </Modal>
            )}
        </div>
    );
}