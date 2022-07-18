import Config from '../../config.json';
import styles from './TeamImage.module.css';
import ImageIcon from '@mui/icons-material/Image';


type Props = {
    url?: string;
    size: number;
    placeholderSize: number;
};

export default function TeamImage({ url, size, placeholderSize }: Props) {

    const sizes = {
        width: `${size}px`,
        height: `${size}px`,
    };

    return (
        <div
            className={styles.picture}
            style={url ? {
                ...sizes,
                backgroundImage: `url(${Config.HOST}${url})`
            } : sizes}
        >
            {(!url) && (
                <ImageIcon
                    className={styles.placeholder}
                    style={{
                        fontSize: `${placeholderSize}px`
                    }}
                />
            )}
        </div>
    );
}
