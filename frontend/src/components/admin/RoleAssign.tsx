import { FormEvent, useEffect, useState } from 'react';
import { SyncLoader } from 'react-spinners';
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

export default function RoleAssign({ id, confirm }: TeamAssignProps) {

    const { usersService } = useApi();

    const [roles, setRoles] = useState<string[] | null>(null);

    const [selectedRole, setSelectedRole] = useState<string | null>(null);

    useEffect(() => {
        setRoles([ROLES.User, ROLES.Emp, ROLES.Admin]);
    }, [usersService]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedRole) return;
        usersService.addUserRole(id, [selectedRole])
            .then((data) => {
                confirm(data);
            });
    };

    return (
        <form className={styles.root} onSubmit={onSubmit}>
            {roles ? (
                <ul className={styles.list}>
                    {roles.map((role, index) => (
                        <li
                            key={index}
                            className={[
                                styles.item,
                                (selectedRole === role ? styles.selected : undefined)
                            ].filter((e) => e).join(' ')}
                            onClick={() => setSelectedRole(role)}
                        >
                            <p>
                                {role}
                            </p>
                        </li>
                    ))}
                </ul>
            ) : (
                <div className={styles.loader}>
                    <SyncLoader loading={true} size={7} margin={20} color='#fff' />
                </div>
            )}
            <VerticalSpacing size={15} />
            <Button className={styles.button} value='Dodaj' disabled={selectedRole === null} />
        </form>
    );
};