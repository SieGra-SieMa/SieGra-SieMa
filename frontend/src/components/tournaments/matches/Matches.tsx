
import { Group } from '../../../_lib/_types/tournament';
import Match from './Match';
import styles from './Matches.module.css';

type MatchesProps = {
    groups: Group[]
};

export default function Matches({
    groups
}: MatchesProps) {

    return (<>
        <h4>Mecze</h4>
        <div className={styles.groups}>
            {groups.filter((group) => group.matches).map((group) => (
                <div className={styles.group} key={group.id}>
                    <h6>Grupa - {group.name}</h6>
                    <div className={styles.matches} key={group.id}>
                        {group.matches && group.matches.map((match) => (
                            <Match key={match.matchId} match={match} />
                        ))}
                    </div>
                </div>
            ))}
        </div>
    </>);
};