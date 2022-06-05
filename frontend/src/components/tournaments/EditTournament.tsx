import { FormEvent, useState } from 'react';
import { Tournament, TournamentRequest } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './EditTournament.module.css';

type EditTournamentProps = {
    tournament: Tournament;
    confirm: (tournament: Tournament) => void;
}

export default function EditTournament({
    tournament,
    confirm,
}: EditTournamentProps) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState(tournament.name);
    const [address, setAddress] = useState(tournament.address);

    const [startDate, setStartDate] = useState(new Date(tournament.startDate).toISOString().split('T')[0]);
    const [endDate, setEndDate] = useState(new Date(tournament.endDate).toISOString().split('T')[0]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedTournament: TournamentRequest = {
            name,
            startDate,
            endDate,
            address
        };
        tournamentsService.updateTournament(tournament.id, updatedTournament)
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