export type Message = {
    message: string;
}

export type Media = {
    id: number;
    url: string;
}


export type Paginated<T> = {
    totalCount: number;
    items: T[];
}

