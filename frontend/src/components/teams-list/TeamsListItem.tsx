import styles from './TeamsListItem.module.css';
import { Team } from '../../_lib/types';
import { teamsService } from '../../_services/teams.service';
import { useCallback, useState } from 'react';
import Modal from '../modal/Modal';
import TeamLeave from './TeamLeave';
import TeamAdd from './TeamAdd';

type TeamsListItemProp = {
    team: Team,
    onRemove: (id: number) => void,
}

export default function TeamsListItem({ team, onRemove }: TeamsListItemProp) {

    const [isAdd, setIsAdd] = useState(false);
    const [isConfirm, setIsConfirm] = useState(false);

    const leaveTeam = useCallback(() => {
        teamsService.leave(team.id)
            .then(() => {
                setIsConfirm(false);
                onRemove(team.id)
            })
    }, [team.id, onRemove]);

    return (
        <div className={styles.root}>
            <div className={styles.content}>
                <h3>{ team.name }</h3>
                <div className={styles.codeBlock}>
                    <span>Code: </span>
                    <h3>{ team.code }</h3>
                </div>
                <ul>
                    {team.players.map((player, index) => (
                        <li
                            key={index}
                        >
                            <p>{`${player.user.name} ${player.user.surname}`}</p>
                        </li>
                    ))}
                </ul>
            </div>
            <div className={styles.footer}>
                <button
                    className={styles.button}
                    onClick={() => setIsAdd(true)}
                >
                    ADD PARTICIPANT
                </button>
                <button
                    className={styles.button}
                    onClick={() => setIsConfirm(true)}
                >
                    LEAVE
                </button>
            </div>
            {isAdd && (
                <Modal
                    close={() => setIsAdd(false)}
                    isClose
                    title={`Team "${team.name}" - Add participant`}
                >
                    <TeamAdd />
                </Modal>
            )}
            {isConfirm && (
                <Modal
                    close={() => setIsConfirm(false)}
                    title={`Team "${team.name}" - Do you really want to leave?`}
                >
                    <TeamLeave
                        cancel={() => setIsConfirm(false)}
                        confirm={() => leaveTeam()}
                    />
                </Modal>
            )}
        </div>
    );
}
