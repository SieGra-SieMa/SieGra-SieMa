import { FormEvent, useState } from 'react';
import { Media } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './AlbumAdd.module.css';

export default function MediaAdd({ confirm, parametr }: { confirm: (media: Media) => void, parametr: string }) {

    const { albumsService } = useApi();

    const [files, setFiles] = useState<FileList | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        // const album: Media = {
        //     createDate: createDate
        // };
        // tournamentsService.addAlbumToTournament(parametr, album)
        //     .then((data) => {
        //         confirm(data);
        //     })
        //File[] xd = null;
        const formData = new FormData();
        formData.append("files", files?.item(0)!, files?.item(0)!.name);
        //console.log(files?.item(0)!);

        //const fileListAsArray = Array.from(files!);
        // albumsService.addMediaToAlbum(parametr, fileListAsArray)
        //      .then((data) => {
        //          confirm(data);
        //      })
        albumsService.addMediaToAlbum(parametr, formData)
            .then((data) => { confirm(data) });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <input
                id='MediaAdd-file'
                type='file'
                onChange={(e) => setFiles(e.target.files)}
            />
            <VerticalSpacing size={15} />
            <Button value='Add' />
        </form>
    );
}

function id(id: any, media: Media) {
    throw new Error('Function not implemented.');
}
