import { useEffect, useState } from "react"
import { FacebookFeed } from "../../_lib/types";
import '../home/Home.css'

export default function NewsFeed() {
    const APP_ID = "102617622481951";
    const ACCESS_TOKEN = "EAAo3XMvZBYycBAHVdiapMw63KyWyPhsjxJP5FuEfAeqGqnBZCkYZBDtg3e5GE93HkiB1iyIMEwPYa1uJo21W8ZBdoTVPZBLCVlYTBUxtjXBK0NmIjPETMwrXaQDVVnzpHxF5HKO6nuepaG5WMTAkhV4411ojTUivZC7DIfNZBohiij9yGvCATDzVRo5hN7ZCJD0ZD";
    const [feed, setFeed] = useState<FacebookFeed | null>(null);
    const [isFetched, setIsFetched] = useState(false);
    // eslint-disable-next-line
    const [postMessage, setPostMessage] = useState("");

    useEffect(() => {
        FB.api(
            `/${APP_ID}/posts?access_token=${ACCESS_TOKEN}`,
            'get',
            {"fields":"full_picture,message,created_time"},
            function(response:FacebookFeed) {
                setFeed(response);
                setIsFetched(true);
            }
          );
    }, []);

    useEffect(() => {
        if(isFetched && feed){
            feed.data.forEach(post => {
                setPostMessage(post.message);
            });
        }
    }, [isFetched, feed]);

    return( 
        <div className="news-section">
            <h2>News feed</h2>
            <ul className="news-list">
            {feed && feed.data.map((post, index) => (
                    <li key={index}>
                        <p>{post.message}</p>
                        <img src={post.full_picture} alt="jano" />
                    </li>
                ))}
            </ul>
        </div>
    )
}