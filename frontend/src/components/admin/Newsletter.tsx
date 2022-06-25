import Button, { ButtonStyle } from '../form/Button';
import styles from './AdminPanel.module.css';
import './Newsletter.css';
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { useEffect, useState } from 'react';
import Input from '../form/Input';
import VerticalSpacing from '../spacing/VerticalSpacing';
import Modal from '../modal/Modal';
import Confirm from '../modal/Confirm';
import { useApi } from '../api/ApiContext';
import { Tournament } from '../../_lib/_types/tournament';

export default function Newsletter() {

    const { usersService, tournamentsService } = useApi();

    const [title, setTitle] = useState('');
    const [body, setBody] = useState('');
    const [tournamentId, setTournamentId] = useState('');

    const [isConfirmation, setIsConfirmation] = useState(false);

    const [tournaments, setTournamnets] = useState<Tournament[] | null>(null);

    useEffect(() => {
        tournamentsService.getTournaments()
            .then((data) => setTournamnets(data));
    }, [tournamentsService]);

    console.log(tournamentId)

    const onConfirm = () => {
        usersService.sendNewsletter(title, body, tournamentId ?? undefined)
            .then(
                (data) => {
                    alert(data.message);
                    setIsConfirmation(false);
                    setTitle('');
                    setBody('');
                },
                (error) => {
                    alert(error);
                }
            )
    };

    return (
        <div>
            <h1>Newsletter</h1>
            <Input
                label='Tytuł'
                value={title}
                onChange={(e) => setTitle(e.target.value)} />
            <VerticalSpacing size={15} />
            <ReactQuill
                theme="snow"
                value={body}
                onChange={setBody}
            />
            <VerticalSpacing size={15} />
            {(tournaments) && (<>
                <p>Odbiorca:</p>
                <VerticalSpacing size={5} />
                <select className={styles.select} onChange={(e) => setTournamentId(e.target.value)}>
                    <option selected value=''>Do wszystkich</option>
                    {tournaments.map((tournament) => (
                        <option value={tournament.id}>Do graczy w {tournament.name}</option>
                    ))}
                </select>
            </>)}
            <VerticalSpacing size={15} />
            <Button
                value='Wyślij'
                onClick={() => setIsConfirmation(true)}
            />
            {(isConfirmation) && (
                <Modal
                    isClose
                    title='Czy na pewno chcesz wysłać?'
                    close={() => setIsConfirmation(false)}>
                    <Confirm
                        label='Potwierdź'
                        cancel={() => setIsConfirmation(false)}
                        confirm={onConfirm}
                        style={ButtonStyle.Yellow}
                    />
                </Modal>
            )}
        </div >
    );
}
