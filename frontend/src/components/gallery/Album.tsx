import Config from '../../config.json';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ROLES } from '../../_lib/roles';
import { useApi } from '../api/ApiContext';
import styles from './AlbumsList.module.css';
import Button, { ButtonStyle } from '../form/Button';
import Modal from '../modal/Modal';
import MediaAdd from './MediaAdd';
import GuardComponent from '../guard-components/GuardComponent';
import { Album as AlbumType } from '../../_lib/_types/tournament';
import Confirm from '../modal/Confirm';
import { Media } from '../../_lib/_types/response';
import EditAlbum from './EditAlbum';
import VerticalSpacing from '../spacing/VerticalSpacing';

export default function Album() {

    const navigate = useNavigate();

    const { albumsService, mediaService } = useApi();

    const { albumId } = useParams<{ albumId: string }>();

    const [album, setAlbum] = useState<AlbumType | null>(null);

    const [isAddPhoto, setIsAddPhoto] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [isRemove, setIsRemove] = useState(false);


    const [selectedImage, setSelectedImage] = useState<Media | null>(null);

    useEffect(() => {
        albumsService.getAlbumWithMedia(albumId!)
            .then((data) => {
                setAlbum(data);
            });
    }, [albumId, albumsService]);

    const removeAlbum = () => {
        albumsService.deleteAlbum(albumId!)
            .then(() => navigate('..'));
    }

    const deleteMedia = () => {
        if (!selectedImage || !album) return;
        mediaService.deleteMedia(selectedImage.id)
            .then(() => {
                setAlbum({
                    ...album,
                    mediaList: album.mediaList.filter((media) => media.id !== selectedImage.id)
                });
                setSelectedImage(null)
            });
    }

    return (
        <>
            <div className={styles.top}>
                <Button value='Wstecz' onClick={() => navigate('..')} />

                <GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
                    <div className={styles.adminControls}>
                        <Button
                            value='Dodaj zdjęcie'
                            onClick={() => setIsAddPhoto(true)}
                        />
                        <Button
                            value='Edytuj album'
                            onClick={() => setIsEdit(true)}
                            style={ButtonStyle.DarkBlue}
                        />
                        <Button
                            value='Usuń album'
                            onClick={() => setIsRemove(true)}
                            style={ButtonStyle.Red}
                        />
                    </div>
                </GuardComponent>
            </div>

            {(album) && (<>
                <h2 className={styles.title}>
                    {album.name}
                </h2>
                <ul className={styles.images}>
                    {album.mediaList.map((media, index) => (
                        <li
                            key={index}
                            className={styles.item}
                            style={{
                                backgroundImage: `url(${Config.HOST}${media.url})`,
                            }}
                            onClick={() => setSelectedImage(media)}
                        >
                        </li>
                    ))}
                </ul>
            </>)}

            {(album && isAddPhoto) && (
                <Modal
                    close={() => setIsAddPhoto(false)}
                    isClose
                    title='Dodaj zdjęcie'
                >
                    <MediaAdd
                        albumId={album.id}
                        confirm={(media) => {
                            setAlbum({
                                ...album,
                                mediaList: album.mediaList.concat(media)
                            });
                            setIsAddPhoto(false);
                        }} />
                </Modal>
            )}
            {(album && isEdit) && (
                <Modal
                    close={() => setIsEdit(false)}
                    isClose
                    title='Edytuj album'
                >
                    <EditAlbum
                        album={album}
                        confirm={(album) => {
                            setAlbum(album);
                            setIsEdit(false);
                        }}
                    />
                </Modal>
            )}
            {(album && isRemove) && (
                <Modal
                    close={() => setIsRemove(false)}
                    isClose
                    title='Dodaj zdjęcie'
                >
                    <Confirm cancel={() => setIsRemove(false)}
                        confirm={removeAlbum}
                        label='Usuń'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}

            {(album && selectedImage) && (
                <Modal
                    close={() => setSelectedImage(null)}
                    isClose
                    title=''
                >
                    <>
                        <img alt='' className={styles.image} src={`${Config.HOST}${selectedImage.url}`} />
                        <GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
                            <VerticalSpacing size={30} />
                            <Button
                                value='Usuń'
                                style={ButtonStyle.Red}
                                onClick={deleteMedia} />
                        </GuardComponent>
                    </>
                </Modal>
            )}
        </>
    );
}