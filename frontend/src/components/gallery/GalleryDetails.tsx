import { useEffect, useState, useCallback } from 'react';
import { TournamentWithAlbums, Album } from '../../_lib/types';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { useApi } from '../api/ApiContext';
import { useTournaments } from '../tournaments/TournamentsContext';
import styles from './GalleryDetails.module.css';
import Button, { ButtonStyle } from '../form/Button';
import Modal from '../modal/Modal';
import AlbumAdd from './AlbumAdd';
import PhotoAdd from './MediaAdd';


export default function GalleryDetails() {

    const navigate = useNavigate();
    const { tournamentsService, albumsService } = useApi();
    const { id } = useParams<{ id: string }>();
    const [tournamentWithAlbums, setTournamentWithAlbums] = useState<TournamentWithAlbums | null>(null);
    const [album, setAlbum] = useState<Album | null>(null);
    const [addAlbum, setAddAlbum] = useState(false);
    const [addPhoto, setAddPhoto] = useState(false);
    const [albumId, setAlbumId] = useState<string | null>(null);



    useEffect(() => {
        tournamentsService.getTournamentWithAlbums(id!)
            .then(data => { setTournamentWithAlbums(data) });
    }, [addAlbum])

    const getMedia = (id: number) => {
        albumsService.getAlbumWithMedia(String(id))
            .then(data => { setAlbum(data) });
        setAlbumId(String(id));
    }

    const onRemove = (id: number) => {
        const data = tournamentWithAlbums!.albums! ? [...tournamentWithAlbums!.albums!] : [];
        const index = tournamentWithAlbums!.albums!.findIndex(e => e.id === id) ?? -1;
        if (index >= 0) {
            data.splice(index, 1);
            tournamentWithAlbums!.albums! = data;
            setTournamentWithAlbums(tournamentWithAlbums);
        }
    };

    const deleteAlbum = (id: number) => {
        albumsService.deleteAlbum(String(id))
            .then(data => onRemove(id));
    }


    return (
        <>
            <div className={styles.top}>
                <h2 className={styles.title}>{tournamentWithAlbums?.name}</h2>
                <Button
                    value='Add photo'
                    onClick={() => setAddPhoto(true)}
                />
                <Button
                    value='Add album'
                    onClick={() => setAddAlbum(true)}
                />
            </div>
            <ul className={styles.content}>
                {tournamentWithAlbums?.albums && tournamentWithAlbums?.albums?.map((album, index) => (
                    <li key={index} className={styles.item} onClick={() => getMedia(album.id!)} style={{ backgroundImage: `url(http://localhost:5000/${album.profilePicture})`, backgroundPosition: 'center', backgroundSize: 'cover', backgroundRepeat: 'no-repeat' }}>
                        <div className={styles.header} >
                            <h3>
                                {album.name}
                            </h3>
                            <div className={styles.dates}>
                                <Button
                                    value='DeleteAlbum album'
                                    onClick={() => deleteAlbum(album.id!)}
                                />
                            </div>
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
            {addAlbum && (
                <Modal
                    close={() => setAddAlbum(false)}
                    isClose
                    title={`Add album`}
                >
                    <AlbumAdd parametr={id!} confirm={(album) => {
                        setAddAlbum(false);
                        tournamentWithAlbums?.albums?.concat(album);
                        setTournamentWithAlbums(tournamentWithAlbums);
                    }} />
                </Modal>
            )}
            {addPhoto && (
                <Modal
                    close={() => setAddPhoto(false)}
                    isClose
                    title={`Add medium`}
                >
                    <PhotoAdd parametr={albumId!} confirm={(medium) => {
                        setAddPhoto(false);
                        // setAddAlbum(false);
                        // tournamentWithAlbums?.albums?.concat(album);
                        // setTournamentWithAlbums(tournamentWithAlbums);
                        album?.mediaList?.concat(medium);
                        setAlbum(album);
                    }} />
                </Modal>
            )}
        </>
    );
}