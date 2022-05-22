import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import Button from '../form/Button';
import GuardComponent from '../guard-components/GuardComponent';
import Modal from '../modal/Modal';
import TournamentAdd from './TournamentAdd';
import { useTournaments } from './TournamentsContext';
import styles from './TournamentsList.module.css';


export default function TournamentsList() {

    const navigate = useNavigate();

    const { tournaments, setTournaments } = useTournaments();

    const [isAdd, setIsAdd] = useState(false);

    return (
        <>
            <div className={styles.top}>
                <h2 className={styles.title}>Tournaments</h2>
                <GuardComponent roles={[ROLES.Admin]}>
                    <Button value='Add' onClick={() => setIsAdd(true)} />
                </GuardComponent>
            </div>
            <ul className={styles.content}>
                {tournaments && tournaments.map((tournament, index) => (
                    <li key={index} className={styles.item} onClick={() => navigate(`${tournament.id!}`)}>
                        <div className={styles.header}>
                            <div className={styles.dates}>
                                {new Date(tournament.startDate).toLocaleDateString()}
                            </div>
                            <h3>
                                {tournament.name}
                            </h3>
                            <div className={styles.dates}>
                                {new Date(tournament.endDate).toLocaleDateString()}
                            </div>
                        </div>
                        <div>
                            <div>
                                Description: {tournament.description}
                            </div>
                            <div>
                                Address: {tournament.address}
                            </div>
                        </div>
                    </li>
                ))}
            </ul>
            {isAdd && (
                <Modal
                    close={() => setIsAdd(false)}
                    isClose
                    title={`Add tournament`}
                >
                    <TournamentAdd confirm={(tournament) => {
                        setIsAdd(false);
                        setTournaments(tournaments ? [...tournaments, tournament] : [tournament]);
                    }} />
                </Modal>
            )}
        </>
    );
}