import { FormEvent, useState } from 'react';
import { ApiResponse } from '../../_services/service';
import Loader from '../loader/Loader';
import VerticalSpacing from '../spacing/VerticalSpacing';
import styles from './Form.module.css';


type Props = {
    className?: string;
    onSubmit: (e: FormEvent) => (ApiResponse<any> | Promise<any> | undefined);
    trigger?: React.ReactNode;
    children: React.ReactNode;
}

export default function Form({ className, onSubmit, trigger, children }: Props) {

    const [isLoading, setIsLoading] = useState(false);

    return (
        <form
            className={[
                styles.root,
                className
            ].filter((e) => e).join(' ')}
            onSubmit={(e) => {
                setIsLoading(true);
                const result = onSubmit(e);
                if (result) {
                    result.then(() => setIsLoading(false))
                        .catch(() => setIsLoading(false));
                } else {
                    setIsLoading(false);
                }
            }}
        >
            {children}
            {isLoading ? (trigger && (<>
                <VerticalSpacing size={10} />
                <Loader size={12} />
            </>)) : (trigger)}
        </form>
    );
}
