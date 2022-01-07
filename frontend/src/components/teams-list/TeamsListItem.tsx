import styles from './TeamsListItem.module.css';
import { Team } from '../../_lib/types';
import { teamsService } from '../../_services/teams.service';
import { useCallback } from 'react';

type TeamsListItemProp = {
    team: Team,
    onRemove: (id: number) => void,
}

export default function TeamsListItem({ team, onRemove }: TeamsListItemProp) {

    const leaveTeam = useCallback(() => {
        teamsService.leave(team.id)
            .then(() => onRemove(team.id))
    }, [team.id, onRemove]);

    return (
        <div className={styles.root}>
            <div className={styles.header}>
                <h3>{ team.name }</h3>
            </div>
            {/* <div className={styles.content}>
                <ul className={styles.participantsList}>
                    {team.players.map((player, index) => (
                        <li
                            key={index}
                            className={styles.participantsListItem}
                        >
                            <p>{`${player.user.name} ${player.user.surname}`}</p>
                        </li>
                    ))}
                </ul>
            </div> */}
            <div className={styles.footer}>
                <div className={styles.codeBlock}>
                    <span>Code: </span>
                    <h3>{ team.code }</h3>
                </div>
                <button
                    className={styles.leaveButton}
                    onClick={leaveTeam}
                >
                    LEAVE
                </button>
            </div>
        </div>
    );
}
