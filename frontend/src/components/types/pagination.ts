export interface LinkModel {
  page: number;
  enabled: boolean;
  text: string;
  active: boolean;
}

export interface PaginationProps {
  currentPage: number;
  totalAmountOfPages: number;
  radio: number;
  onChange(page: number): void;
}


export interface RecordsPerPageSelectionProps {
  onChange(recordsPerPage: number): void;
}