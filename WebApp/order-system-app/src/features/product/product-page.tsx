import { useMemo } from "react";

import Box from "@mui/joy/Box";
import Button from "@mui/joy/Button";
import Stack from "@mui/joy/Stack";

import { useSearchParams } from "react-router-dom";
import qs from "qs";

import MainLayout from "../common/main-layout";
import ProductGrid from "./product-grid";
import {
  GetProductsRequest,
  useGetProductsQuery,
} from "../../services/product-api";
import { useAppDispatch } from "../../app/store";
import { updateCartLine } from "../cart/cart-slice";
import { Product } from "../../services/types";

function createSearchParams(obj: object) {
  return new URLSearchParams(qs.stringify(obj));
}

const initialSearchParams = createSearchParams({
  pageIndex: 1,
  pageSize: 18,
});

export default function ProductPage() {
  const dispatch = useAppDispatch();

  const [searchParams, setSearchParams] = useSearchParams(initialSearchParams);
  const getProductRequest = useMemo<GetProductsRequest>(() => {
    const request = {
      pageIndex: parseInt(searchParams.get("pageIndex") || "1") ?? 1,
      pageSize: parseInt(searchParams.get("pageSize") || "20") ?? 16,
    };

    if (!request.pageIndex || request.pageIndex < 1) {
      request.pageIndex = 1;
    }
    if (!request.pageSize || request.pageSize < 1) {
      request.pageSize = 18;
    }

    return request;
  }, [searchParams]);

  const { data, isFetching, isSuccess } = useGetProductsQuery(
    getProductRequest,
    {
      refetchOnMountOrArgChange: true,
    }
  );

  const items = data?.items || [];

  function handlePrev(): void {
    const params = createSearchParams({
      ...getProductRequest,
      pageIndex: getProductRequest.pageIndex - 1,
    });
    setSearchParams(params);
  }

  function handleNext(): void {
    const params = createSearchParams({
      ...getProductRequest,
      pageIndex: getProductRequest.pageIndex + 1,
    });
    setSearchParams(params);
  }

  const shouldDisableButtons = data == null || isFetching || !isSuccess;

  function handleAddToCart(item: Product): MayBePromise<void> {
    dispatch(
      updateCartLine({
        op: "add",
        productId: item.id,
        quantity: 1,
      })
    );
  }

  return (
    <MainLayout>
      <Box mb={3}>
        <ProductGrid items={items} onSelect={handleAddToCart} />
      </Box>
      <Box>
        <Stack direction="row" gap={2} justifyContent="flex-end">
          <Button
            onClick={handlePrev}
            disabled={shouldDisableButtons || getProductRequest.pageIndex < 2}
          >
            Prev
          </Button>
          <Button
            onClick={handleNext}
            disabled={
              shouldDisableButtons ||
              getProductRequest.pageIndex > data.totalPage - 1
            }
          >
            Next
          </Button>
        </Stack>
      </Box>
    </MainLayout>
  );
}
