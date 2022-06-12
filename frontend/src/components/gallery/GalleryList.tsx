import Config from '../../config.json';
import { useNavigate } from 'react-router-dom';
import { useTournaments } from '../tournaments/TournamentsContext';
import ImageIcon from '@mui/icons-material/Image';
import styles from './GalleryList.module.css';
import { useEffect } from 'react';

export default function GalleryList() {

    const navigate = useNavigate();

    const { tournaments } = useTournaments();

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <>
            <div className={styles.top}>
                <h1>Galeria</h1>
            </div>
            <ul className={styles.content}>
                {tournaments && tournaments.map((tournament, index) => (
                    <li
                        key={index}
                        className={styles.item}
                        onClick={() => navigate(`${tournament.id!}/albums`)}
                        style={tournament.profilePicture ? {
                            backgroundImage: `url(${Config.HOST}${tournament.profilePicture})`
                        } : undefined}
                    >
                        <div className={styles.box}>
                            {(!tournament.profilePicture) && <ImageIcon className={styles.picture} />}
                            <h4>
                                {tournament.name}
                            </h4>
                        </div>
                    </li>
                ))}
            </ul>
        </>
    );
}