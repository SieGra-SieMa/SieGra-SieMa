import { useEffect, useState } from 'react';
import { FacebookFeed } from '../../_lib/types';
import './NewsFeed.css';
import Post from './post/Post';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';

interface NewsFeedProps {
	fetchLimit?: number;
}

export default function NewsFeed(props?: NewsFeedProps) {
	const APP_ID = "102617622481951";
	const ACCESS_TOKEN =
		"EAAo3XMvZBYycBACpK7VGHceN5x4YkHF36r3IsvEt8btZBH0Q5cMJg5il78AUm74yp3pp61zFydbzLAcTT4vhDrvzM02yUD321ecwArDrkHb2HtqxsArg0iGPz52b3bGX5FXFdO4fM4QZAXe4iAhiPTv9njiZCPezACVRXmQolUREyzcKQwYJQKjkdkZBhNsoZD";
	const [feed, setFeed] = useState<FacebookFeed | null>(null);
	const fetchLimit = props?.fetchLimit ? props?.fetchLimit : 10;

	const [slide, setSlide] = useState(0);
	const [nextVisible, setNextVisible] = useState(true);
	const [prevVisible, setPrevVisible] = useState(false);

	useEffect(() => {
		FB.api(
			`/${APP_ID}/posts?access_token=${ACCESS_TOKEN}&limit=${fetchLimit}`,
			"get",
			{ fields: "full_picture,message,created_time,permalink_url" },
			function (response: FacebookFeed) {
				console.log(1)
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
