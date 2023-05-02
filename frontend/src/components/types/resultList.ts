import { ReactElement } from "react";

export interface ResultListProps{
    list: any;
    loadingUI?: ReactElement;
    emptyListUI?: ReactElement;
    children: ReactElement;
}