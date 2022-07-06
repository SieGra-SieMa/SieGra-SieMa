import Config from '../../config.json';
import { useEffect, useState } from 'react';
import { FacebookFeed } from '../../_lib/types';
import './NewsFeed.css';
import Post from './post/Post';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';


type Props = {
	fetchLimit?: number;
}

export default function NewsFeed(props: Props) {

	const [feed, setFeed] = useState<FacebookFeed | null>(null);
	const fetchLimit = props.fetchLimit ?? 10;

	const [slide, setSlide] = useState(0);
	const [nextVisible, setNextVisible] = useState(true);
	const [prevVisible, setPrevVisible] = useState(false);

	useEffect(() => {
		FB.api(
			`/${Config.APP_ID}/posts?access_token=${Config.ACCESS_TOKEN}&limit=${fetchLimit}`,
			"get",
			{ fields: "full_picture,message,created_time,permalink_url" },
			function (response: FacebookFeed) {
				setFeed(response);
			}
		);

	}, [fetchLimit]);

	useEffect(() => {
		setNextVisible(slide === fetchLimit - 1 ? false : true);
		setPrevVisible(slide === 0 ? false : true);
	}, [slide, fetchLimit]);

	function nextSlide() {
		let nextSlide = slide + 1;
		setSlide(nextVisible ? nextSlide : slide);
	}
	function prevSlide() {
		let prevSlide = slide - 1;
		setSlide(prevVisible ? prevSlide : slide);
	}

	return (
		<div className="news-wrapper">
			<ul
				className="news-list"
				style={{
					width: `${fetchLimit * 100}%`,
					transform: `translateX(-${slide / fetchLimit * 100}%)`,
				}}
			>
				{feed &&
					feed.data.map((post, index) => (
						<Post
							key={index}
							created_time={post.created_time}
							full_picture={post.full_picture}
							id={post.id}
							message={post.message}
							permalink_url={post.permalink_url}
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
	);
}
