import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import styles from './TournamentPage.module.css';


export default function TournamentPage() {

    const navigate = useNavigate();

    const { id } = useParams<{ id: string }>();

    const { tournamentsService } = useApi();

    const [tournament, setTournament] = useState<Tournament | null>(null);

    useEffect(() => {
        tournamentsService.getTournamentbyId(id!)
            .then((data) => {
                setTournament(data);
            });
    }, [id, tournamentsService]);

    return (
        <div className={['container', styles.root].join(' ')}>
            <div className={styles.top}>
                <Button value='Back' onClick={() => navigate('..')} />
                <h1 className={styles.title}>
                    {tournament && tournament.name}
                </h1>
            </div>

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
                                    <th>Win</th>
                                    <th>Lose</th>
                                    <th>Draft</th>
                                    <th>Points</th>
                                </tr>
                            </thead>
                            <tbody>
                                {group.teams && group.teams.map((team, index) => (
                                    <tr>
                                        <td>{index}</td>
                                        <td>{team.name}</td>
                                        <td>0</td>
                                        <td>0</td>
                                        <td>0</td>
                                        <td>0</td>
                                        <td>0</td>
                                    </tr>
                                ))}
                                <tr>
                                    <td>1</td>
                                    <td>Team name</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>Team name</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr><tr>
                                    <td>1</td>
                                    <td>Team name</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr><tr>
                                    <td>1</td>
                                    <td>Team name</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr>
                            </tbody>
                        </table>
                    </li>
                ))}
            </ul>
        </div>
    );
}