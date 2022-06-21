import { TeamInTournament } from '../../../_lib/_types/tournament';
import styles from './TeamsList.module.css';
import Button, { ButtonStyle } from '../../form/Button';
import { useState } from 'react';
import Modal from '../../modal/Modal';
import Confirm from '../../modal/Confirm';
import { useApi } from '../../api/ApiContext';
import { TeamPaidEnum } from '../../../_lib/types';
import { useTournament } from '../TournamentContext';
import GuardComponent from '../../guard-components/GuardComponent';
import { ROLES } from '../../../_lib/roles';

type TeamUnpaidProps = {
    team: TeamInTournament;
}

export default function TeamPaid({ team }: TeamUnpaidProps) {

    const { tournamentsService } = useApi();
    const { teams, setTeams } = useTournament();

    const [isChange, setIsChange] = useState(false);

    const updateTeamStatus = () => {

        tournamentsService.setTeamStatus(team.tournamentId, team.teamId, TeamPaidEnum.Unpaid)
            .then((data) => {
                const newTeams = teams!.filter((e) => e.teamId !== team.teamId);
                setTeams([...newTeams, data]);
                setIsChange(false);
            });

    }

    return (
        <li className={styles.team}>
            <p>
                {team.teamName}
            </p>
            <GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
                <Button value='Nie zapłacono' onClick={() => setIsChange(true)} />
                {(isChange) && (
                    <Modal
                        title={`Potwierdzenie anulowania opłaty - Zespół "${team.teamName}"`}
                        isClose
                        close={() => setIsChange(false)}
                    >
                        <Confirm
                            cancel={() => setIsChange(false)}
                            confirm={updateTeamStatus}
                            label='Potwierdź'
                            style={ButtonStyle.Yellow}
                        />
                    </Modal>
                )}
            </GuardComponent>
        </li>
    );
};