import { Link } from 'react-router-dom';
import './Footer.css';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';

export default function Footer() {
    return (
        <div className="footer-section">
            <div className="container">
                <div className="footer-container">
                    <div className="footer-block">
                        <Link to="/">
                            <img className="footer-icon" src="/logo_w.png" alt="" />
                        </Link>
                    </div>
                    <div className="footer-block">
                        <a className="slide-button" href="mailto:siegrasiema.inicjatywa@gmail.com"><p>Get in touch</p></a>
                    </div>
                    <div className="footer-block">
                        <div className='icon-text'><PhoneIcon/> <p>796 688 795</p></div>
                        <div className='icon-text'><EmailIcon/> <p>siegrasiema.inicjatywa@gmail.com</p></div>
                    </div>
                </div>
                <p id='designed-by'>
                    Designed & created by Jakub Adamczyk, Jan Biniek, Taras Kulyavets, Jakub Pawłowicz
                </p>
            </div>
        </div>
    );
}