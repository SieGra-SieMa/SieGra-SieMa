import { FormEvent, useState } from 'react';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './TournamentAdd.module.css';

export default function TournamentAdd({ confirm }: { confirm: (tournament: Tournament) => void }) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [address, setAddress] = useState('');
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const tournament: Tournament = {
            name,
            startDate: startDate,
            endDate: endDate,
            description,
            address
        };
        tournamentsService.createTournament(tournament)
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
            <Button value='Add' />
        </form>
    );
}