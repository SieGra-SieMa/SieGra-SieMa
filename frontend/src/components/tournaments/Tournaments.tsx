import { useEffect, useMemo, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { TournamentListItem } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import { TournamentsContext } from './TournamentsContext';
import styles from './Tournaments.module.css';

export default function Tournaments() {

    const { tournamentsService } = useApi();

    const [tournaments, setTournaments] = useState<TournamentListItem[] | null>(null);

    useEffect(() => {
        tournamentsService.getTournaments()
            .then((data) => {
                setTournaments(data);
            });
    }, [tournamentsService]);

    const value = useMemo(() => {
        return {
            tournaments: tournaments?.sort((a, b) => new Date(b.endDate).getTime() - new Date(a.endDate).getTime())
                .sort((a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime()) ?? null,
            setTournaments
        }
    }, [tournaments, setTournaments]);

    return (
        <TournamentsContext.Provider value={value}>
            <div className={styles.wrapper}>
                <div className={['container', styles.root].join(' ')}>
                    <Outlet />
                </div>
            </div>
        </TournamentsContext.Provider>
    );
}