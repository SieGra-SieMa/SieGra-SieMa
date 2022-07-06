import { Phase as PhaseType } from '../../../_lib/_types/tournament';
import styles from './Ladder.module.css';
import Match from './Match';


type Props = {
    phase: PhaseType;
}

export default function Phase({ phase }: Props) {
    return (
        <div className={styles.phase}>
            <div className={styles.phaseTitle}>{phase.name}</div>
            <div className={styles.matches}>
                {phase.matches.map((match, index) => (
                    <Match key={index} match={match} />
                ))}
            </div>
        </div>
    );
}