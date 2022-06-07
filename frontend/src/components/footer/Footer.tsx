import { Link } from 'react-router-dom';
import styles from './Footer.module.css';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';

export default function Footer() {
    return (
        <div className={styles.footerSection}>
            <div className={styles.container}>
                <div className={styles.footerContainer}>
                    <div className={styles.footerBlock}>
                        <Link to="/">
                            <img className={styles.footerIcon} src="/logo_w.png" alt="" />
                        </Link>
                    </div>
                    <div className={styles.footerBlock}>
                        <a className="slide-button" href="mailto:siegrasiema.inicjatywa@gmail.com"><p>Get in touch</p></a>
                    </div>
                    <div className={styles.footerBlock}>
                        <div className={styles.iconText}><PhoneIcon/> <p>796 688 795</p></div>
                        <div className={styles.iconText}><EmailIcon/> <p>siegrasiema.inicjatywa@gmail.com</p></div>
                    </div>
                </div>
                <p id={styles.designedBy}>
                    Designed & created by Jakub Adamczyk, Jan Biniek, Taras Kulyavets, Jakub Pawłowicz
                </p>
            </div>
        </div>
    );
}