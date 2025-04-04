
export interface Paging<T> {
    page: number;
    pageSize: number;
    count: number;
    data: T[];
}
