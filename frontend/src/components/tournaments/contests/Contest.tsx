import { useState } from 'react';
import { ROLES } from '../../../_lib/roles';
import { Contest as ContestType } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button, { ButtonStyle } from '../../form/Button';
import GuardComponent from '../../guard-components/GuardComponent';
import Confirm from '../../modal/Confirm';
import Modal from '../../modal/Modal';
import AddScoreContest from './AddScoreContest';
import EditContest from './EditContest';
import styles from './Contests.module.css';

type ContestProps = {
    contest: ContestType;
};

export default function Contest({ contest }: ContestProps) {

    const { tournamentsService } = useApi();

    const [isAddScore, setIsAddScore] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [isDelete, setIsDelete] = useState(false);

    const onDelete = () => {
        tournamentsService.deleteContest(contest.tournamentId, contest.id)
            .then(() => {
                setIsDelete(false);
            });
    };

    return (
        <li className={styles.contest}>
            <div>
                <h4 className={styles.title}>{contest.name}</h4>
                <ul>
                    {contest.contestants.map((player) => (
                        <li
                            key={player.userId}
                            className={styles.contestant}
                        >
                            <p>
                                {player.name} {player.surname}
                            </p>
                            <p>
                                {player.points}
                            </p>
                        </li>
                    ))}
                </ul>
            </div>
            <GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
                <div className={styles.controls}>
                    <Button
                        value='Dodaj wynik'
                        onClick={() => setIsAddScore(true)}
                    />
                    <GuardComponent roles={[ROLES.Admin]}>
                        <Button
                            value='Edytuj konkurs'
                            onClick={() => setIsEdit(true)}
                            style={ButtonStyle.DarkBlue}
                        />
                        <Button
                            value='Usuń konkurs'
                            onClick={() => setIsDelete(true)}
                            style={ButtonStyle.Red}
                        />
                    </GuardComponent>
                </div>
                {(isAddScore) && (
                    <Modal
                        title={`Konkurs - "${contest.name}"`}
                        isClose
                        close={() => setIsAddScore(false)}
                    >
                        <AddScoreContest
                            contest={contest}
                            confirm={() => {
                                setIsAddScore(false);
                            }}
                        />
                    </Modal>
                )}
                <GuardComponent roles={[ROLES.Admin]}>
                    {(isEdit) && (
                        <Modal
                            title={`Edytuj konkurs - "${contest.name}"`}
                            isClose
                            close={() => setIsEdit(false)}
                        >
                            <EditContest
                                contest={contest}
                                confirm={() => {
                                    setIsEdit(false);
                                }}
                            />
                        </Modal>
                    )}
                    {(isDelete) && (
                        <Modal
                            title={`Czy na pewno chcesz usunąć konkurs - "${contest.name}"?`}
                            isClose
                            close={() => setIsDelete(false)}
                        >
                            <Confirm
                                label='Usuń'
                                cancel={() => setIsDelete(false)}
                                confirm={onDelete}
                                style={ButtonStyle.Red}
                            />
                        </Modal>
                    )}
                </GuardComponent>
            </GuardComponent>
        </li>
    );
};
