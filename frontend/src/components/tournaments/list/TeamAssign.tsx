import { FormEvent, useState } from 'react';
import { SyncLoader } from 'react-spinners';
import { Team } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './TeamAssign.module.css';


type Props = {
    id: number;
    confirm: (team: Team) => void;
    teams: Team[] | null;
};

export default function TeamAssign({ id, confirm, teams }: Props) {

    const { tournamentsService } = useApi();

    const [selectedTeam, setSelectedTeam] = useState<Team | null>(null);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedTeam) return;
        return tournamentsService.addTeam(id, selectedTeam.id)
            .then((data) => {
                confirm(selectedTeam);
            });
    };

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button
                className={styles.button}
                value='Zapisz'
                disabled={selectedTeam === null}
            />
        </>}>
            {teams ? (
                <ul className={styles.list}>
                    {teams.map((team, index) => (
                        <li
                            key={index}
                            className={[
                                styles.item,
                                (selectedTeam === team ? styles.selected : undefined)
                            ].filter((e) => e).join(' ')}
                            onClick={() => setSelectedTeam(team)}
                        >
                            <p>
                                {team.name}
                            </p>
                        </li>
                    ))}
                </ul>
            ) : (
                <div className={styles.loader}>
                    <SyncLoader loading={true} size={7} margin={20} color='#fff' />
                </div>
            )}
        </Form>
    );
};
