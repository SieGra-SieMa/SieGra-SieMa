import { useState } from 'react';
import { ROLES } from '../../../_lib/roles';
import Button from '../../form/Button';
import GuardComponent from '../../guard-components/GuardComponent';
import Modal from '../../modal/Modal';
import CreateTournament from './CreateTournament';
import { useTournaments } from '../TournamentsContext';
import styles from './TournamentsList.module.css';
import TournamentsListItem from './TournamentsListItem';
import { SyncLoader } from 'react-spinners';

export default function TournamentsList() {

    const { tournaments, setTournaments } = useTournaments();

    const [isAdd, setIsAdd] = useState(false);

    return (
        <>
            <div className={styles.top}>
                <h1>Turnieje</h1>
                <GuardComponent roles={[ROLES.Admin]}>
                    <Button value='Dodaj turniej' onClick={() => setIsAdd(true)} />
                </GuardComponent>
            </div>
            <ul className={styles.content}>
                {tournaments ? tournaments.map((tournament, index) => (
                    <TournamentsListItem
                        key={index}
                        tournament={tournament}
                    />
                )) : (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={20} margin={20} color='#fff' />
                    </div>
                )}
            </ul>
            {isAdd && (
                <Modal
                    close={() => setIsAdd(false)}
                    isClose
                    title={`Dodaj turniej`}
                >
                    <CreateTournament confirm={(tournament) => {
                        setIsAdd(false);
                        setTournaments(tournaments ? [...tournaments, tournament] : [tournament]);
                    }} />
                </Modal>
            )}
        </>
    );
}