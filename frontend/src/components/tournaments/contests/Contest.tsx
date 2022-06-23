import { useState } from 'react';
import { ROLES } from '../../../_lib/roles';
import { Contest as ContestType } from '../../../_lib/_types/tournament';
import Button from '../../form/Button';
import GuardComponent from '../../guard-components/GuardComponent';
import Modal from '../../modal/Modal';
import AddScoreContest from './AddScoreContest';

type ContestProps = {
    contest: ContestType;
};

export default function Contest({ contest }: ContestProps) {
    const [currentContest, setCurrentContest] = useState(contest);

    const [isAddScore, setIsAddScore] = useState(false)

    return (
        <div>
            <p>{contest.name}</p>
            <div>
                {contest.contestants.map((player) => (
                    <p key={player.userId}>
                        {player.name} {player.surname} - {player.points}
                    </p>
                ))}
            </div>
            <GuardComponent roles={[ROLES.Employee, ROLES.Admin]}>
                <Button value='Dodaj punkty' onClick={() => setIsAddScore(true)} />
                {(isAddScore) && (
                    <Modal
                        title={`Konkurs - "${contest.name}"`}
                        isClose
                        close={() => setIsAddScore(false)}
                    >
                        <AddScoreContest
                            contest={contest}
                            confirm={() => {
                                setIsAddScore(false);
                            }}
                        />
                    </Modal>
                )}
            </GuardComponent>
        </div>
    );
}
