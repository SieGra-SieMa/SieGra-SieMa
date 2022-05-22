import { FormEvent, useState } from 'react';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './TournamentEdit.module.css';

type TournamentEditProps = {
    tournament: Tournament;
    confirm: (tournament: Tournament) => void;
}

export default function TournamentEdit({
    tournament,
    confirm,
}: TournamentEditProps) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState(tournament.name);
    const [description, setDescription] = useState(tournament.description);
    const [address, setAddress] = useState(tournament.address);

    const [startDate, setStartDate] = useState(new Date(tournament.startDate).toISOString().split('T')[0]);
    const [endDate, setEndDate] = useState(new Date(tournament.endDate).toISOString().split('T')[0]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedTournament: Tournament = {
            id: tournament.id,
            name,
            startDate,
            endDate,
            description,
            address
        };
        tournamentsService.updateTournament(updatedTournament)
            .then((data) => {
                // TODO check update endpoint response
                confirm(updatedTournament);
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
                id='TournamentAdd-description'
                label='Description'
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />
            <Input
                id='TournamentAdd-address'
                label='Address'
                value={address}
                required
                onChange={(e) => setAddress(e.target.value)}
            />
            <Input
                id='TournamentAdd-startDate'
                label='Start Date'
                type='date'
                value={startDate}
                required
                onChange={(e) => setStartDate(e.target.value)}
            />
            <Input
                id='TournamentAdd-endDate'
                label='End Date'
                type='date'
                value={endDate}
                required
                onChange={(e) => setEndDate(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <Button value='Save' />
        </form>
    );
}