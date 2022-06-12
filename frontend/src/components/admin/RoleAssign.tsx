import { FormEvent, useState } from 'react';
import { User } from '../../_lib/types';
import { useApi } from '../api/ApiContext';
import { ROLES } from '../../_lib/roles';
import Button from '../form/Button';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './RoleAssign.module.css';

type TeamAssignProps = {
    id: number;
    confirm: (user: User) => void;
};

const roles = [ROLES.User, ROLES.Employee, ROLES.Admin];

export default function RoleAssign({ id, confirm }: TeamAssignProps) {

    const { usersService } = useApi();

    const [selectedRoles, setSelectedRoles] = useState<string[]>([]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedRoles.length) return;
        usersService.addUserRole(id, selectedRoles)
            .then((data) => {
                confirm(data);
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            <ul className={styles.list}>
                {roles.map((role, index) => (
                    <li
                        key={index}
                        className={[
                            styles.item,
                            (selectedRoles.includes(role) ? styles.selected : undefined)
                        ].filter((e) => e).join(' ')}
                        onClick={() => {
                            selectedRoles.includes(role) ?
                                setSelectedRoles(selectedRoles.filter((e) => e !== role)) :
                                setSelectedRoles([...selectedRoles, role]);
                        }}
                    >
                        <p>
                            {role}
                        </p>
                    </li>
                ))}
            </ul>
            <VerticalSpacing size={15} />
            <Button className={styles.button} value='Dodaj' disabled={!selectedRoles.length} />
        </form >
    );
};