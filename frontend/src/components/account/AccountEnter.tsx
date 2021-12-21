import styles from './AccountEnter.module.css';
import CreateAccount from './CreateAccount';
import SignIn from './SignIn';

export default function AccountEnter() {
    return (
        <div className={styles.root}>
            <SignIn />
            <span>OR</span>
            <CreateAccount />
        </div>
    );
}
