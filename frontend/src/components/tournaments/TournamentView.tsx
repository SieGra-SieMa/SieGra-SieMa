import { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import GuardComponent from '../guard-components/GuardComponent';
import LadderComponent from '../ladder/LadderComponent';
import Confirm from '../modal/Confirm';
import Modal from '../modal/Modal';
import TournamentEdit from './TournamentEdit';
import { useTournaments } from './TournamentsContext';
import styles from './TournamentView.module.css';


export default function TournamentView() {

    const navigate = useNavigate();

    const { id } = useParams<{ id: string }>();

    const { tournamentsService } = useApi();
    const { tournaments, setTournaments } = useTournaments();

    const [tournament, setTournament] = useState<Tournament | null>(null);

    const [isEdit, setIsEdit] = useState(false);
    const [isDelete, setIsDelete] = useState(false);

    useEffect(() => {
        tournamentsService.getTournamentbyId(id!)
            .then((data) => {
                setTournament(data);
            });
    }, [id, tournamentsService]);

    const deleteTournament = useCallback(() => {
        tournamentsService.deleteTournament(id!)
            .then((data) => {
                setIsDelete(false);
                navigate('..');
                if (tournaments) {
                    setTournaments(tournaments.filter((tournament) => tournament.id !== parseInt(id!)));
                }
            });
    }, [id, tournamentsService, navigate, setTournaments, tournaments]);

    return (
        <>
            <div className={styles.top}>
                <Button value='Back' onClick={() => navigate('..')} />
                <h1 className={styles.title}>
                    {tournament && tournament.name}
                </h1>
                <GuardComponent roles={[ROLES.Admin]}>
                    <Button value='Edit' onClick={() => setIsEdit(true)} style={ButtonStyle.DarkBlue} />
                    <Button value='Delete' onClick={() => setIsDelete(true)} style={ButtonStyle.Red} />
                </GuardComponent>
            </div>
            {tournament && tournament.ladder && <LadderComponent ladder={tournament.ladder} />}
            <ul className={styles.groups}>
                {tournament && tournament.groups && tournament.groups.filter((group) => !group.ladder).map((group) => (
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
            {
                tournament && isEdit && (
                    <Modal
                        isClose
                        close={() => setIsEdit(false)}
                        title={`Edit tournament - "${tournament.name}"`}
                    >
                        <TournamentEdit
                            tournament={tournament}
                            confirm={(updatedTournament) => {
                                setTournament({ ...tournament, ...updatedTournament });
                                if (tournaments) {
                                    const filtered = tournaments.filter((e) => e.id !== updatedTournament.id);
                                    const edited = tournaments.find((e) => e.id === updatedTournament.id)!;
                                    edited.name = updatedTournament.name;
                                    edited.description = updatedTournament.description;
                                    edited.address = updatedTournament.address;
                                    edited.startDate = updatedTournament.startDate;
                                    edited.endDate = updatedTournament.endDate;
                                    const newData = [...filtered, edited];
                                    setTournaments(newData);
                                }
                                setIsEdit(false);
                            }}
                        />
                    </Modal>
                )
            }
            {
                tournament && isDelete && (
                    <Modal
                        close={() => setIsDelete(false)}
                        title={`Tournament "${tournament.name}" - Do you really want to delete?`}
                    >
                        <Confirm
                            cancel={() => setIsDelete(false)}
                            confirm={() => deleteTournament()}
                            label='Delete'
                        />
                    </Modal>
                )
            }
        </>
    );
}