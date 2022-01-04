import CreateTeam from './CreateTeam';
import JoinTeam from './JoinTeam';
import styles from './TeamOptions.module.css';

export default function TeamOptions() {
    return (
        <div className={`container ${styles.root}`}>
            <CreateTeam />
            <JoinTeam />
        </div>
    );
}
