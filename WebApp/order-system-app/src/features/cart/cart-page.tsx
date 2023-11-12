import { useMemo } from "react";

import Box from "@mui/joy/Box";
import Button from "@mui/joy/Button";
import IconButton from "@mui/joy/IconButton";
import Input from "@mui/joy/Input";
import Sheet from "@mui/joy/Sheet";
import Stack from "@mui/joy/Stack";
import Table from "@mui/joy/Table";

import { chain } from "lodash";

import DeleteIcon from "@mui/icons-material/DeleteRounded";

import { selectCartLines, updateCartLine } from "./cart-slice";
import { useAppDispatch, useAppSelector } from "../../app/store";
import { useGetProductsByIdsQuery } from "../../services/product-api";
import MainLayout from "../common/main-layout";
import useFormat from "../setting/use-format";
import { NavLink } from "react-router-dom";

export default function CartPage() {
  const dispatch = useAppDispatch();
  const { currency: C, number: N } = useFormat();
  const lines = useAppSelector(selectCartLines);
  const ids = useMemo(
    () => chain(lines).map("productId").uniq().value(),
    [lines]
  );
  const { data } = useGetProductsByIdsQuery(
    { ids },
    {
      refetchOnMountOrArgChange: true,
      skip: ids.length == 0,
    }
  );
  const products = useMemo(
    () =>
      chain(data)
        .reduce(function (a, c) {
          a[c.id] = c;
          return a;
        }, {})
        .value(),
    [data]
  );

  function handleChange(productId: string, quantity: number) {
    dispatch(
      updateCartLine({
        op: "set",
        productId,
        quantity,
      })
    );
  }

  const rows: React.ReactNode[] = [];

  let index = 1;
  let total = 0;

  for (const item of lines) {
    const product = products[item.productId];
    const price = product?.price ?? 0;
    const quantity = item.quantity;
    const rowTotal = price * quantity;

    total += rowTotal;

    rows.push(
      <tr key={item.productId}>
        <td>{index + 1}</td>
        <td>{product?.name}</td>
        <td>{C(price)}</td>
        <td>
          <Input
            value={quantity}
            onChange={(e) => {
              handleChange(item.productId, e.target.valueAsNumber);
            }}
            type="number"
            slotProps={{
              input: {
                min: 1,
                max: 99,
                sx: { textAlign: "right" },
              },
            }}
            sx={{ width: 80 }}
          />
        </td>
        <td>{C(rowTotal)}</td>
        <td>
          <IconButton
            onClick={() => {
              handleChange(item.productId, 0);
            }}
          >
            <DeleteIcon />
          </IconButton>
        </td>
      </tr>
    );

    index++;
  }

  return (
    <MainLayout>
      <Box mb={3}>
        <Sheet>
          <Table
            sx={{
              "& thead tr th:first-child, & thead tr th:last-child": {
                width: "40px",
              },
              "& thead tr th:nth-child(3), & thead tr th:nth-child(5)": {
                width: "150px",
              },
              "& thead tr th:nth-child(4)": {
                width: "100px",
              },
              "& tbody tr td:nth-child(2), & tbody tr td:nth-child(5)": {
                fontWeight: "bold",
              },
              "& tbody tr td:not(:nth-child(2))": {
                textAlign: "right",
              },
              "& tfoot tr td:nth-child(n-1)": {
                fontWeight: "bold",
                textAlign: "right",
              },
            }}
          >
            <thead>
              <tr>
                <th>#</th>
                <th>Product</th>
                <th>Price</th>
                <th>Quatity</th>
                <th>Total</th>
                <th>&nbsp;</th>
              </tr>
            </thead>
            <tbody>{rows}</tbody>
            <tfoot>
              <tr>
                <td colSpan={4}>&nbsp;</td>
                <td>{C(total)}</td>
                <td>&nbsp;</td>
              </tr>
            </tfoot>
          </Table>
        </Sheet>
      </Box>
      <Box>
        <Stack direction="row" justifyContent="flex-end">
          <Button
            component={NavLink}
            aria-label="Checkout"
            title="Checkout"
            to="/cart/checkout"
            replace
          >
            Checkout
          </Button>
        </Stack>
      </Box>
    </MainLayout>
  );
}
