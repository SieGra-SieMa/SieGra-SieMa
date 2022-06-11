import { Group } from '../../../_lib/_types/tournament';
import styles from './Groups.module.css';

type GroupsProps = {
    groups: Group[];
}

export default function Groups({ groups }: GroupsProps) {

    return (<>
        <h4>Grupy</h4>
        <ul className={styles.groups}>
            {groups.filter((group) => !group.ladder)
                .map((group) => (
                    <li className={styles.group} key={group.id}>
                        <table>
                            <caption>
                                Grupa - {group.name}
                            </caption>
                            <thead>
                                <tr>
                                    <th>Place</th>
                                    <th>Team</th>
                                    <th>Matches</th>
                                    <th>Won</th>
                                    <th>Lost</th>
                                    <th>Tied</th>
                                    <th>Scored</th>
                                    <th>Conceded</th>
                                    <th>Points</th>
                                </tr>
                            </thead>
                            <tbody>
                                {group.teams && group.teams.sort((a, b) => b.goalScored - a.goalScored)
                                    .sort((a, b) => b.points - a.points).map((team, index) => (
                                        <tr key={index}>
                                            <td>{index}</td>
                                            <td>{team.name}</td>
                                            <td>{team.playedMatches}</td>
                                            <td>{team.wonMatches}</td>
                                            <td>{team.lostMatches}</td>
                                            <td>{team.tiedMatches}</td>
                                            <td>{team.goalScored}</td>
                                            <td>{team.goalConceded}</td>
                                            <td>{team.points}</td>
                                        </tr>
                                    ))}
                            </tbody>
                        </table>
                    </li>
                ))}
        </ul>
    </>);
};