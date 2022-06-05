import { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { Tournament as TournamentType } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import GuardComponent from '../guard-components/GuardComponent';
import Ladder from '../ladder/Ladder';
import Confirm from '../modal/Confirm';
import Modal from '../modal/Modal';
import EditTournament from './EditTournament';
import { useTournaments } from './TournamentsContext';
import styles from './Tournament.module.css';
import { TournamentContext } from './TournamentContext';

export default function Tournament() {

    const navigate = useNavigate();

    const { id } = useParams<{ id: string }>();

    const { tournamentsService } = useApi();
    const { tournaments, setTournaments } = useTournaments();

    const [tournament, setTournament] = useState<TournamentType | null>(null);
    const [isEdit, setIsEdit] = useState(false);
    const [isDelete, setIsDelete] = useState(false);

    useEffect(() => {
        tournamentsService.getTournamentbyId(id!)
            .then((data) => {
                setTournament(data);
            });
    }, [id, tournamentsService]);

    const editTournament = useCallback((updatedTournament: TournamentType) => {
        setTournament(updatedTournament);
        setIsEdit(false);
        if (tournaments) {
            const filtered = tournaments.filter(
                (e) => e.id !== updatedTournament.id
            );
            setTournaments([...filtered, updatedTournament]);
        }
    }, [tournaments, setTournaments]);

    const deleteTournament = useCallback(() => {
        tournamentsService.deleteTournament(id!)
            .then((data) => { // TODO
                setIsDelete(false);
                navigate('..');
                if (tournaments) {
                    const updatedTournaments = tournaments.filter(
                        (tournament) => tournament.id !== parseInt(id!)
                    );
                    setTournaments(updatedTournaments);
                }
            });
    }, [
        id,
        tournaments,
        setTournaments,
        tournamentsService,
        navigate
    ]);

    return (
        <TournamentContext.Provider value={{ tournament, setTournament }}>
            <div className={styles.top}>
                <Button value='Back' onClick={() => navigate('..')} />
                {tournament && (
                    <GuardComponent roles={[ROLES.Admin]}>
                        <div className={styles.adminControls}>
                            <Button
                                value='Edit'
                                onClick={() => setIsEdit(true)}
                                style={ButtonStyle.DarkBlue}
                            />
                            <Button
                                value='Delete'
                                onClick={() => setIsDelete(true)}
                                style={ButtonStyle.Red}
                            />
                        </div>
                    </GuardComponent>
                )}

            </div>
            <h1 className={styles.title}>
                {tournament && tournament.name}
            </h1>
            <h2>Ladder</h2>
            {tournament && tournament.ladder && <Ladder ladder={tournament.ladder} />}
            <h2>Groups</h2>
            <ul className={styles.groups}>
                {tournament && tournament.groups &&
                    tournament.groups.filter((group) => !group.ladder)
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
            <h2>Matches</h2>
            {tournament && isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title={`Edit tournament - "${tournament.name}"`}
                >
                    <EditTournament
                        tournament={tournament}
                        confirm={editTournament}
                    />
                </Modal>
            )}
            {tournament && isDelete && (
                <Modal
                    close={() => setIsDelete(false)}
                    title={`Tournament "${tournament.name}" - Do you really want to delete?`}
                >
                    <Confirm
                        cancel={() => setIsDelete(false)}
                        confirm={deleteTournament}
                        label='Delete'
                    />
                </Modal>
            )}
        </TournamentContext.Provider>
    );
};