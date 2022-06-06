import { useEffect, useMemo, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import { TournamentsContext } from '../tournaments/TournamentsContext';
import styles from './GalleryPage.module.css';

export default function TournamentsPage() {

    const { tournamentsService } = useApi();

    const [tournaments, setTournaments] = useState<Tournament[] | null>(null);

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
            <div className={['container', styles.root].join(' ')}>
                <Outlet />
            </div>
        </TournamentsContext.Provider>
    );
}