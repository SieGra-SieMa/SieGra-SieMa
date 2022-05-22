import styles from './TeamsListItem.module.css';
import { Team } from '../../_lib/types';
import { useCallback, useState } from 'react';
import Modal from '../modal/Modal';
import Confirm from '../modal/Confirm';
import TeamAdd from './TeamAdd';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';

type TeamsListItemProp = {
    team: Team,
    onRemove: (id: number) => void,
}

export default function TeamsListItem({ team, onRemove }: TeamsListItemProp) {

    const { teamsService } = useApi();

    const [isAdd, setIsAdd] = useState(false);
    const [isConfirm, setIsConfirm] = useState(false);

    const captain = team.players.find((player) => player.id === team.captainId);

    const leaveTeam = useCallback(() => {
        teamsService.leaveTeam(team.id)
            .then(() => {
                setIsConfirm(false);
                onRemove(team.id)
            })
    }, [team.id, onRemove, teamsService]);

    return (
        <div className={styles.root}>
            <div className={styles.content}>
                <h3>{team.name}</h3>
                <div className={styles.codeBlock}>
                    <span>Code: </span>
                    <h3>{team.code}</h3>
                </div>
                <div className={styles.codeBlock}>
                    <span>Captain: </span>
                    <h3>{captain ? `${captain.name} ${captain.surname}` : 'Username'}</h3>
                </div>
                <ul>
                    {team.players.filter((player) => player.id !== team.captainId).map((player, index) => (
                        <li
                            key={index}
                        >
                            <p>{`${player.name} ${player.surname}`}</p>
                        </li>
                    ))}
                </ul>
            </div>
            <div className={styles.footer}>
                <Button
                    value='Add participants'
                    onClick={() => setIsAdd(true)}
                    style={ButtonStyle.Orange}
                />
                <Button
                    value='Leave'
                    onClick={() => setIsConfirm(true)}
                    style={ButtonStyle.Red}
                />
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
                    <Confirm
                        cancel={() => setIsConfirm(false)}
                        confirm={() => leaveTeam()}
                        label='Leave'
                    />
                </Modal>
            )}
        </div>
    );
}
