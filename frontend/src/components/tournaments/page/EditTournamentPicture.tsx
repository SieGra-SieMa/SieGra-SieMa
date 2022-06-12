import { FormEvent, useState } from 'react';
import { Tournament } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './EditTournamentPicture.module.css';

type EditTournamentProps = {
    tournament: Tournament;
    confirm: (url: string) => void;
}

export default function EditTournamentPicture({
    tournament,
    confirm,
}: EditTournamentProps) {

    const { tournamentsService } = useApi();

    const [file, setFile] = useState<File | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!file) return;
        const data = new FormData();
        data.append('file', file);
        tournamentsService.addProfilePhoto(tournament.id, data)
            .then((data) => {
                confirm(data[0].url);
            });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <label
                className={styles.selectImage}
                style={file ? {
                    backgroundImage: `url(${URL.createObjectURL(file)})`,
                } : undefined}
                htmlFor="EditTournamentPicture-file"
            >
                {file ? file.name : 'Wybierz zdjęcie'}
                <input
                    id="EditTournamentPicture-file"
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