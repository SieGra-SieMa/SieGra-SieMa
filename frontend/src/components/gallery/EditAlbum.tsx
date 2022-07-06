import { FormEvent, useState } from 'react';
import { Album } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Form from '../form/Form';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';


type Props = {
    confirm: (album: Album) => void;
    album: Album;
};

export default function EditAlbum({ confirm, album }: Props) {

    const { albumsService } = useApi();

    const [name, setName] = useState(album.name);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedAlbum: Album = {
            ...album,
            name,
        };
        albumsService.editAlbum(album.id, updatedAlbum)
            .then((data) => {
                confirm(updatedAlbum);
            });
    }

    return (
        <Form onSubmit={onSubmit}>
            <Input
                id='EditAlbum-name'
                label='Name'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Zapisz' />
        </Form>
    );
};
