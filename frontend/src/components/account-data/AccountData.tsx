import { useState } from 'react';
import Button, { ButtonStyle } from '../form/Button';
import Modal from '../modal/Modal';
import { useUser } from '../user/UserContext';
import styles from './AccountData.module.css';
import AccountDataEdit from './AccountDataEdit';

export default function AccountData() {

    const { user, setUser } = useUser();

    const [isEdit, setIsEdit] = useState(false);

    return (
        <div className={styles.root}>
            <div className={styles.container}>
                <div className={styles.avatarBlock}>
                    <img src="http://localhost:3000/hero.jpeg" alt="" />
                </div>
                <div className={styles.dataBlock}>
                    <div className={styles.controls}>
                        <Button
                            value='Edit'
                            onClick={() => setIsEdit(true)}
                            style={ButtonStyle.DarkBlue}
                        />
                    </div>
                    <h2>{user ? `${user.name} ${user.surname}` : 'Username'}</h2>
                </div>
            </div>
            {user && isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title={'Edit user'}
                >
                    <AccountDataEdit confirm={(user) => {
                        setUser(user);
                        setIsEdit(false);
                    }} />
                </Modal>
            )}
        </div>
    );
}
