import { Link } from "react-router-dom";
import { FacebookPost } from "../../../_lib/types";
import styles from "./Post.module.css";

export default function Post(post: FacebookPost) {
	return (
		<>
			<li className={styles.item}>
				<div className={styles.post}>
					<div
						className={styles.pictureBlock}
						style={
							post.full_picture
								? {
										backgroundImage: `url(${post.full_picture})`,
								  }
								: undefined
						}
					></div>
					<div className={styles.itemContent}>
						<div className={styles.itemDetails}>
							<p className={styles.dates}>
								{post.created_time.split("T")[0]}
							</p>
							<p>{post.message.slice(0, 200)}</p>
						</div>
						<Link className="slide-button" to={post.permalink_url}>
							Zobacz więcej
						</Link>
					</div>
				</div>
			</li>
			{/* <li
				className="news-post"
				style={{
					backgroundImage: `url(${
						props.feed
							? props.feed.data[props.index].full_picture
							: "../../public/logo.png"
					})`,
				}}
			>
				<div className="post-image">
					<div className="card-container">
						<div className="post-description">
							<h6 className="post-date">
								{props.feed &&
									props.feed.data[
										props.index
									].created_time.split("T")[0]}
							</h6>
							<p className="message">
								{props.feed &&
									props.feed.data[props.index].message.slice(
										0,
										50
									)}
							</p>
							<a
								className="slide-button"
								href={`${
									props.feed &&
									props.feed.data[props.index].permalink_url
								}`}
								target="_blank"
								rel="noreferrer"
							>
								<p>See more</p>
							</a>
						</div>
					</div>
				</div>
			</li> */}
		</>
	);
}
