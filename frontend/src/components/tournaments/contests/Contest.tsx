import { Contest as ContestType } from "../../../_lib/_types/tournament";
import styles from "./Contests.module.css";


type Props = {
    contest: ContestType;
};

export default function Contest({ contest }: Props) {

    return (
        <div className={styles.scores}>
            {contest.contestants.map((player) => (
                <div key={player.userId} className={styles.score}>
                    <h6>
                        {player.name} {player.surname}
                    </h6>
                    <p>{player.points}</p>
                </div>
            ))}
        </div>
    );
}
