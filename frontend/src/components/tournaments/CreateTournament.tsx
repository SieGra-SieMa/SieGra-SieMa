import { FormEvent, useState } from 'react';
import { TournamentListItem, TournamentRequest } from '../../_lib/_types/tournament';
import { useApi } from '../api/ApiContext';
import Button from '../form/Button';
import DatePicker from '../form/DatePicker';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './CreateTournament.module.css';

type CreateTournamentType = {
    confirm: (tournament: TournamentListItem) => void;
};

export default function CreateTournament({ confirm }: CreateTournamentType) {

    const { tournamentsService } = useApi();

    const [name, setName] = useState('');
    const [address, setAddress] = useState('');
    const [startDate, setStartDate] = useState<Date | null>(null);
    const [endDate, setEndDate] = useState<Date | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        const tournament: TournamentRequest = {
            name,
            startDate: startDate!.toISOString(),
            endDate: endDate!.toISOString(),
            address,
        };
        tournamentsService.createTournament(tournament)
            .then((data) => {
                confirm(data);
            });
    }

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <Input
                id='TournamentAdd-name'
                label='Nazwa'
                value={name}
                required
                onChange={(e) => setName(e.target.value)}
            />
            <Input
                id='TournamentAdd-address'
                label='Adres'
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
                    if (new Date().getTime() - date.getTime() > 0) return false;
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
                    if (new Date().getTime() - date.getTime() > 0) return false;
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
            <Button value='Dodaj' />
        </form>
    );
}
