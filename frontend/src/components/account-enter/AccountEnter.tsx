import { useEffect } from 'react';
import styles from './AccountEnter.module.css';
import CreateAccount from './CreateAccount';
import SignIn from './SignIn';

export default function AccountEnter() {

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <div className={styles.root}>
            <SignIn />
            <span>OR</span>
            <CreateAccount />
        </div>
    );
}
