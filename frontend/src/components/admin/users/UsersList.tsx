import styles from './UsersList.module.css';
import UsersListItem from './UsersListItem';
import { useState, useEffect } from 'react';
import { User } from '../../../_lib/types';
import SyncLoader from 'react-spinners/SyncLoader';
import { useApi } from '../../api/ApiContext';
import { useUser } from '../../user/UserContext';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';

export default function UsersList() {

    const { usersService } = useApi();
    const { user } = useUser();

    const [users, setUsers] = useState<User[] | null>(null);
    const [search, setSearch] = useState('');

    useEffect(() => {
        if (!user) return;
        usersService.getUsers()
            .then(
                result => setUsers(result.filter((e) => user.id !== e.id)),
                error => alert(error)
            );
    }, [user, usersService]);

    const onUserPropChange = (user: User) => {
        const data = users ? [...users] : [];
        const index = data.findIndex(e => e.id === user.id);
        console.log(data, index, user)
        if (index >= 0) {
            data[index] = user;
            setUsers(data);
        }
    };

    return (
        <>
            <h1>Użytkownicy</h1>
            <Input
                placeholder='Wyszukaj...'
                value={search}
                onChange={(e) => setSearch(e.target.value)}
            />
            <VerticalSpacing size={15} />
            <div className={styles.content}>
                {users ? users.filter((user) => {
                    return (
                        user.name.toLowerCase().includes(search.toLowerCase()) ||
                        user.surname.toLowerCase().includes(search.toLowerCase()) ||
                        user.email.toLowerCase().includes(search.toLowerCase())
                    )
                }).map((user, index) => (
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
        </>
    );
}
