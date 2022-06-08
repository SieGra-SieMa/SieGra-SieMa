import styles from './TeamsListItem.module.css';
import { Team } from '../../_lib/types';
import { useCallback, useState } from 'react';
import Modal from '../modal/Modal';
import Confirm from '../modal/Confirm';
import TeamAdd from './TeamAdd';
import TeamChange from './TeamDataEdit';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import { useUser } from '../user/UserContext';

type TeamsListItemProp = {
    team: Team,
    onRemove: (id: number) => void,
    onCaptainSwitch: (team: Team) => void,
    onPlayerRemovedSwitch: (team: Team) => void
}

export default function TeamsListItem({ team, onRemove, onCaptainSwitch, onPlayerRemovedSwitch }: TeamsListItemProp) {

    const { teamsService } = useApi();
    const { user } = useUser();

    const [isAdd, setIsAdd] = useState(false);
    const [isConfirm, setIsConfirm] = useState(false);
    const [isSwitch, setIsSwitch] = useState(false);
    const [isRemoveConfirm, setIsRemoveConfirm] = useState(false);
    const [isTeamEdit, setIsTeamEdit] = useState(false);
    const [chosenPlayer, setChosenPlayer] = useState<number>();

    const captain = team.players!.find((player) => player.id === team.captainId);

    const leaveTeam = useCallback(() => {
        teamsService.leaveTeam(team.id!)
            .then(() => {
                setIsConfirm(false);
                onRemove(team.id!)
            })
    }, [team.id, onRemove, teamsService]);

    const switchCaptain = useCallback((id: number) => {
        teamsService.switchCaptain(team.id!, id)
            .then((data) => {
                setIsSwitch(false);
                onCaptainSwitch(data)
            });
    }, [team.id, onCaptainSwitch, teamsService]);

    const removePlayer = useCallback((id: number) => {
        teamsService.removePlayer(team.id!, id)
            .then((data) => {
                setIsRemoveConfirm(false);
                onPlayerRemovedSwitch(data)
            });
    }, [team.id, onPlayerRemovedSwitch, teamsService]);

    return (
        <div className={styles.root}>
            <div className={styles.content}>
                <h3>{team.name}
                {(user?.id === team.captainId) && (<>
                    <Button
                        value='Edit'
                        onClick={() => { setIsTeamEdit(true); }}
                        style={ButtonStyle.Orange}
                    />
                </>)}
                </h3>
                <div className={styles.codeBlock}>
                    <span>Code: </span>
                    <h3>{team.code}</h3>
                </div>
                <div className={styles.codeBlock}>
                    <span>Captain: </span>
                    <h3>{captain ? `${captain.name} ${captain.surname}` : 'Username'}</h3>
                </div>
                <ul>
                    {team.players!.filter((player) => player.id !== team.captainId).map((player, index) => (
                        <li
                            key={index}
                        >
                            <div className={styles.teamMember}>
                                <p>{`${player.name} ${player.surname}`}</p>
                                {(user?.id === team.captainId) && (<>
                                    <Button
                                        value='Switch Captain'
                                        onClick={() => { setChosenPlayer(player.id); setIsSwitch(true); }}
                                        style={ButtonStyle.Orange}
                                    />
                                    <Button
                                        value='Remove'
                                        onClick={() => { setChosenPlayer(player.id); setIsRemoveConfirm(true); }}
                                        style={ButtonStyle.Red}
                                    />
                                </>)}
                            </div>

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
            {isSwitch && (
                <Modal
                    close={() => setIsSwitch(false)}
                    title={`Team "${team.name}" - Do you really want to switch captain?`}
                >
                    <Confirm
                        cancel={() => setIsSwitch(false)}
                        confirm={() => switchCaptain(chosenPlayer!)}
                        label='Switch'
                    />
                </Modal>
            )}
            {isRemoveConfirm && (
                <Modal
                    close={() => setIsRemoveConfirm(false)}
                    title={`Team "${team.name}" - Do you really want to remove player`}
                >
                    <Confirm
                        cancel={() => setIsRemoveConfirm(false)}
                        confirm={() => removePlayer(chosenPlayer!)}
                        label='Remove'
                    />
                </Modal>
            )}
            {isTeamEdit && (
                <Modal
                close={() => setIsTeamEdit(false)}
                isClose
                title={`Team "${team.name}" - Change name`}
            >
                <TeamChange parameter={team.id!} confirm={(team) => {
                        // setUser(user);
                        // setIsEdit(false);
                        onCaptainSwitch(team);
                        setIsTeamEdit(false);
                    }}/>
            </Modal>
            )}
        </div>
    );
}
