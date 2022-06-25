import { FormEvent, useState } from 'react';
import { Player, Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button, { ButtonStyle } from '../../form/Button';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import { useTeams } from '../TeamsContext';
import styles from './Players.module.css';

type PlayersToRemoveProps = {
    team: Team;
    confirm: () => void;
}

export default function PlayersToRemove({
    team,
    confirm,
}: PlayersToRemoveProps) {

    const { teamsService } = useApi();
    const { teams, setTeams } = useTeams();

    const [selectedPlayer, setSelectedPlayer] = useState<Player | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedPlayer) return;
        teamsService.removePlayer(team.id, selectedPlayer.id)
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
        <form className={styles.root} onSubmit={onSubmit}>
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
            <VerticalSpacing size={15} />
            <Button
                className={styles.button}
                value='Usuń'
                disabled={selectedPlayer === null}
                style={ButtonStyle.Red}
            />
        </form >

    );
}
