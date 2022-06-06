import { useEffect, useState } from 'react';
import { TournamentWithAlbums, Album } from '../../_lib/types';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { useApi } from '../api/ApiContext';
import { useTournaments } from '../tournaments/TournamentsContext';
import styles from './GalleryDetails.module.css';

export default function GalleryDetails() {

    const navigate = useNavigate();
    const { tournamentsService, albumsService } = useApi();
    const { id } = useParams<{ id: string }>();
    const [tournamentWithAlbums, setTournamentWith] = useState<TournamentWithAlbums | null>(null);
    const [album, setAlbum] = useState<Album | null>(null);

    useEffect(() => {
        tournamentsService.getTournamentWithAlbums(id!)
            .then(data => { setTournamentWith(data) });

    }, [id, tournamentWithAlbums])

    const getMedia = (id: number) => {
        albumsService.getAlbumWithMedia(String(id))
            .then(data => {setAlbum(data)});
    }


    return (
        <>
            <div className={styles.top}>
                <h2 className={styles.title}>{tournamentWithAlbums?.name}</h2>
            </div>
            <ul className={styles.content}>
                {tournamentWithAlbums?.albums && tournamentWithAlbums?.albums?.map((album, index) => (
                    <li key={index} className={styles.item} onClick={() => getMedia(album.id)} style={{ backgroundImage: `url(http://localhost:5000/${album.profilePicture})`, backgroundPosition: 'center', backgroundSize: 'cover', backgroundRepeat: 'no-repeat' }}>
                        <div className={styles.header} >
                            <h3>
                                {album.name}
                            </h3>
                        </div>
                    </li>
                ))}
            </ul>
            <ul className={styles.image}>
                {album && album?.mediaList?.map((media, index) => (
                    <li key={index} className={styles.item} style={{ backgroundImage: `url(http://localhost:5000/${media.url})`, backgroundPosition: 'center', backgroundSize: 'cover', backgroundRepeat: 'no-repeat' }}>
                    </li>
                ))}
            </ul>
        </>
    );
}