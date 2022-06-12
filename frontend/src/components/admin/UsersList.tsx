import styles from './UsersList.module.css';
import UsersListItem from './UsersListItem';
import { useState } from 'react';
import { useEffect } from 'react';
import { User } from '../../_lib/types';
import SyncLoader from 'react-spinners/SyncLoader';
import { useApi } from '../api/ApiContext';

export default function UsersList() {

    const { usersService } = useApi();

    const [users, setUsers] = useState<User[] | null>(null);

    useEffect(() => {
        usersService.getUsers()
            .then(
                result => setUsers(result),
                error => alert(error)
            );
    }, [usersService]);

    const onUserPropChange = (user: User) => {
        const data = users ? [...users] : [];
        const index = data.findIndex(e => e.id === user.id) ?? -1;
        if (index >= 0) {
            data[index] = user;
            setUsers(data);
        }
    };

    return (
        <div className="container">
            <h2>Users:</h2>
            <div className={styles.list}>
                {users ? users.map((user, index) => (
                    <UsersListItem
                        key={index}
                        user={user}
                        onUserPropChange={onUserPropChange}
                    />
                )) : (
                    <div className={styles.loader}>
                        <SyncLoader loading={true} size={20} margin={20} color='#fff' />
                    </div>
                )}
            </div>
        </div>
    );
}
