import { useState } from 'react';
import { Tournament } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import Input from '../form/Input';
import styles from './TournamentAdd.module.css';

export default function TournamentAdd({ confirm }: { confirm: () => void }) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [address, setAddress] = useState('');
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

    const onSubmit = () => {
        const tournament: Tournament = {
            name,
            startDate: startDate,
            endDate: endDate,
            description,
            address
        };
        tournamentsService.createTournament(tournament)
            .then((data) => {
                confirm();
            })
    }

    return (
        <div className={styles.root}>
            <Input
                id='TournamentAdd-name'
                label='Name'
                value={name}
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
                onChange={(e) => setAddress(e.target.value)}
            />
            <Input
                id='TournamentAdd-startDate'
                label='Start Date'
                type='date'
                value={startDate}
                onChange={(e) => setStartDate(e.target.value)}
            />
            <Input
                id='TournamentAdd-endDate'
                label='End Date'
                type='date'
                value={endDate}
                onChange={(e) => setEndDate(e.target.value)}
            />
            <div className={styles.spacing}></div>
            <button onClick={onSubmit} className="button">Add</button>
        </div>
    );
}