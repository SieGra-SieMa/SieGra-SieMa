import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Home.css';

export default function Home() {

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <section className="main-section">
            <div className="hero-section">
                <div className="container hero-container">
                    <div className="image-container hero-image">
                        <img src="http://localhost:3000/hero.png" alt="" />
                    </div>
                    <div className="hero-block-info">
                        <h2>Pasja pomagania</h2>
                        <p>
                            <i>
                                "W końcu nikt nie jest większy od koszykówki.
                                Ani Wilt, ani Doktor J, ani Michael, ani Larry,
                                ani Magic, ani LeBron. Nikt. Każdy z nas musiał
                                coś przezwyciężyć, żeby zajść tak daleko - swoje
                                pochodzenie, swoje ograniczenia, swoje wątpliwości.
                                Coś, co powstrzymało tak wielu innych i pewnie mogło
                                zatrzymać i nas, ale jednak nie zatrzymało."
                            </i><b> - Ray Allen</b>
                            <br /><br />
                            Każdy z nas ma przed sobą swoją drogę, a naszym celem jest pomoc
                            w jej przejściu. Dołącz do nas i poprzez pasję do koszykówki pomagaj razem z nami!
                        </p>
                    </div>
                </div>
            </div>
            <div className="dreamteam-section">
                <div className="container dreamteam-container">
                    <h2>Poznaj naszą drużynę!</h2>
                    <ul className="dreamteam-member-list">
                        <li className="dreamteam-member">
                            <div className="image-container dreamteam-member-image">
                                <img src="http://localhost:3000/card_kamila.png" alt="" />
                            </div>
                            <h4>Kamila</h4>
                        </li>
                        <li className="dreamteam-member">
                            <div className="image-container dreamteam-member-image">
                                <img src="http://localhost:3000/card_olek.png" alt="" />
                            </div>
                            <h4>Olek</h4>
                        </li>
                        <li className="dreamteam-member">
                            <div className="image-container dreamteam-member-image">
                                <img src="http://localhost:3000/card_mateusz.png" alt="" />
                            </div>
                            <h4>Mateusz</h4>
                        </li>
                    </ul>
                    <Link className='slide-button' to="/about-us">
                        Poznaj nas bliżej!
                    </Link>
                </div>
            </div>
            <div className="news-section">
                <h2>News feed</h2>
                <ul className="news-list">
                    <li className="news-post">

                    </li>
                    <li className="news-post">

                    </li>
                    <li className="news-post">

                    </li>
                </ul>
            </div>
        </section>
    );
}
