import { useEffect, useState } from "react";
import { FacebookFeed } from "../../_lib/types";
import "./NewsFeed.css";
import Post from "./post/Post";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";

interface NewsFeedProps {
	fetchLimit?: string;
}

export default function NewsFeed(props?: NewsFeedProps) {
	const APP_ID = "102617622481951";
	const ACCESS_TOKEN =
		"EAAo3XMvZBYycBAHVdiapMw63KyWyPhsjxJP5FuEfAeqGqnBZCkYZBDtg3e5GE93HkiB1iyIMEwPYa1uJo21W8ZBdoTVPZBLCVlYTBUxtjXBK0NmIjPETMwrXaQDVVnzpHxF5HKO6nuepaG5WMTAkhV4411ojTUivZC7DIfNZBohiij9yGvCATDzVRo5hN7ZCJD0ZD";
	const [feed, setFeed] = useState<FacebookFeed | null>(null);
	const fetchLimit = props?.fetchLimit ? props?.fetchLimit : "10";

	const [slide, setSlide] = useState(0);
	const [nextVisible, setNextVisible] = useState(true);
	const [prevVisible, setPrevVisible] = useState(false);
	const [translation, setTranslation] = useState(0);

	useEffect(() => {
		FB.api(
			`/${APP_ID}/posts?access_token=${ACCESS_TOKEN}&limit=${fetchLimit}`,
			"get",
			{ fields: "full_picture,message,created_time,permalink_url" },
			function (response: FacebookFeed) {
				setFeed(response);
			}
		);
	}, [fetchLimit]);

    useEffect(() => {
        setTranslation(slide * -100);
        setNextVisible(slide === parseInt(fetchLimit) - 1 ? false : true);
		setPrevVisible(slide === 0 ? false : true);
    }, [slide]);

	function nextSlide() {
		let nextSlide = slide + 1;
		setSlide(nextVisible? nextSlide : slide);
		console.log("next" + slide);
	}
	function prevSlide() {
		let prevSlide = slide - 1;
		setSlide(prevVisible ? prevSlide : slide);
		console.log("prev " + slide);
	}

	return (
		<>
			<div className="news-section">
				<h2>News feed</h2>
				<ul
					className="news-list"
					style={{
						width: `${parseInt(fetchLimit) * 100}vw`,
						transform: `translateX(${translation}vw)`,
					}}
				>
					{feed &&
						feed.data.map((post, index) => (
							<Post
								created_time={post.created_time}
								full_picture={post.full_picture}
								id={post.id}
								message={post.message}
								permalink_url={post.created_time}
							/>
						))}
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
			</div>
		</>
	);
}
