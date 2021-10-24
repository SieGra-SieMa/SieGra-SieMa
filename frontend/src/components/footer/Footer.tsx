import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Footer.css';



export default function Footer(){
    const location = useLocation();
    return(<>
        <div className="footer-wave">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320" style={{backgroundColor: location.pathname === '/' ? '#5F7B88' : '#fff'}}><path fill="#3e555f" fillOpacity="1" d="M0,256L80,229.3C160,203,320,149,480,122.7C640,96,800,96,960,106.7C1120,117,1280,139,1360,149.3L1440,160L1440,320L1360,320C1280,320,1120,320,960,320C800,320,640,320,480,320C320,320,160,320,80,320L0,320Z"></path></svg>
        </div>
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
            </>
    );
}