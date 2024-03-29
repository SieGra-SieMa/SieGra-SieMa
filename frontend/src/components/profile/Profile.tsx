import { useState } from 'react';
import Button, { ButtonStyle } from '../form/Button';
import { useAuth } from '../auth/AuthContext';
import Modal from '../modal/Modal';
import { useUser } from '../user/UserContext';
import styles from './Profile.module.css';
import EditProfile from './EditProfile';
import AccountPasswordChange from './PasswordChange';
import Confirm from '../modal/Confirm';
import { useApi } from '../api/ApiContext';
import { useAlert } from '../alert/AlertContext';

export default function Profile() {

    const alert = useAlert();
    const { setSession } = useAuth();
    const { user, setUser } = useUser();
    const { usersService } = useApi();

    const [isEdit, setIsEdit] = useState(false);
    const [isNewsletterJoin, setIsNewsletterJoin] = useState(false);
    const [isNewsletterLeave, setIsNewsletterLeave] = useState(false);
    const [isPasswordChange, setIsPasswordChange] = useState(false);
    const [isAccountDelete, setIsAccountDetele] = useState(false);

    const joinNewsletter = () => {
        return usersService.joinNewsletter()
            .then((data) => {
                setUser(data);
                setIsNewsletterJoin(false);
            });
    }

    const leaveNewsletter = () => {
        return usersService.leaveNewsletter()
            .then((data) => {
                setUser(data);
                setIsNewsletterLeave(false);
            });
    }

    const deleteAccount = () => {
        return usersService.deleteAccount()
            .then(() => {
                setIsAccountDetele(false);
                setSession(null);
                alert.success('Konto zostało zablokowane');
            });
    }

    return (
        <div className={[
            'container',
            styles.root,
        ].join(' ')}>
            <div className={styles.container}>
                <h1>Profil</h1>
                <h4>{user ? `${user.name} ${user.surname}` : 'Użytkownik'}</h4>
                <div className={styles.controls}>
                    <Button
                        value='Edytuj użytkownika'
                        onClick={() => setIsEdit(true)}
                        style={ButtonStyle.DarkBlue}
                    />
                    {user && (
                        user.newsletter ? (
                            <Button
                                value='Zrezygnuj z newslettera'
                                onClick={() => setIsNewsletterLeave(true)}
                                style={ButtonStyle.Red}
                            />
                        ) : (
                            <Button
                                value='Dolącz do newslettera'
                                onClick={() => setIsNewsletterJoin(true)}
                            />
                        )
                    )}
                    <Button
                        value='Zmień hasło'
                        onClick={() => setIsPasswordChange(true)}
                        style={ButtonStyle.Red}
                    />
                    <Button
                        value='Usuń konto'
                        onClick={() => setIsAccountDetele(true)}
                        style={ButtonStyle.Red}
                    />
                </div>
            </div>
            {user && isEdit && (
                <Modal
                    isClose
                    close={() => setIsEdit(false)}
                    title={'Edytuj użytkownika'}
                >
                    <EditProfile confirm={() => {
                        setIsEdit(false);
                    }} />
                </Modal>
            )}
            {isPasswordChange && (
                <Modal
                    isClose
                    close={() => setIsPasswordChange(false)}
                    title={`Zmiana hasła`}
                >
                    <AccountPasswordChange confirm={() => {
                        setIsPasswordChange(false);
                        setSession(null);
                    }} />
                </Modal>
            )}
            {isNewsletterJoin && (
                <Modal
                    close={() => setIsNewsletterJoin(false)}
                    title="Czy na pewno chcesz dołączyć do newslettera?"
                >
                    <Confirm
                        cancel={() => setIsNewsletterJoin(false)}
                        confirm={joinNewsletter}
                        label='Potwierdź'
                        style={ButtonStyle.Yellow}
                    />
                </Modal>
            )}
            {isNewsletterLeave && (
                <Modal
                    close={() => setIsNewsletterLeave(false)}
                    title="Czy na pewno chcesz zrezygnować z newslettera?"
                >
                    <Confirm
                        cancel={() => setIsNewsletterLeave(false)}
                        confirm={leaveNewsletter}
                        label='Potwierdź'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
            {isAccountDelete && (
                <Modal
                    close={() => setIsAccountDetele(false)}
                    title="Czy na pewno chcesz usunąć konto?"
                >
                    <Confirm
                        cancel={() => setIsAccountDetele(false)}
                        confirm={deleteAccount}
                        label='Potwierdź'
                        style={ButtonStyle.Red}
                    />
                </Modal>
            )}
        </div>
    );
}
