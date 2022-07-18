import { useState } from 'react';
import { Match as MatchType } from '../../../_lib/_types/tournament';
import TeamImage from '../../image/TeamImage';
import Modal from '../../modal/Modal';
import { useTournament } from '../TournamentContext';
import styles from './Ladder.module.css';
import LadderMatchResult from './LadderMatchResult';


type Props = {
    match: MatchType;
}

export default function Match({ match }: Props) {

    const { tournament } = useTournament();

    const [isEdit, setIsEdit] = useState(false);

    const teamHome = tournament!.teams.find((team) => team.teamId === match.teamHomeId);
    const teamAway = tournament!.teams.find((team) => team.teamId === match.teamAwayId);

    return (
        <div className={styles.matchItem}>
            <div className={styles.match} onClick={() => setIsEdit(true)}>
                <div className={styles.team}>
                    <TeamImage
                        url={teamHome?.teamProfileUrl}
                        size={36}
                        placeholderSize={17}
                    />
                    <div>
                        {teamHome?.teamName ?? '---------'}
                    </div>
                    <div>
                        {match.teamHomeScore ?? '-'}
                    </div>
                </div>
                <div className={styles.team}>
                    <TeamImage
                        url={teamAway?.teamProfileUrl}
                        size={36}
                        placeholderSize={17}
                    />
                    <div>
                        {teamAway?.teamName ?? '---------'}
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
                    <LadderMatchResult
                        match={match}
                        confirm={() => setIsEdit(false)}
                    />
                </Modal>
            )}
        </div>
    );
}