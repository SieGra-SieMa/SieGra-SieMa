import Config from '../../../config.json';
import styles from './TeamsListItem.module.css';
import { Team } from '../../../_lib/types';
import Button, { ButtonStyle } from '../../form/Button';
import { useState } from 'react';
import Modal from '../../modal/Modal';
import EditTeamPicture from '../../teams/controls/EditTeamPicture';
import ImageIcon from '@mui/icons-material/Image';
import EditTeam from '../../teams/controls/EditTeam';
import Confirm from '../../modal/Confirm';
import { useApi } from '../../api/ApiContext';

type TeamsListItemProps = {
    team: Team;
    onTeamChange: (team: Team) => void;
    onTeamDelete: (team: Team) => void;
};

export default function TeamsListItem({ team, onTeamChange, onTeamDelete }: TeamsListItemProps) {

    const { teamsService } = useApi();

    const [isPicture, setIsPicture] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [isDelete, setIsDelete] = useState(false);

    const capitan = team.players.find((player) => player.id === team.captainId);

    const onDelete = () => {
        teamsService.admindDeleteTeam(team.id)
            .then((data) => {
                onTeamDelete(team);
                setIsDelete(false);
            });
    }

    return (
        <div className={styles.root}>
            <div className={styles.pictureBlock} style={team.profilePicture ? {
                backgroundImage: `url(${Config.HOST}${team.profilePicture})`
            } : undefined}>
                {(!team.profilePicture) && (<ImageIcon className={styles.picture} />)}
            </div>
            <h6 className={styles.title}>
                {team.name}
            </h6>
            <p>
                Kod: {team.code}
            </p>
            {(capitan) && (
                <p>
                    Kapitan: {capitan.name}  {capitan.surname}
                </p>
            )}
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
                value='Usuń zespół'
                onClick={() => setIsDelete(true)}
                style={ButtonStyle.Red}
            />
            {(isPicture) && (
                <Modal
                    isClose
                    title='Edytuj zdjęcie profilowe'
                    close={() => setIsPicture(false)}
                >
                    <EditTeamPicture
                        team={team}
                        confirm={(url) => {
                            onTeamChange({
                                ...team,
                                profilePicture: url
                            });
                            setIsPicture(false);
                        }}
                    />
                </Modal>
            )}
            {(isEdit) && (
                <Modal
                    isClose
                    title='Edytuj zespół'
                    close={() => setIsEdit(false)}
                >
                    <EditTeam
                        team={team}
                        confirm={(team) => {
                            onTeamChange(team);
                            setIsEdit(false);
                        }}
                    />
                </Modal>
            )}
            {(isDelete) && (
                <Modal
                    isClose
                    title={`Czy na pewno chcesz usunąć zespół - "${team.name}"?`}
                    close={() => setIsDelete(false)}
                >
                    <Confirm
                        cancel={() => setIsDelete(false)}
                        confirm={onDelete}
                        label='Usuń'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
        </div>
    );
};
