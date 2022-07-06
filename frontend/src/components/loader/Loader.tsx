import { SyncLoader } from 'react-spinners';
import styles from './Loader.module.css';


type Props = {
    size?: number;
    margin?: number;
}

export default function Loader({ size = 7, margin = 20 }: Props) {

    return (
        <div className={styles.root}>
            <SyncLoader
                loading
                size={size}
                margin={margin}
                color="#fff"
            />
        </div>
    );
}
