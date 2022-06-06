import { FormEvent, useState } from 'react';
import { Tournament, TournamentRequest } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import DatePicker from '../form/DatePicker';
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

    const [startDate, setStartDate] = useState<Date | null>(new Date(tournament.startDate));
    const [endDate, setEndDate] = useState<Date | null>(new Date(tournament.endDate));

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const updatedTournament: TournamentRequest = {
            name,
            startDate: startDate!.toISOString(),
            endDate: endDate!.toISOString(),
            address,
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
            <DatePicker
                id='DatePicker-startDate'
                label='Czas rozpoczęcia'
                date={startDate}
                onChange={(date) => setStartDate(date)}
                filterDate={(date) => {
                    if (!endDate) return true;
                    if (date.getFullYear() - endDate.getFullYear() > 0) return false;
                    if (date.getFullYear() - endDate.getFullYear() < 0) return true;
                    if (date.getMonth() - endDate.getMonth() > 0) return false;
                    if (date.getMonth() - endDate.getMonth() < 0) return true;
                    return date.getDate() - endDate.getDate() <= 0;
                }}
                filterTime={(date) => endDate ? date.getTime() - endDate.getTime() <= 0 : true}
            />
            <DatePicker
                id='DatePicker-endDate'
                label='Czas końca'
                date={endDate}
                onChange={(date) => setEndDate(date)}
                filterDate={(date) => {
                    if (!startDate) return true;
                    if (date.getFullYear() - startDate.getFullYear() < 0) return false;
                    if (date.getFullYear() - startDate.getFullYear() > 0) return true;
                    if (date.getMonth() - startDate.getMonth() < 0) return false;
                    if (date.getMonth() - startDate.getMonth() > 0) return true;
                    return date.getDate() - startDate.getDate() >= 0;
                }}
                filterTime={(date) => startDate ? date.getTime() - startDate.getTime() >= 0 : true}
            />
            <VerticalSpacing size={15} />
            <Button value='Zatwierdź' />
        </form>
    );
}