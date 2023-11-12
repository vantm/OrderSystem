export interface PageRequest {
  pageIndex: number;
  pageSize: number;
}

export interface PageResponse<T> {
  items: T[];
  totalPage: number;
}

export interface Product {
  id: string;
  name: string;
  price: number;
  image?: string;
}
