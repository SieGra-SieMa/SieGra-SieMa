import { Phase } from '../../_lib/types';
import styles from './Ladder.module.css';
import MatchComponent from './MatchComponent';

type PhaseComponentProps = {
    phase: Phase
}

export default function PhaseComponent({ phase }: PhaseComponentProps) {
    return (
        <div className={styles.phase}>
            <div className={styles.phaseTitle}>{phase.phase}</div>
            <div className={styles.matches}>
                {phase.matches.map((match, index) => (
                    <MatchComponent key={index} match={match} />
                ))}
            </div>
        </div>
    );
}