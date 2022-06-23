import { useState } from 'react';
import { ROLES } from '../../../_lib/roles';
import { Contest as ContestType } from '../../../_lib/_types/tournament';
import Button from '../../form/Button';
import GuardComponent from '../../guard-components/GuardComponent';
import Modal from '../../modal/Modal';
import styles from './Contests.module.css';
import Contest from './Contest';
import CreateContest from './CreateContest';
import { useTournament } from '../TournamentContext';

type ContestsProps = {
    contests: ContestType[];
};

export default function Contests({ contests }: ContestsProps) {

    const { tournament } = useTournament();

    const [isAddContest, setIsAddContest] = useState(false);

    return (
        <div>
            <div className={styles.header}>
                <h4>Konkursy</h4>
                <GuardComponent roles={[ROLES.Admin]}>
                    <Button
                        value='Dodaj konkurs'
                        onClick={() => setIsAddContest(true)}
                    />
                </GuardComponent>
            </div>
            <ul className={styles.contests}>
                {contests.map((contest) => (
                    <Contest key={contest.id} contest={contest} />
                ))}
            </ul>
            {(isAddContest) && (
                <Modal
                    title='Dodanie konkursu'
                    isClose
                    close={() => setIsAddContest(false)}
                >
                    <CreateContest
                        tournamentId={tournament!.id}
                        confirm={() => {
                            setIsAddContest(false);
                        }}
                    />
                </Modal>
            )}
        </div>
    );
};
