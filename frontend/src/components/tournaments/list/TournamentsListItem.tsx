import Config from '../../../config.json';
import { useNavigate } from 'react-router-dom';
import { TournamentListItem as TournamentListItemType } from '../../../_lib/_types/tournament';
import ImageIcon from '@mui/icons-material/Image';
import styles from './TournamentsList.module.css';


type TournamentsListItemProps = {
    tournament: TournamentListItemType;
};

export default function TournamentsListItem({
    tournament
}: TournamentsListItemProps) {

    const navigate = useNavigate();

    return (
        <li className={styles.item} onClick={() => navigate(`${tournament.id}`)}>
            <div className={styles.pictureBlock} style={tournament.profilePicture ? {
                backgroundImage: `url(${Config.HOST}${tournament.profilePicture})`,
            } : undefined}>
                {(!tournament.profilePicture) && <ImageIcon className={styles.picture} />}
            </div>
            <div className={styles.itemContent}>
                <h4 className={styles.itemTitle}>{tournament.name}</h4>
                <p className={styles.dates}>
                    {new Date(tournament.startDate).toLocaleString()}
                    <span className={styles.line}> | </span>
                    {new Date(tournament.endDate).toLocaleString()}
                </p>
                <p>
                    {tournament.address}
                    <br />
                    {tournament.description}
                </p>
            </div>
        </li>
    );
};
