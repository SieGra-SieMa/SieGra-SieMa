import { FormEvent, useState } from 'react';
import { Album, AlbumRequest } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './AlbumAdd.module.css';


type AlbumAddProps = {
    confirm: (album: Album) => void;
    tournamentId: string;
};

export default function AlbumAdd({ confirm, tournamentId }: AlbumAddProps) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const album: AlbumRequest = {
            name,
            createDate: new Date().toISOString(),
        };
        tournamentsService.addAlbum(tournamentId, album)
            .then((data) => {
                confirm(data);
            });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='TournamentAdd-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </form>
    );
};
