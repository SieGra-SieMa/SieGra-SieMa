import styles from "./Pagination.module.css";
import { useSearchParams } from "react-router-dom";
import Button from "../form/Button";

export const COUNT = 12;

type Props = {
    totalPages: number;
    children: JSX.Element;
}

export default function Pagination({ totalPages, children }: Props) {

    const [searchParams, setSearchParams] = useSearchParams();

    const pageParam = parseInt(searchParams.get('page') || '1');
    const page = isNaN(pageParam) ? 1 : pageParam;

    const nav = (index: number) => (
        <Button
            key={index}
            onClick={() => {
                setSearchParams({ page: `${index}` });
            }}
            value={`${index}`}
            disabled={index === page}
        />
    )

    const getPages = () => {
        const arr: JSX.Element[] = [];
        for (
            let i = page > 2 ? (page > totalPages - 2 ? totalPages - 3 : page - 1) : 2;
            i < (page > 3 ? (page + 1 < totalPages ? page + 2 : (page !== totalPages ? page + 1 : totalPages)) : 5);
            i++
        ) {
            arr.push(
                nav(i)
            )
        }
        return arr;
    }

    return (totalPages > 1) ? (<>
        {children}
        <div className={styles.pagination}>
            {(totalPages > 5) ? (<>
                {nav(1)}
                {page > 3 && "..."}
                {getPages()}
                {totalPages - page > 2 && "..."}
                {nav(totalPages)}
            </>) : (
                Array.from('-'.repeat(totalPages)).map((_, index) => (
                    nav(index + 1)
                ))
            )}
        </div>
    </>) : children;
}
