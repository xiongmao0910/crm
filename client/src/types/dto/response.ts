export type TResponseDto<T> = {
    status: boolean;
    message: string;
    data: T;
};

export type TResponsePageDto<T> = TResponseDto<T> & {
    totalRowCount: number;
};

export type TResponsePageData<T> = {
    data: T[];
    totalRowCount: number;
} | null;
