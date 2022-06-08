import Config from '../../../config.json';
import { useNavigate } from 'react-router-dom';
import { TournamentListItem as TournamentListItemType } from '../../../_lib/_types/tournament';
import ImageIcon from '@mui/icons-material/Image';
import styles from './TournamentsList.module.css';
import Button from '../../form/Button';
import { useState } from 'react';
import Modal from '../../modal/Modal';
import TeamAssign from './TeamAssign';
import { useTournaments } from '../TournamentsContext';
import { useUser } from '../../user/UserContext';


type TournamentsListItemProps = {
    tournament: TournamentListItemType;
};

export default function TournamentsListItem({
    tournament
}: TournamentsListItemProps) {

    const navigate = useNavigate();

    const { user } = useUser();
    const { tournaments, setTournaments } = useTournaments();

    const [isTeamAssign, setIsTeamAssign] = useState(false);

    return (<>
        <li className={styles.item} onClick={() => navigate(`${tournament.id}`)}>
            <div className={styles.pictureBlock} style={tournament.profilePicture ? {
                backgroundImage: `url(${Config.HOST}${tournament.profilePicture})`,
            } : undefined}>
                {(!tournament.profilePicture) && <ImageIcon className={styles.picture} />}
            </div>
            <div className={styles.itemContent}>
                <div className={styles.itemDetails}>
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
                {user && (!tournament.status && !tournament.isUserEnroll) && (<>
                    <Button
                        value='Zapisz zespół'
                        onClick={(e) => {
                            e.stopPropagation();
                            setIsTeamAssign(true);
                        }}
                    />
                </>)}
            </div>
        </li>
        {user && (!tournament.status && !tournament.isUserEnroll) && isTeamAssign && (
            <Modal
                title={`Zapisz zespół - "${tournament.name}"`}
                isClose
                close={() => setIsTeamAssign(false)}
            >
                <TeamAssign
                    id={tournament.id}
                    confirm={() => {
                        setIsTeamAssign(false);
                        const updatedTournament = {
                            ...tournament,
                            status: false,
                            isUserEnroll: true,
                        };
                        const filtered = tournaments!.filter(
                            (e) => e.id !== updatedTournament.id
                        );
                        setTournaments([...filtered, updatedTournament]);
                    }}
                />
            </Modal>
        )}
    </>);
};
