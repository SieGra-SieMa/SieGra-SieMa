import { FormEvent, useState } from 'react';
import { Album, AlbumRequest } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Form from '../form/Form';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';


type Props = {
    confirm: (album: Album) => void;
    tournamentId: string;
};

export default function CreateAlbum({ confirm, tournamentId }: Props) {

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
        <Form onSubmit={onSubmit}>
            <Input
                id='CreateAlbum-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Dodaj' />
        </Form>
    );
};
