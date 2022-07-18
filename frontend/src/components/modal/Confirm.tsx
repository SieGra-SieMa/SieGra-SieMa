import { useState } from 'react';
import Button, { ButtonStyle } from '../form/Button';
import Loader from '../loader/Loader';
import styles from './Confirm.module.css';


type Props = {
    cancel: () => void;
    confirm: () => (Promise<any> | undefined);
    label: string;
    style: ButtonStyle;
};

export default function Confirm({
    cancel,
    confirm,
    label,
    style,
}: Props) {

    const [isLoading, setIsLoading] = useState(false);

    return (
        <div className={styles.root}>
            {(isLoading) ? (
                <Loader />
            ) : (<>
                <Button
                    value='Anuluj'
                    onClick={cancel}
                    style={ButtonStyle.Grey}
                />
                <Button
                    value={label}
                    onClick={() => {
                        setIsLoading(true);
                        const result = confirm();
                        if (result) {
                            result.catch(() => setIsLoading(false));
                        } else {
                            setIsLoading(false);
                        }
                    }}
                    style={style}
                />
            </>)}
        </div>
    );
};