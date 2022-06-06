import { useEffect, useState } from 'react';
import { Tournament } from '../../_lib/types';
import { useNavigate } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { useApi } from '../api/ApiContext';
import { useTournaments } from '../tournaments/TournamentsContext';
import styles from './GalleryList.module.css';

export default function GalleryList() {

    const navigate = useNavigate();

    const { tournaments, setTournaments } = useTournaments();

    return (
        <>
        <div className={styles.top}>
                <h2 className={styles.title}>Gallery</h2>
            </div>
            <ul className={styles.content}>
                {tournaments && tournaments.map((tournament, index) => (
                    <li key={index} className={styles.item} style={{backgroundImage: `url(http://localhost:5000/${tournament.profilePicture})`, backgroundPosition: 'center',
                    backgroundSize: 'cover',
                    backgroundRepeat: 'no-repeat'}}>
                        <div className={styles.header} >
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
                    </li>
                ))}
            </ul>
        </>
    );
}