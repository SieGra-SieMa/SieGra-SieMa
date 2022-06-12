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
    team: Team;
}

export default function TeamsListItem({
    team,
}: TeamsListItemProp) {

    const { teamsService } = useApi();
    const { user } = useUser();

    const { teams, setTeams } = useTeams();

    const [isAdd, setIsAdd] = useState(false);
    const [isSwitch, setIsSwitch] = useState(false);
    const [isRemoveConfirm, setIsRemoveConfirm] = useState(false);
    const [chosenPlayer, setChosenPlayer] = useState<number>();

    const [isEdit, setIsEdit] = useState(false);
    const [isLeave, setIsLeave] = useState(false);

    const captain = team.players!.find((player) => player.id === team.captainId);

    const leaveTeam = useCallback(() => {
        teamsService.leaveTeam(team.id)
            .then(() => {
                const data = teams ? [...teams] : [];
                const index = data.findIndex(e => e.id === team.id) ?? -1;
                if (index >= 0) {
                    data.splice(index, 1);
                    setTeams(data);
                }
                setIsLeave(false);
            })
    }, [team.id, teams, setTeams, teamsService]);

    const switchCaptain = useCallback((id: number) => {
        teamsService.switchCaptain(team.id, id)
            .then((team) => {
                const data = teams ? [...teams] : [];
                const index = data.findIndex(e => e.id === team.id) ?? -1;
                if (index >= 0) {
                    data[index] = team;
                    setTeams(data);
                }
                setIsSwitch(false);
            });
    }, [team.id, teams, setTeams, teamsService]);

    const removePlayer = useCallback((id: number) => {
        teamsService.removePlayer(team.id, id)
            .then((team) => {
                const data = teams ? [...teams] : [];
                const index = data.findIndex(e => e.id === team.id) ?? -1;
                if (index >= 0) {
                    data[index] = team;
                    setTeams(data);
                }
                setIsRemoveConfirm(false);
            });
    }, [team.id, teams, setTeams, teamsService]);

    return (
        <div className={styles.root}>
            <div className={styles.content}>
                <h3>{team.name}</h3>
                <div className={styles.codeBlock}>
                    <span>Code: </span>
                    <h6>{team.code}</h6>
                </div>
                <div className={styles.codeBlock}>
                    <span>Captain: </span>
                    <h6>{captain ? `${captain.name} ${captain.surname}` : 'Username'}</h6>
                </div>
                <ul>
                    {team.players!.filter((player) => player.id !== team.captainId).map((player, index) => (
                        <li key={index}>
                            <div className={styles.teamMember}>
                                <p>{`${player.name} ${player.surname}`}</p>
                                {(user?.id === team.captainId) && (<>
                                    <Button
                                        value='Zmień kapitana'
                                        onClick={() => {
                                            setChosenPlayer(player.id);
                                            setIsSwitch(true);
                                        }}
                                    />
                                    <Button
                                        value='Usuń gracza'
                                        onClick={() => {
                                            setChosenPlayer(player.id);
                                            setIsRemoveConfirm(true);
                                        }}
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
                    value='Dodaj gracza'
                    onClick={() => setIsAdd(true)}
                />
                {user && team.captainId === user.id && (
                    <Button
                        value='Edytuj zespół'
                        onClick={() => setIsEdit(true)}
                        style={ButtonStyle.DarkBlue}
                    />
                )}
                <Button
                    value='Opuść zespół'
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
                    title={'Edytuj zespół'}
                >
                    <TeamEdit
                        team={team}
                        confirm={(team) => {
                            setIsEdit(false);
                            if (teams) {
                                const index = teams.findIndex((e) => e.id === team.id);
                                const data = [...teams];
                                data[index] = {
                                    ...team,
                                    players: teams[index].players,
                                };
                                setTeams(data);
                            }
                        }}
                    />
                </Modal>
            )}
            {isLeave && (
                <Modal
                    close={() => setIsLeave(false)}
                    title={`Czy na pewno chcesz opuścić zespół? - "${team.name}"`}
                >
                    <Confirm
                        cancel={() => setIsLeave(false)}
                        confirm={() => leaveTeam()}
                        label='Opuść'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
            {isSwitch && (
                <Modal
                    close={() => setIsSwitch(false)}
                    title={`Zespół "${team.name}" - Czy na pewno chcesz zmienić kapitana?`}
                >
                    <Confirm
                        cancel={() => setIsSwitch(false)}
                        confirm={() => switchCaptain(chosenPlayer!)}
                        label='Zmień'
                        style={ButtonStyle.Yellow}
                    />
                </Modal>
            )}
            {isRemoveConfirm && (
                <Modal
                    close={() => setIsRemoveConfirm(false)}
                    title={`Zespół "${team.name}" - Czy na pewno chcesz usunąć gracza?`}
                >
                    <Confirm
                        cancel={() => setIsRemoveConfirm(false)}
                        confirm={() => removePlayer(chosenPlayer!)}
                        label='Usuń'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
        </div>
    );
}
