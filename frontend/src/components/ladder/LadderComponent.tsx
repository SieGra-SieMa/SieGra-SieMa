import { Ladder } from '../../_lib/types';
import styles from './Ladder.module.css';
import PhaseComponent from './PhaseComponent';

type LadderComponentProps = {
    ladder: Ladder
}

export default function LadderComponent({ ladder }: LadderComponentProps) {
    return (
        <div className={styles.ladder}>
            {ladder.phases.map((phase, index) => (
                <PhaseComponent key={index} phase={phase} />
            ))}
        </div>
    );
}