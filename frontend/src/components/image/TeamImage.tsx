import Config from '../../config.json';
import styles from './TeamImage.module.css';
import ImageIcon from '@mui/icons-material/Image';


type Props = {
    className?: string;
    url?: string;
    size: number;
    placeholderSize: number;
    onClick?: () => void;
};

export default function TeamImage({ className, url, size, placeholderSize, onClick }: Props) {

    const sizes = {
        width: `${size}px`,
        height: `${size}px`,
    };

    return (
        <div
            className={[
                styles.picture,
                className
            ].filter((e) => e).join(' ')}
            style={url ? {
                ...sizes,
                backgroundImage: `url(${Config.HOST}${url})`
            } : sizes}
            onClick={onClick}
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
