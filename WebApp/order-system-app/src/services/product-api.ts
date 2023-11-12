import qs from "qs";

import { api } from "./api";
import { PageRequest, PageResponse, Product } from "./types";

import "./product-api.mock";

export interface GetProductsRequest extends PageRequest {}
export interface GetProductsResponse extends PageResponse<Product> {}

export interface GetProductsByIdsRequest {
  ids: string[];
}

export type GetProductByIdsResponse = Product[];

export const productApi = api.injectEndpoints({
  endpoints(build) {
    return {
      getProducts: build.query<GetProductsResponse, GetProductsRequest>({
        query: (args) => {
          const queryString = qs.stringify(args, {
            addQueryPrefix: true,
          });
          const url = `/products${queryString}`;
          return url;
        },
        providesTags: ["product"],
      }),
      getProductsByIds: build.query<
        GetProductByIdsResponse,
        GetProductsByIdsRequest
      >({
        query: (args) => {
          const queryString = qs.stringify(args, {
            addQueryPrefix: true,
            arrayFormat: "brackets",
          });
          const url = `/products/ids${queryString}`;
          return url;
        },
      }),
    };
  },
});

export const {
  useGetProductsQuery,
  useLazyGetProductsQuery,
  useGetProductsByIdsQuery,
  useLazyGetProductsByIdsQuery,
} = productApi;
