import { useEffect, useState } from "react"
import { FacebookFeed } from "../../_lib/types";
import './NewsFeed.css';

interface NewsFeedProps {
    fetchLimit?: string;
}

export default function NewsFeed(props?: NewsFeedProps) {
    const APP_ID = "102617622481951";
    const ACCESS_TOKEN = "EAAo3XMvZBYycBAHVdiapMw63KyWyPhsjxJP5FuEfAeqGqnBZCkYZBDtg3e5GE93HkiB1iyIMEwPYa1uJo21W8ZBdoTVPZBLCVlYTBUxtjXBK0NmIjPETMwrXaQDVVnzpHxF5HKO6nuepaG5WMTAkhV4411ojTUivZC7DIfNZBohiij9yGvCATDzVRo5hN7ZCJD0ZD";
    const [feed, setFeed] = useState<FacebookFeed | null>(null);
    const fetchLimit = props?.fetchLimit ? props?.fetchLimit : "10";

    useEffect(() => {
        FB.api(
            `/${APP_ID}/posts?access_token=${ACCESS_TOKEN}&limit=${fetchLimit}`,
            'get',
            { "fields": "full_picture,message,created_time,permalink_url" },
            function (response: FacebookFeed) {
                setFeed(response);
            }
        );
    }, [fetchLimit]);

    interface PostProps {
        feed: FacebookFeed | null,
        index: number
    }

    function Post(props: PostProps) {
        return (
            <li className="news-post" style={{
                backgroundImage: `url(${props.feed ? props.feed.data[props.index].full_picture : "../../public/logo.png"})`
            }}>
                <div className="post-image" >
                    <div className="card-container">
                        <div className="post-description">
                            <h6 className="post-date">
                                {props.feed && props.feed.data[props.index].created_time.split('T')[0]}
                            </h6>
                            <p className="message">
                                {props.feed && props.feed.data[props.index].message.slice(0, 50)}
                            </p>
                            <a className="slide-button" href={`${props.feed && props.feed.data[props.index].permalink_url}`} target='_blank' rel="noreferrer">
                                <p>See more</p>
                            </a>
                        </div>
                    </div>
                </div>
            </li>
        );
    }

    return (
        // <div className="news-section">
        //     <h2>News feed</h2>
        //     <div className="posts">
        //         <ul className="news-list">
        //             {feed && feed.data.map((post, index) => (
        //                 <li key={index} className="news-post">
        //                     <div className="post-image"><img src={post.full_picture} alt="" /></div>
        //                     <div className="post-description">
        //                         <h6>{post.created_time.split('T')[0]}</h6>
        //                         <p className="message">{post.message}</p>
        //                     </div>
        //                 </li>
        //             ))}
        //         </ul>
        //     </div> 
        // </div>
        <div className="news-section">
            <div className="container news-container">
                <h2>News feed</h2>
                <ul className="news-list">
                    <Post feed={feed} index={0} />
                    <Post feed={feed} index={1} />
                    <Post feed={feed} index={2} />
                </ul>
            </div>
        </div>
    )
}