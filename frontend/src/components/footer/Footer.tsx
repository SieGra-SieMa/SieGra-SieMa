import { Link } from 'react-router-dom';
import './Footer.css';

export default function Footer() {
    return (
        <div className="footer-section">
            <div className="container">
                <div className="footer-container">
                    <div className="footer-block">
                        <Link to="/">
                            <img className="footer-icon" src="/logo.png" alt="" />
                        </Link>
                    </div>
                    <div className="footer-block">
                        <a className="contact-button" href="mailto:siegrasiema.inicjatywa@gmail.com"><h3>Get in touch</h3></a>
                    </div>
                    <div className="footer-block">
                        <div>Phone: 796 688 795</div>
                        <div>Email: siegrasiema.inicjatywa@gmail.com</div>
                    </div>
                </div>
            </div>
        </div>
    );
}