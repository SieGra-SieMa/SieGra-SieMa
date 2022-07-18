import { FormEvent, useState } from 'react';
import { Player, Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button, { ButtonStyle } from '../../form/Button';
import Form from '../../form/Form';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import { useTeams } from '../TeamsContext';
import styles from './Players.module.css';


type Props = {
    team: Team;
    confirm: () => void;
}

export default function PlayersToSetCapitan({
    team,
    confirm,
}: Props) {

    const { teamsService } = useApi();
    const { teams, setTeams } = useTeams();

    const [selectedPlayer, setSelectedPlayer] = useState<Player | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedPlayer) return;
        return teamsService.switchCaptain(team.id, selectedPlayer.id)
            .then((team) => {
                const data = teams ? [...teams] : [];
                const index = data.findIndex(e => e.id === team.id) ?? -1;
                if (index >= 0) {
                    data[index] = team;
                    setTeams(data);
                }
                confirm();
            });
    }

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button
                className={styles.button}
                value='Wybierz'
                disabled={selectedPlayer === null}
                style={ButtonStyle.Red}
            />
        </>}>
            <ul className={styles.list}>
                {team.players.filter((player) => player.id !== team.captainId)
                    .map((player, index) => (
                        <li
                            key={index}
                            className={[
                                styles.item,
                                (selectedPlayer === player ? styles.selected : undefined)
                            ].filter((e) => e).join(' ')}
                            onClick={() => setSelectedPlayer(player)}
                        >
                            <p>{`${player.name} ${player.surname}`}</p>
                        </li>
                    ))}
            </ul>
        </Form >
    );
}
