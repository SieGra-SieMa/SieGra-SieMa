import { FormEvent, useState } from 'react';
import { Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './EditTeamPicture.module.css';

type EditTeamPictureProps = {
    team: Team;
    confirm: (url: string) => void;
}

export default function EditTeamPicture({
    team,
    confirm,
}: EditTeamPictureProps) {

    const { teamsService } = useApi();

    const [file, setFile] = useState<File | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!file) return;
        const data = new FormData();
        data.append('file', file);
        teamsService.addProfilePhoto(team.id, data)
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
                htmlFor="EditTeamPicture-file"
            >
                {file ? file.name : 'Wybierz zdjęcie'}
                <input
                    id="EditTeamPicture-file"
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