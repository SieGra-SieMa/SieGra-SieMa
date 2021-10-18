import React from 'react';
import './Home.css';

export default function Home() {
    return (
        <div className="content-section">
            <div className="container hero-container">
                <div className="img-container">
                    {/* <img id="heroImg" src="http://localhost:3000/hero.jpeg" alt="hero"/> */}
                </div>
                <div className="hero-block-info">
                    <h2>New tournament</h2>
                    <h3>Take part in a new tournament, join now, create team and invite your friends.</h3>
                    <br/>
                    <p>Together more fun.</p>
                </div>
            </div>
        </div>
    );
}
