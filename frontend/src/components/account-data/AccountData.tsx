import { useState } from 'react';
import Button, { ButtonStyle } from '../form/Button';
import { useAuth } from '../auth/AuthContext';
import Modal from '../modal/Modal';
import { useUser } from '../user/UserContext';
import styles from './AccountData.module.css';
import AccountDataEdit from './AccountDataEdit';
import AccountPasswordChange from './AccountPasswordChange';
import Confirm from '../modal/Confirm';
import { useApi } from '../api/ApiContext';



export default function AccountData() {

    const { user, setUser } = useUser();
    const { setSession } = useAuth();
    const { usersService } = useApi();

    const [isEdit, setIsEdit] = useState(false);
    const [isConfirm, setIsConfirm] = useState(false);
    const [isChanged, setIsChanged] = useState(false);

    const join = () => {
        usersService.joinNewsletter();
        setIsConfirm(false);
    }

    // const leave = () => {
    //     usersService.leaveNewsletter();
    // }

    return (
        <div className={styles.root}>
            <div className={styles.container}>
                <div className={styles.controls}>
                    <Button
                        value='Edytuj użytkownika'
                        onClick={() => setIsEdit(true)}
                        style={ButtonStyle.DarkBlue}
                    />
                </div>
                <h2>{user ? `${user.name} ${user.surname}` : 'Username'}</h2>
                <div className={styles.userButtons}>
                    <Button
                        value='Zmień hasło'
                        onClick={() => setIsChanged(true)}
                        style={ButtonStyle.Red}
                    />
                    <Button
                        value='Dolącz do newslettera'
                        onClick={() => setIsConfirm(true)}
                    />
                </div>
            </div>
            {user && isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title={'Edytuj użytkownika'}
                >
                    <AccountDataEdit confirm={(user) => {
                        setUser(user);
                        setIsEdit(false);
                    }} />
                </Modal>
            )}
            {isChanged && (
                <Modal
                    isClose
                    close={() => setIsChanged(false)}
                    title={`Zmiana hasła`}
                >
                    <AccountPasswordChange confirm={() => {
                        setIsEdit(false);
                        setSession(null);
                    }} />
                </Modal>
            )}
            {isConfirm && (
                <Modal
                    close={() => setIsConfirm(false)}
                    title={`Czy na pewno chcesz dołączyć do newsslettera?`}
                >
                    <Confirm
                        cancel={() => setIsConfirm(false)}
                        confirm={() => join()}
                        label='Potwierdź'
                        style={ButtonStyle.Yellow}
                    />
                </Modal>
            )}
        </div>
    );
}
