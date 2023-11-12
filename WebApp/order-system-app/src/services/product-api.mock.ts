import { chain } from "lodash";

import { mock } from "./api";
import { GetProductByIdsResponse, GetProductsResponse } from "./product-api";
import { Product } from "./types";

const initialProducts: Product[] = [];

function randDim() {
  return Math.round(1 + Math.random() * 9) * 100;
}

if (initialProducts.length == 0) {
  for (let i = 0; i < 80; i++) {
    const product: Product = {
      id: crypto.randomUUID(),
      name: `Product ${i + 1}`,
      price: Math.round(Math.random() * 999 + 1) * 500,
      image: `http://placekitten.com/${randDim()}/${randDim()}`,
    };

    initialProducts.push(product);
  }

  mock.db.loadData({
    products: initialProducts,
  });
}

mock.get<GetProductsResponse>(
  "/products",
  (scheme, request) => {
    const pageIndex = parseInt(request.queryParams["pageIndex"] as string);
    const pageSize = parseInt(request.queryParams["pageSize"] as string);
    const offset = (pageIndex - 1) * pageSize;
    const allProducts = scheme.all("products");

    const items = chain(allProducts.models)
      .slice(offset)
      .take(pageSize)
      .map("attrs")
      .value() as Product[];
    const totalPage = Math.ceil(allProducts.models.length / pageSize);

    return { items, totalPage };
  },
  {
    timing: 200,
  }
);

mock.get<GetProductByIdsResponse>(
  "/products/ids",
  (scheme, request) => {
    const ids = request.queryParams["ids"];
    const allProducts = scheme.all("products");
    const items = chain(allProducts.models)
      .filter((x) => ids.includes(x.attrs.id))
      .map("attrs")
      .value() as Product[];

    return items;
  },
  {
    timing: 200,
  }
);
