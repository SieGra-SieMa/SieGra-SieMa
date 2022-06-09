import { Link } from 'react-router-dom';
import styles from './Footer.module.css';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import Facebook from '@mui/icons-material/Facebook';
import Instagram from '@mui/icons-material/Instagram';

export default function Footer() {
    return (
        <div className={styles.footerSection}>
            <div className={styles.footerContainer}>
                <div className={styles.contact}>
                    <div className={styles.iconText}><PhoneIcon /> <p>796 688 795</p></div>
                    <div className={styles.iconText}><EmailIcon /> <p>siegrasiema.inicjatywa@gmail.com</p></div>
                </div>
                <div className={styles.links}>
                    <a className="slide-button" href="mailto:siegrasiema.inicjatywa@gmail.com"><p>Get in touch</p></a>
                    <div className={styles.social}>
                        <a href="https://www.facebook.com/siegrasiema.inicjatywa/" target="_blank" rel="noreferrer"><Facebook/></a>
                        <a href="https://www.instagram.com/siegrasiema.inicjatywa/" target="_blank" rel="noreferrer"><Instagram/></a>
                    </div>
                </div>
                <Link className={styles.logo} to="/">
                    <img className={styles.footerIcon} src="/logo_w.png" alt="" />
                </Link>
            </div>
            <p id={styles.designedBy}>
                Designed & created by Jakub Adamczyk, Jan Biniek, Taras Kulyavets, Jakub Pawłowicz
            </p>
        </div>
    );
}