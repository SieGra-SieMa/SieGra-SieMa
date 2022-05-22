import Button, { ButtonStyle } from '../form/Button';
import { useUser } from '../user/UserContext';
import styles from './AccountData.module.css';

export default function AccountData() {

    const { user } = useUser();

    return (
        <div className={styles.root}>
            <div className={styles.container}>
                <div className={styles.avatarBlock}>
                    <img src="http://localhost:3000/hero.jpeg" alt="" />
                </div>
                <div className={styles.dataBlock}>
                    <div className={styles.controls}>
                        <Button value='Edit' style={ButtonStyle.DarkBlue} />
                    </div>
                    <h2>{user ? `${user.name} ${user.surname}` : 'Username'}</h2>
                </div>
            </div>
        </div>
    );
}
