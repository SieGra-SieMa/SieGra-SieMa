import { useEffect } from 'react';
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
                            </i> - Ray Allen
                            <br/><br/>
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
                                <img src="http://localhost:3000/card_olek.png" alt="" />
                            </div>
                            <h4>Olek</h4>
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
                                <img src="http://localhost:3000/card_mateusz.png" alt="" />
                            </div>
                            <h4>Mateusz</h4>
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
        </section>
    );
}
