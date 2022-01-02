import { authenticationService } from '../../_services/authentication.service';
import styles from './AccountData.module.css';

export default function AccountData() {

    const user = authenticationService.currentUserValue;

    return (
        <div className={styles.root}>
            <div className={styles.container}>
                <div className={styles.avatarBlock}>
                    <img src="http://localhost:3000/hero.jpeg" alt="" />
                </div>
                <div className={styles.dataBlock}>
                    <div className={styles.controls}>
                        <button className={`${styles.editButton} button`}>
                            Edit
                        </button>
                    </div>
                    <h2>{user?.name} {user?.surname}</h2>
                </div>
            </div>
        </div>
    );
}
