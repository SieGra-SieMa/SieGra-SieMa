import { FormEvent, useState } from 'react';
import { Media } from '../../_lib/_types/response';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './MediaAdd.module.css';

export default function MediaAdd({ confirm, albumId }: { confirm: (media: Media) => void, albumId: number }) {

    const { albumsService } = useApi();

    const [file, setFile] = useState<File | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!file) return;
        const formData = new FormData();
        formData.append("files", file);
        albumsService.addMediaToAlbum(albumId, formData)
            .then((data) => {
                confirm(data)
            });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <label
                className={styles.selectImage}
                style={file ? {
                    backgroundImage: `url(${URL.createObjectURL(file)})`,
                } : undefined}
                htmlFor="MediaAdd-file"
            >
                {file ? file.name : 'Wybierz zdjęcie'}
                <input
                    id="MediaAdd-file"
                    type="file"
                    accept="image/png, image/jpeg"
                    hidden
                    required
                    onChange={(e) => setFile(e.target.files ? e.target.files[0] : null)}
                />
            </label>
            <VerticalSpacing size={15} />
            <Button value='Zatwierdź' />
        </form>
    );
}
