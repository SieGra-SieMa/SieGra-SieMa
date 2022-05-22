import styles from './TeamsListItem.module.css';
import { Team } from '../../_lib/types';
import { useCallback, useState } from 'react';
import Modal from '../modal/Modal';
import Confirm from '../modal/Confirm';
import TeamAdd from './TeamAdd';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import { useUser } from '../user/UserContext';
import TeamEdit from './TeamEdit';
import { useTeams } from './TeamsContext';

type TeamsListItemProp = {
    team: Team,
}

export default function TeamsListItem({ team }: TeamsListItemProp) {

    const { teamsService } = useApi();
    const { user } = useUser();
    const { teams, setTeams } = useTeams();

    const [isAdd, setIsAdd] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [isLeave, setIsLeave] = useState(false);

    const captain = team.players.find((player) => player.id === team.captainId);

    const leaveTeam = useCallback(() => {
        teamsService.leaveTeam(team.id)
            .then(() => {
                setIsLeave(false);
                const data = teams ? [...teams] : [];
                const index = data.findIndex(e => e.id === team.id) ?? -1;
                if (index >= 0) {
                    data.splice(index, 1);
                    setTeams(data);
                }
            })
    }, [team.id, teams, setTeams, teamsService]);

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
                {user && team.captainId === user.id && (
                    <Button
                        value='Edit'
                        onClick={() => setIsEdit(true)}
                        style={ButtonStyle.DarkBlue}
                    />
                )}
                <Button
                    value='Leave'
                    onClick={() => setIsLeave(true)}
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
            {isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title={'Team edit'}
                >
                    <TeamEdit
                        team={team}
                        confirm={() => setIsEdit(false)}
                    />
                </Modal>
            )}
            {isLeave && (
                <Modal
                    close={() => setIsLeave(false)}
                    title={`Team "${team.name}" - Do you really want to leave?`}
                >
                    <Confirm
                        cancel={() => setIsLeave(false)}
                        confirm={() => leaveTeam()}
                        label='Leave'
                    />
                </Modal>
            )}
        </div>
    );
}
