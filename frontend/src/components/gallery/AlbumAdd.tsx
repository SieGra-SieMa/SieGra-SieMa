import { FormEvent, useState } from 'react';
import { Album } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './AlbumAdd.module.css';

export default function AlbumAdd({ confirm, parametr  }: { confirm: (album: Album) => void, parametr: string}) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');
    const [createDate, setCreateDate] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const album: Album = {
            name,
            createDate: createDate
        };
        tournamentsService.addAlbumToTournament(parametr, album)
            .then((data) => {
                confirm(data);
            })
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
            <Input
                id='TournamentAdd-startDate'
                label='Start Date'
                type='date'
                value={createDate}
                required
                onChange={(e) => setCreateDate(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Add' />
        </form>
    );
}

function id(id: any, album: Album) {
    throw new Error('Function not implemented.');
}
