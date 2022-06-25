import Config from '../../config.json';
import styles from './TeamsListItem.module.css';
import { Team } from '../../_lib/types';
import { useCallback, useState } from 'react';
import Modal from '../modal/Modal';
import Confirm from '../modal/Confirm';
import AddParticipant from './controls/AddParticipant';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import { useUser } from '../user/UserContext';
import EditTeam from './controls/EditTeam';
import { useTeams } from './TeamsContext';
import PlayersToRemove from './controls/PlayersToRemove';
import PlayersToSetCapitan from './controls/PlayersToSetCapitan';
import EditTeamPicture from './controls/EditTeamPicture';
import ImageIcon from '@mui/icons-material/Image';

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
    const [isRemove, setIsRemove] = useState(false);
    const [isSetCapitan, setIsSetCapitan] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [isLeave, setIsLeave] = useState(false);
    const [isPicture, setIsPicture] = useState(false);

    const captain = team.players.find((player) => player.id === team.captainId);

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

    return (
        <div className={styles.root}>
            <div className={styles.content}>
                <div className={styles.pictureBlock} style={team.profilePicture ? {
                    backgroundImage: `url(${Config.HOST}${team.profilePicture})`
                } : undefined}>
                    {(!team.profilePicture) && (<ImageIcon className={styles.picture} />)}
                </div>
                <h4 className={styles.title}>{team.name}</h4>
                <p>
                    Kod: <b>{team.code}</b>
                </p>
                <p>
                    Kapitan: <b>{captain ? `${captain.name} ${captain.surname}` : 'Użytkownik'}</b>
                </p>
                <p>
                    Graczy:
                </p>
                <ul className={styles.players}>
                    {team.players.length > 0 ? team.players.filter((player) => player.id !== team.captainId)
                        .map((player, index) => (
                            <li key={index}>
                                <p>{`${player.name} ${player.surname}`}</p>
                            </li>
                        )) : (
                        <div>
                            Null
                        </div>
                    )}
                    <div className={styles.controls}>
                        <Button
                            value='Dodaj gracza'
                            onClick={() => setIsAdd(true)}
                        />
                        {user && team.captainId === user.id && (<>
                            <Button
                                value='Usuń gracza'
                                onClick={() => setIsRemove(true)}
                                style={ButtonStyle.Red}
                            />
                        </>)}
                    </div>

                </ul>
                {user && team.captainId === user.id && (<>
                    <Button
                        value='Edytuj zdjęcie profilowe'
                        onClick={() => setIsPicture(true)}
                        style={ButtonStyle.DarkBlue}
                    />
                    <Button
                        value='Edytuj zespół'
                        onClick={() => setIsEdit(true)}
                        style={ButtonStyle.DarkBlue}
                    />
                    <Button
                        value='Zmień kapitana'
                        onClick={() => setIsSetCapitan(true)}
                        style={ButtonStyle.Red}
                    />
                </>)}
                <Button
                    value='Opuść zespół'
                    onClick={() => setIsLeave(true)}
                    style={ButtonStyle.Red}
                />
            </div>
            {
                isAdd && (
                    <Modal
                        close={() => setIsAdd(false)}
                        isClose
                        title={`Zespół "${team.name}" - Dodaj gracza`}
                    >
                        <AddParticipant />
                    </Modal>
                )
            }
            {
                isEdit && (
                    <Modal
                        isClose
                        close={() => setIsEdit(false)}
                        title={`Edytuj zespół - "${team.name}"`}
                    >
                        <EditTeam
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
                )
            }
            {
                isPicture && (
                    <Modal
                        isClose
                        close={() => setIsPicture(false)}
                        title={`Zespół "${team.name}" - Wybierz zdjęcie profilowe`}
                    >
                        <EditTeamPicture
                            team={team}
                            confirm={(url) => {
                                setIsPicture(false);
                                if (teams) {
                                    const index = teams.findIndex((e) => e.id === team.id);
                                    const data = [...teams];
                                    data[index] = {
                                        ...team,
                                        profilePicture: url,
                                    };
                                    setTeams(data);
                                }
                            }} />
                    </Modal>
                )
            }
            {
                isLeave && (
                    <Modal
                        close={() => setIsLeave(false)}
                        title={`Czy na pewno chcesz opuścić zespół - "${team.name}"?`}
                    >
                        <Confirm
                            cancel={() => setIsLeave(false)}
                            confirm={() => leaveTeam()}
                            label='Opuść'
                            style={ButtonStyle.Red}
                        />
                    </Modal>
                )
            }
            {
                isSetCapitan && (
                    <Modal
                        isClose
                        close={() => setIsSetCapitan(false)}
                        title={`Zespół "${team.name}" -  Wybierz kapitana`}
                    >
                        <PlayersToSetCapitan
                            team={team}
                            confirm={() => setIsSetCapitan(false)}
                        />
                    </Modal>
                )
            }
            {
                isRemove && (
                    <Modal
                        isClose
                        close={() => setIsRemove(false)}
                        title={`Zespół "${team.name}" - Wybierz gracza do usunięcia`}
                    >
                        <PlayersToRemove
                            team={team}
                            confirm={() => setIsRemove(false)}
                        />
                    </Modal>
                )
            }
        </div >
    );
}
