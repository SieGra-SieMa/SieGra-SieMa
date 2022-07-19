import { FormEvent, useState } from 'react';
import { User } from '../../../_lib/types';
import { useApi } from '../../api/ApiContext';
import { ROLES } from '../../../_lib/roles';
import Button from '../../form/Button';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './RoleAssign.module.css';
import Form from '../../form/Form';


type Props = {
    user: User;
    confirm: (user: User) => void;
};

export default function RoleAssign({ user, confirm }: Props) {

    const { usersService } = useApi();

    const [selectedRoles, setSelectedRoles] = useState<string[]>([]);

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedRoles.length) return;
        return usersService.addUserRole(user.id, selectedRoles)
            .then((data) => {
                confirm(data);
            });
    };

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button
                className={styles.button}
                value='Dodaj'
                disabled={!selectedRoles.length}
            />
        </>}>
            <ul className={styles.list}>
                {Object.values(ROLES).filter((role) => !user.roles.includes(role))
                    .map((role, index) => (
                        <li
                            key={index}
                            className={[
                                styles.item,
                                (selectedRoles.includes(role) ? styles.selected : undefined)
                            ].filter((e) => e).join(' ')}
                            onClick={() => {
                                const roles = selectedRoles.includes(role) ?
                                    selectedRoles.filter((e) => e !== role) :
                                    [...selectedRoles, role];
                                setSelectedRoles(roles);
                            }}
                        >
                            <p>
                                {role}
                            </p>
                        </li>
                    ))}
            </ul>
        </Form >
    );
};