import Config from '../../../config.json';
import { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../../_lib/roles';
import { TeamInTournament, Tournament as TournamentType } from '../../../_lib/_types/tournament';
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
import TeamAssign from '../list/TeamAssign';
import { useAuth } from '../../auth/AuthContext';
import { SyncLoader } from 'react-spinners';
import TeamsList from '../teams/TeamsList';
import Matches from '../matches/Matches';
import ImageIcon from '@mui/icons-material/Image';

export default function Tournament() {

    const navigate = useNavigate();

    const { id } = useParams<{ id: string }>();

    const { tournamentsService } = useApi();
    const { session } = useAuth();
    const { tournaments, setTournaments } = useTournaments();

    const [tournament, setTournament] = useState<TournamentType | null>(() => {
        const tournament = tournaments?.find((tournament) => tournament.id === parseInt(id!));
        if (!tournament) return null;
        return {
            ...tournament,
            albums: [],
            groups: [],
            ladder: [],
        };
    });
    const [isLoading, setIsLoading] = useState(true);
    const [teams, setTeams] = useState<TeamInTournament[] | null>(null);
    const [isPrepare, setIsPrepare] = useState(false);
    const [isReset, setIsReset] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [isDelete, setIsDelete] = useState(false);
    const [isPicture, setIsPicture] = useState(false);
    const [isTeamAssign, setIsTeamAssign] = useState(false);
    const [isTeamRemove, setIsTeamRemove] = useState(false);
    const [isLadderCompose, setIsLadderCompose] = useState(false);
    const [isLadderReset, setIsLadderReset] = useState(false);


    const isOpen = tournament?.isOpen;

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    useEffect(() => {
        tournamentsService.getTournamentById(id!)
            .then((data) => {
                setTournament(data);
                setIsLoading(false);
            });
    }, [id, tournamentsService]);

    useEffect(() => {
        if (!isOpen) return;

        tournamentsService.getTeamsInTournament(id!)
            .then((data) => {
                setTeams(data);
            });

    }, [isOpen, id, tournamentsService]);

    const prepareTournament = () => {
        tournamentsService.prepareTournamnet(id!)
            .then((data) => {
                setTournament(data);
                setIsPrepare(false);
            })
    };

    const resetTournament = () => {
        tournamentsService.resetTournament(id!)
            .then((data) => {
                setTournament(data);
                setIsReset(false);
            })
    };

    const editTournament = useCallback((updatedTournament: TournamentType) => {
        setTournament(updatedTournament);
        setIsEdit(false);
        if (tournaments) {
            const index = tournaments!.findIndex(
                (e) => e.id === updatedTournament.id
            );
            const data = [...tournaments];
            data[index] = updatedTournament;
            setTournaments(data);
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

    const removeTeam = (teamId: number) => () => {
        tournamentsService.removeTeam(id!, teamId)
            .then(() => {
                setIsTeamRemove(false);
                const updatedTournament = {
                    ...tournament!,
                    team: null,
                }
                setTournament(updatedTournament);
                if (tournaments) {
                    const index = tournaments!.findIndex(
                        (e) => e.id === updatedTournament.id
                    );
                    const data = [...tournaments];
                    data[index] = updatedTournament;
                    setTournaments(data);
                }
                setTeams(teams && teams.filter((team) => team.teamId !== teamId));
            });
    };

    const composeLadder = () => {
        tournamentsService.composeLadder(id!)
            .then((data) => {
                setTournament(data);
                setIsLadderCompose(false);
            });
    }

    const resetLadder = () => {
        tournamentsService.resetLadder(id!)
            .then((data) => {
                setTournament(data);
                setIsLadderReset(false);
            });
    }

    return (
        <TournamentContext.Provider value={{ tournament, setTournament, teams, setTeams }}>
            <div className={styles.top}>
                <Button value='Wstecz' onClick={() => navigate('..')} />
                {tournament && (
                    <GuardComponent roles={[ROLES.Admin]}>
                        <div className={styles.adminControls}>
                            {(isOpen) ? (
                                <Button
                                    value='Przygotuj turniej'
                                    onClick={() => setIsPrepare(true)}
                                    style={ButtonStyle.Yellow}
                                />
                            ) : (
                                <Button
                                    value='Resetuj turniej'
                                    onClick={() => setIsReset(true)}
                                    style={ButtonStyle.Red}
                                />
                            )}
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

            {tournament && (
                <div>
                    <h1 className={styles.title}>{tournament.name}</h1>
                    <h4 className={styles.dates}>
                        {new Date(tournament.startDate).toLocaleString()}
                        <span className={styles.line}> | </span>
                        {new Date(tournament.endDate).toLocaleString()}
                    </h4>
                    <div className={styles.details}>
                        <p>Address: {tournament.address}</p>
                        <p>Opis: {tournament.description}</p>
                        {(session && tournament.isOpen) && (
                            tournament.team ? (
                                <div className={styles.team}>
                                    <h6>{tournament.team.name}</h6>
                                    <Button
                                        value='Usuń zespół'
                                        onClick={() => setIsTeamRemove(true)}
                                        style={ButtonStyle.Red}
                                    />
                                </div>
                            ) : (
                                <Button
                                    value='Zapisz zespół'
                                    onClick={() => setIsTeamAssign(true)}
                                />
                            )
                        )}
                    </div>
                </div>
            )}
            {(isOpen) && (<TeamsList teams={teams} />)}
            {(!isOpen) && (
                isLoading ? (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={20} margin={20} color='#fff' />
                    </div>
                ) : (<>
                    {(tournament && !isOpen) && (<>
                        {tournament.groups.length > 1 && (<>
                            <Groups groups={tournament.groups} />
                            <Matches groups={tournament.groups} />
                        </>)}
                        {tournament.ladder[0]?.matches[0].teamHome && <Ladder ladder={tournament.ladder} />}
                        <GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
                            {tournament && tournament.groups.map((group) =>
                                group.matches?.map(e =>
                                    e.teamAwayScore !== null && e.teamHomeScore !== null).every(e => e)
                                ?? true)
                                .every(e => e) && (
                                    tournament.ladder[0]?.matches[0].teamHome ? (
                                        <div className={styles.ladderControls}>
                                            <Button
                                                value='Zresetuj drabinke'
                                                onClick={() => setIsLadderReset(true)}
                                                style={ButtonStyle.Red}
                                            />
                                        </div>

                                    ) : (
                                        <div className={styles.ladderControls}>
                                            <Button
                                                value='Zbuduj drabinke'
                                                onClick={() => setIsLadderCompose(true)}
                                            />
                                        </div>
                                    )
                                )}
                        </GuardComponent>
                    </>)}
                </>)
            )}

            {(tournament && tournament.albums.length > 0) && (<>
                <h4>Albumy</h4>
                <ul className={styles.albums}>
                    {tournament.albums.map((album, index) => (
                        <li
                            key={index}
                            className={styles.item}
                            style={album.profilePicture ? {
                                backgroundImage: `url(${Config.HOST}${album.profilePicture})`,
                            } : undefined}
                            onClick={() => navigate(`/gallery/${id!}/albums/${album.id}`)}>
                            <div className={styles.box}>
                                {(!album.profilePicture) && <ImageIcon className={styles.picture} />}
                                <h4>
                                    {album.name}
                                </h4>
                            </div>
                        </li>
                    ))}
                </ul>
            </>)}

            {(tournament && isPrepare) && (
                <Modal
                    isClose
                    close={() => setIsPrepare(false)}
                    title={`Czy na pewno chcesz przygotuj turniej`}
                >
                    <Confirm
                        cancel={() => setIsPrepare(false)}
                        confirm={prepareTournament}
                        label='Potwierdź'
                        style={ButtonStyle.Yellow}
                    />
                </Modal>
            )}
            {(tournament && isReset) && (
                <Modal
                    isClose
                    close={() => setIsReset(false)}
                    title={`Czy na pewno chcesz zresetować turniej?"`}
                >
                    <Confirm
                        cancel={() => setIsReset(false)}
                        confirm={resetTournament}
                        label='Potwierdź'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
            {(tournament && isEdit) && (
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
            {(tournament && isDelete) && (
                <Modal
                    close={() => setIsDelete(false)}
                    title='Czy na pewno chcesz usunąć turniej"?'
                >
                    <Confirm
                        cancel={() => setIsDelete(false)}
                        confirm={deleteTournament}
                        label='Usuń'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
            {(tournament && isPicture) && (
                <Modal
                    isClose
                    close={() => setIsPicture(false)}
                    title='Edytuj zdjęcie profilowe'
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
                                const index = tournaments!.findIndex(
                                    (e) => e.id === updatedTournament.id
                                );
                                const data = [...tournaments];
                                data[index] = updatedTournament;
                                setTournaments(data);
                            }
                        }}
                    />
                </Modal>
            )}
            {(session && tournament && tournament.isOpen && !tournament.team && isTeamAssign) && (
                <Modal
                    title='Zapisz zespół'
                    isClose
                    close={() => setIsTeamAssign(false)}
                >
                    <TeamAssign
                        id={tournament.id}
                        confirm={(team) => {
                            setIsTeamAssign(false);
                            const updatedTournament = {
                                ...tournament,
                                team,
                            };
                            setTournament(updatedTournament);
                            if (tournaments) {
                                const index = tournaments!.findIndex(
                                    (e) => e.id === updatedTournament.id
                                );
                                const data = [...tournaments];
                                data[index] = updatedTournament;
                                setTournaments(data);
                            }
                            const newTeams: TeamInTournament[] = [];
                            if (teams) {
                                newTeams.push(...teams);
                            }
                            newTeams.push({
                                teamId: team.id,
                                tournamentId: tournament.id,
                                teamName: team.name,
                                teamProfileUrl: team.profilePicture,
                                paid: false
                            });
                            setTeams(newTeams);
                        }}
                    />
                </Modal>
            )}
            {(session && tournament && tournament.isOpen && tournament.team && isTeamRemove) && (
                <Modal
                    title={`Czy na pewno chcesz usunąć zespół - "${tournament.team.name}"?`}
                    isClose
                    close={() => setIsTeamRemove(false)}
                >
                    <Confirm
                        confirm={removeTeam(tournament.team.id)}
                        cancel={() => setIsTeamRemove(false)}
                        label='Usuń'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
            {(isLadderCompose) && (
                <Modal
                    title={`Czy na pewno chcesz zbudować drabinke?`}
                    isClose
                    close={() => setIsLadderCompose(false)}
                >
                    <Confirm
                        confirm={composeLadder}
                        cancel={() => setIsLadderCompose(false)}
                        label='Potwierdź'
                        style={ButtonStyle.Yellow}
                    />
                </Modal>
            )}
            {(isLadderReset) && (
                <Modal
                    title={`Czy na pewno chcesz zresetować drabinke?`}
                    isClose
                    close={() => setIsLadderReset(false)}
                >
                    <Confirm
                        confirm={resetLadder}
                        cancel={() => setIsLadderReset(false)}
                        label='Potwierdź'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
        </TournamentContext.Provider>
    );
};