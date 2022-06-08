import { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../../_lib/roles';
import { Tournament as TournamentType } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button, { ButtonStyle } from '../../form/Button';
import GuardComponent from '../../guard-components/GuardComponent';
import Ladder from '../ladder/Ladder';
import Confirm from '../../modal/Confirm';
import Modal from '../../modal/Modal';
import EditTournament from './EditTournament';
import { useTournaments } from '../TournamentsContext';
import styles from './Tournament.module.css';
import { TournamentContext } from '../TournamentContext';
import EditTournamentPicture from './EditTournamentPicture';
import Groups from '../groups/Groups';

export default function Tournament() {

    const navigate = useNavigate();

    const { id } = useParams<{ id: string }>();

    const { tournamentsService } = useApi();
    const { tournaments, setTournaments } = useTournaments();

    const [tournament, setTournament] = useState<TournamentType | null>(() => {
        const tournament = tournaments?.find((tournament) => tournament.id === parseInt(id!));
        if (!tournament) return null;
        return {
            ...tournament,
            groups: [],
            ladder: [],
        };
    });
    const [isEdit, setIsEdit] = useState(false);
    const [isDelete, setIsDelete] = useState(false);
    const [isPicture, setIsPicture] = useState(false);

    const isOpen = tournament?.status || (tournament?.groups?.length ?? 0) === 0;

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    useEffect(() => {
        tournamentsService.getTournamentbyId(id!)
            .then((data) => {
                setTournament(data);
            });
    }, [id, tournamentsService]);

    useEffect(() => {
        if (!isOpen) return;

        tournamentsService.getTeamsInTournament(id!)
            .then((data) => {
                console.log(data);
            });

    }, [isOpen, id, tournamentsService]);

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
            .then((data) => {
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
                <Button value='Wstecz' onClick={() => navigate('..')} />
                {tournament && (
                    <GuardComponent roles={[ROLES.Admin]}>
                        <div className={styles.adminControls}>
                            <Button
                                value='Edytuj zdjęcie profilowe'
                                onClick={() => setIsPicture(true)}
                                style={ButtonStyle.DarkBlue}
                            />
                            <Button
                                value='Edytuj turniej'
                                onClick={() => setIsEdit(true)}
                                style={ButtonStyle.DarkBlue}
                            />
                            <Button
                                value='Usuń turniej'
                                onClick={() => setIsDelete(true)}
                                style={ButtonStyle.Red}
                            />
                        </div>
                    </GuardComponent>
                )}
            </div>

            <div className={styles.details}>
                <h1 className={styles.title}>{tournament && tournament.name}</h1>
                <h4 className={styles.dates}>
                    {tournament && (<>
                        {new Date(tournament.startDate).toLocaleString()}
                        <span className={styles.line}> | </span>
                        {new Date(tournament.endDate).toLocaleString()}
                    </>)}
                </h4>
                <p>{tournament && tournament.address}</p>
                <p>{tournament && tournament.description}</p>
            </div>





            {tournament?.status && (
                <>
                    <h2>Ladder</h2>
                    {tournament && tournament.ladder && <Ladder ladder={tournament.ladder} />}
                    <h2>Groups</h2>
                    {tournament && tournament.groups && <Groups groups={tournament.groups} />}
                    <h2>Matches</h2>
                </>
            )}



            {tournament && isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title={`Edytuj turniej  - "${tournament.name}"`}
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
                    title={`Czy na pewno chcesz usunąć turniej - "${tournament.name}"?`}
                >
                    <Confirm
                        cancel={() => setIsDelete(false)}
                        confirm={deleteTournament}
                        label='Usuń'
                    />
                </Modal>
            )}
            {tournament && isPicture && (
                <Modal
                    isClose
                    close={() => setIsPicture(false)}
                    title={`Edytuj zdjęcie profilowe - "${tournament.name}"`}
                >
                    <EditTournamentPicture
                        tournament={tournament}
                        confirm={(url) => {
                            setIsPicture(false);
                            const updatedTournament = {
                                ...tournament,
                                profilePicture: url,
                            };
                            setTournament(updatedTournament);
                            setIsEdit(false);
                            if (tournaments) {
                                const filtered = tournaments.filter(
                                    (e) => e.id !== updatedTournament.id
                                );
                                setTournaments([...filtered, updatedTournament]);
                            }
                        }}
                    />
                </Modal>
            )}
        </TournamentContext.Provider>
    );
};