import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./Home.css";
import NewsFeed from "../facebook/NewsFeed";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";

export default function Home() {
	const [slide, setSlide] = useState(100);
	const [nextVisible, setNextVisible] = useState(true);
	const [prevVisible, setPrevVisible] = useState(false);

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	function nextSlide() {
		let nextSlide = slide - 100;
		setSlide(nextSlide <= 100 && nextSlide >= -100 ? nextSlide : slide);
		setNextVisible(slide === -100 ? false : true);
        setPrevVisible(slide === 100 ? false : true);
	}
	function prevSlide() {
		let prevSlide = slide + 100;
		setSlide(prevSlide <= 100 && prevSlide >= -100 ? prevSlide : slide);
        setNextVisible(slide === -100 ? false : true);
		setPrevVisible(slide === 100 ? false : true);
	}

	return (
		<section className="main-section">
			<div className="hero-section">
				<div className="container hero-container">
					<div className="image-container hero-image">
						<img src="/hero.png" alt="" />
					</div>
					<div className="hero-block-info">
						<h2>Pasja pomagania</h2>
						<p>
							<i>
								"W końcu nikt nie jest większy od koszykówki.
								Ani Wilt, ani Doktor J, ani Michael, ani Larry,
								ani Magic, ani LeBron. Nikt. Każdy z nas musiał
								coś przezwyciężyć, żeby zajść tak daleko - swoje
								pochodzenie, swoje ograniczenia, swoje
								wątpliwości. Coś, co powstrzymało tak wielu
								innych i pewnie mogło zatrzymać i nas, ale
								jednak nie zatrzymało."
							</i>
							<b> - Ray Allen</b>
							<br />
							<br />
							Każdy z nas ma przed sobą swoją drogę, a naszym
							celem jest pomoc w jej przejściu. Dołącz do nas i
							poprzez pasję do koszykówki pomagaj razem z nami!
						</p>
					</div>
				</div>
			</div>
			<div className="dreamteam-section">
				<div className="container dreamteam-container">
					<h2>Poznaj naszą drużynę!</h2>
					<ul
						className="dreamteam-member-list"
						style={{ transform: `translateX(${slide}vw)` }}
					>
						<li className="dreamteam-member">
							<div className="image-container dreamteam-member-image">
								<img src="/card_kamila.png" alt="" />
							</div>
							<h4>Kamila</h4>
						</li>
						<li className="dreamteam-member">
							<div className="image-container dreamteam-member-image">
								<img src="/card_olek.png" alt="" />
							</div>
							<h4>Olek</h4>
						</li>
						<li className="dreamteam-member">
							<div className="image-container dreamteam-member-image">
								<img src="/card_mateusz.png" alt="" />
							</div>
							<h4>Mateusz</h4>
						</li>
					</ul>
					<div className="navigation-buttons">
						<button
							onClick={prevSlide}
							className={
								prevVisible
									? "navigation-button navigation-button-visible"
									: "navigation-button navigation-button-hidden"
							}
							id="prev-button"
						>
							<NavigateBeforeIcon fontSize="large" />
						</button>
						<button
							onClick={nextSlide}
							className={
								nextVisible
									? "navigation-button navigation-button-visible"
									: "navigation-button navigation-button-hidden"
							}
							id="next-button"
						>
							<NavigateNextIcon fontSize="large" />
						</button>
					</div>

					<Link className="slide-button" to="/about-us">
						Poznaj nas bliżej!
					</Link>
				</div>
			</div>
			<NewsFeed />
		</section>
	);
}
