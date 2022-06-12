import styles from './UsersListItem.module.css';
import { User } from '../../_lib/types';
import { useCallback, useState } from 'react';
import Modal from '../modal/Modal';
import Confirm from '../modal/Confirm';
import { useApi } from '../api/ApiContext';
import Button, { ButtonStyle } from '../form/Button';
import RoleAssign from './RoleAssign';

type UsersListItemProp = {
    user: User,
    onUserPropChange: (user: User) => void,
};

export default function UserListItem({ user, onUserPropChange }: UsersListItemProp) {

    const [isAdd, setIsAdd] = useState(false);
    const [isRemove, setIsRemove] = useState(false);
    const [chosenRole, setChosenRole] = useState<string | null>(null);

    const removeRole = (role: string) => {
        usersService.removeUserRole(user.id, [role])
            .then((data) => {
                setIsRemove(false);
                onUserPropChange(data);
            });
    };

    const { usersService } = useApi();


    return (
        <div className={styles.root}>
            <div className={styles.content}>
                <h3>{user.name} {user.surname}
                    <Button
                        value='Edit'
                        onClick={() => { console.log('abc') }}
                    />
                    <Button
                        value='Delete'
                        onClick={() => { console.log('abc') }}
                        style={ButtonStyle.Red}
                    />
                </h3>
                <div className={styles.codeBlock}>
                    <span>Roles: </span>
                    <Button
                        value='Add'
                        onClick={() => { setIsAdd(true) }}
                        style={ButtonStyle.DarkBlue}
                    />
                </div>
                <ul>
                    {user.roles!.map((role, index) => (
                        <li
                            key={index}
                        >
                            <div className={styles.teamMember}>
                                <p>{`${role}`}</p>
                                <>
                                    <Button
                                        value='Remove'
                                        onClick={() => { setChosenRole(role); setIsRemove(true) }}
                                        style={ButtonStyle.Red}
                                    />
                                </>
                            </div>

                        </li>
                    ))}
                </ul>
            </div>
            {isRemove && (
                <Modal
                    close={() => setIsRemove(false)}
                    title={`Do you really want to remove role?`}
                >
                    <Confirm
                        cancel={() => setIsRemove(false)}
                        confirm={() => removeRole(chosenRole!)}
                        label='Remove'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
            {(isAdd) && (
                <Modal
                    title='Dodaj role'
                    isClose
                    close={() => setIsAdd(false)}
                >
                    <RoleAssign
                        id={user.id}
                        confirm={(user) => { onUserPropChange(user); setIsAdd(false) }}
                    />
                </Modal>
            )}
        </div>
    );
};
