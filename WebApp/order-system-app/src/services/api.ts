import { createApi, fetchBaseQuery, retry } from "@reduxjs/toolkit/query/react";
import { createServer, Model } from "miragejs";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

const baseQuery = fetchBaseQuery({
  baseUrl: BASE_URL,
});

const baseQueryWithRetry = retry(baseQuery, {
  maxRetries: 3,
});

export const api = createApi({
  reducerPath: "splitApi",
  baseQuery: baseQueryWithRetry,
  tagTypes: ["product", "cart", "order"],
  endpoints: () => ({}),
});

export const mock = createServer({
  urlPrefix: BASE_URL,
  models: {
    products: Model,
  },
});
