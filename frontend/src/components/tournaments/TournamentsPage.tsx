import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import GuardComponent from '../guard-components/GuardComponent';
import Modal from '../modal/Modal';
import TournamentAdd from './TournamentAdd';
import styles from './TournamentsPage.module.css';

export default function TournamentsPage() {

    const navigate = useNavigate();

    const { tournamentsService } = useApi();

    const [isAdd, setIsAdd] = useState(false);

    useEffect(() => {
        tournamentsService.getTournaments()
            .then((data) => {
                setTournament(data);
            });
    }, [tournamentsService]);

    const [tournaments, setTournament] = useState<Tournament[] | null>(null);

    return (
        <div className={['container', styles.root].join(' ')}>
            <div className={styles.top}>
                <h2 className={styles.title}>Tournaments</h2>
                <GuardComponent roles={[ROLES.Admin]}>
                    <button className="button" onClick={() => setIsAdd(true)}>Add</button>
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
                    <TournamentAdd confirm={() => setIsAdd(false)} />
                </Modal>
            )}
        </div>
    );
}