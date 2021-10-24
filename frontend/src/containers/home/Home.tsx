import React from 'react';
import { Link } from 'react-router-dom';
import './Home.css';

export default function Home() {
    return (
        <section className="main-section">
            <div className="hero-wave">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320" style={{backgroundColor: 'white'}}><path fill="#5F7B88" fillOpacity="1" d="M0,256L80,256C160,256,320,256,480,218.7C640,181,800,107,960,101.3C1120,96,1280,160,1360,192L1440,224L1440,320L1360,320C1280,320,1120,320,960,320C800,320,640,320,480,320C320,320,160,320,80,320L0,320Z"></path></svg>
            </div>
            <div className="hero-section">
                <div className="container hero-container">
                    <div className="image-container hero-image">
                        <img src="http://localhost:3000/hero.jpeg" alt="" />
                    </div>
                    <div className="hero-block-info">
                        <h2>About us</h2>
                        <h3>Take part in a new tournament, join now, create team and invite your friends.</h3>
                        <br/>
                        <p>Together more fun.</p>
                    </div>
                </div>
            </div>
            <div className="dreamteam-wave">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320" style={{backgroundColor: '#5F7B88'}}><path fill="#E3A084" fillOpacity="1" d="M0,256L80,229.3C160,203,320,149,480,122.7C640,96,800,96,960,106.7C1120,117,1280,139,1360,149.3L1440,160L1440,320L1360,320C1280,320,1120,320,960,320C800,320,640,320,480,320C320,320,160,320,80,320L0,320Z"></path></svg>
            </div>
            <div className="dreamteam-section">
                <div className="container">
                    <h2>Meet our dream team!</h2>
                    <ul className="dreamteam-member-list">
                        <li className="dreamteam-member">
                            <div className="image-container dreamteam-member-image">
                                <img src="http://localhost:3000/hero.jpeg" alt="" />
                            </div>
                            <h3>Kamila</h3>
                            <p>
                                Pierwszy raz w projekcie społecznym wzięłam udział 
                                przeszło 4 lata temu. Od tego czasu wraz z moimi 
                                znajomymi zrealizowaliśmy kilka autorskich inicjatyw 
                                (m.in. „Mój pierwszy raz” oraz „Warszawiacy”), które 
                                znacząco wpłynęły na mój rozwój. Nieodłączna częścią 
                                mojego życia jest rysunek, dzięki któremu mogę dać 
                                upust mojej wyobrazi i kreatywności oraz podróże, które 
                                pozwalają mi poznawać nowych ludzi i być otwartą na 
                                otaczający świat. Jednak to właśnie zainteresowanie 
                                sportem, które rozpoczęło się od uczęszczania do klasy 
                                pływackiej skłoniło mnie ku pójściu dalej oraz 
                                przyłączeniu się do inicjatywy społecznej, która daje mi 
                                możliwość połączenia mojego hobby z pomocą 
                                potrzebującym.
                            </p>
                        </li>
                        <li className="dreamteam-member">
                            <div className="image-container dreamteam-member-image">
                                <img src="http://localhost:3000/hero.jpeg" alt="" />
                            </div>
                            <h3>Olek</h3>
                            <p>
                                Większość z nas całe życie szuka pasji i próbuje obrać własną 
                                ścieżkę. Ja miałem to szczęście ze odkryłem to wszystko za 
                                małolata dzięki moim rodzicom. Ojciec studiując politykę 
                                społeczną zaraził mnie potrzeba pomagania innym, a zapisując na 
                                zajęcia z koszykówki pozwolił znaleźć sport w którym szczerze się 
                                zakochałem. Dzięki mojej mamie nauczyłem się nigdy nie 
                                poddawać i walczyć o swoje marzenia. Z takim przygotowaniem 
                                wystartowałem w życie. Cała reszta zależała ode mnie. 
                                Angażowałem się w szkolne projekty i zbiórki, rozwijałem się 
                                sportowo. W wieku 15 lat stwierdziłem że chciałbym zrobić coś 
                                swojego. W 3 gimnazjum zorganizowałem turniej charytatywny 
                                wspierający chorego chłopca z mojej szkoły, a w 2 liceum 
                                udzielałem darmowych korepetycji dzieciom z trudną sytuacja 
                                finansowa. Obecny projekt jest więc zwieńczeniem moich marzeń 
                                z racji tego ze łączę pomoc z pasją, a zawsze byłem wierny zasadzie 
                                aby łączyć przyjemne z pożytecznym.
                            </p>
                        </li>
                        <li className="dreamteam-member">
                            <div className="image-container dreamteam-member-image">
                                <img src="http://localhost:3000/hero.jpeg" alt="" />
                            </div>
                            <h3>Mateusz</h3>
                            <p>
                                Organizowałem Szlachetna Paczkę na Białołęce, dalej pozostaję tam wolontariuszem, ale chciałem zrobić coś swojego. 
                                Po finale Paczki czułem dziką satysfakcję. 
                                Chce więcej, w tym roku będę miał 13 takich finałów, pod znakiem akcji, którą razem tworzymy. 
                                Późno zacząłem przygodę ze sportem, nie wdałem się w tatę który był profesjonalnym koszykarzem.
                                Macham sztangą i dążę do trójbojowych debiutów, a ostatnio odkryłem magię szosówki. 
                                Mnóstwo pracy przed nami, ale nie mogę się doczekać pierwszego turnieju
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div className="hero-wave">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320" style={{backgroundColor: '#E3A084'}}><path fill="#5F7B88" fillOpacity="1" d="M0,256L80,256C160,256,320,256,480,218.7C640,181,800,107,960,101.3C1120,96,1280,160,1360,192L1440,224L1440,320L1360,320C1280,320,1120,320,960,320C800,320,640,320,480,320C320,320,160,320,80,320L0,320Z"></path></svg>
            </div>
            <div className="news-section">
                <div className="container">
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
            </div>
            <div className="news-wave">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320" style={{backgroundColor: '#5F7B88'}}><path fill="#3e555f" fillOpacity="1" d="M0,256L80,229.3C160,203,320,149,480,122.7C640,96,800,96,960,106.7C1120,117,1280,139,1360,149.3L1440,160L1440,320L1360,320C1280,320,1120,320,960,320C800,320,640,320,480,320C320,320,160,320,80,320L0,320Z"></path></svg>
            </div>
        </section>
    );
}
