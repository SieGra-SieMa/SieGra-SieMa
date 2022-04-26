import { useEffect } from 'react';
import styles from './AccountEntry.module.css';
import CreateAccount from './CreateAccount';
import SignIn from './SignIn';

export default function AccountEntry() {

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <div className={`container ${styles.root}`}>
            <SignIn />
            <span>OR</span>
            <CreateAccount />
        </div>
    );
}
