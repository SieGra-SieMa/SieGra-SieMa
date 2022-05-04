import { useState } from 'react';
import styles from './TournamentsPage.module.css';

export default function TournamentsPage() {

    // const [tournaments, setTournaments] = useState([
    const [tournaments] = useState([
        {
            name: 'Global Game',
            startDate: '10.01.2022',
            endDate: '30.01.2022',
            description: 'Our first tournament in 2022',
            address: 'Warsaw Poland'
        },
        {
            name: 'Universe Game',
            startDate: '22.01.2022',
            endDate: '02.02.2022',
            description: 'Our second tournament in 2022',
            address: 'Lviv Ukraine'
        }
    ]);

    return (
        <div className="container">
            <h1>Tournamets</h1>
            <div className={styles.content}>
                {tournaments && tournaments.map((tournament, index) => (
                    <div key={index} className={styles.item}>
                        <div className={styles.header}>
                            <div className={styles.dates}>
                                {tournament.startDate}
                            </div>
                            <h3>
                                {tournament.name}
                            </h3>
                            <div className={styles.dates}>
                                {tournament.endDate}
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
                    </div>
                ))}
            </div>
        </div>
    );
}