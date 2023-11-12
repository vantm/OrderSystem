import { useMemo } from "react";

import Box from "@mui/joy/Box";
import Button from "@mui/joy/Button";
import Container from "@mui/joy/Container";
import FormControl from "@mui/joy/FormControl";
import FormLabel from "@mui/joy/FormLabel";
import Input from "@mui/joy/Input";
import Sheet from "@mui/joy/Sheet";
import Stack from "@mui/joy/Stack";
import Table from "@mui/joy/Table";
import Typography from "@mui/joy/Typography";

import BackIcon from "@mui/icons-material/ChevronLeftRounded";

import { NavLink } from "react-router-dom";
import { chain } from "lodash";

import { selectCartLines } from "./cart-slice";
import { useAppSelector } from "../../app/store";
import { useGetProductsByIdsQuery } from "../../services/product-api";
import PageHelmet from "../common/page-helmet";
import useFormat from "../setting/use-format";

const pageTitle = "Checkout your cart";

export default function CheckoutPage() {
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
        <td>{N(quantity)}</td>
        <td>{C(rowTotal)}</td>
      </tr>
    );

    index++;
  }

  return (
    <Container maxWidth="md">
      <Box my={3}>
        <PageHelmet title={pageTitle} />
        <Box mb={3}>
          <Button
            startDecorator={<BackIcon />}
            component={NavLink}
            to="/cart"
            replace
          >
            Back
          </Button>
        </Box>
        <Box mb={3}>
          <Typography level="h2">Checkout</Typography>
        </Box>

        <Box mb={3}>
          <Stack gap={2} maxWidth="min(300px, 100%)">
            <FormControl>
              <FormLabel>Name</FormLabel>
              <Input />
            </FormControl>
            <FormControl>
              <FormLabel>Email</FormLabel>
              <Input type="email" />
            </FormControl>
            <FormControl>
              <FormLabel>Delivery Address</FormLabel>
              <Input />
            </FormControl>
          </Stack>
        </Box>

        <Box>
          <Box mb={3} borderRadius="sm" overflow="hidden">
            <Sheet
              variant="soft"
              sx={{
                overflow: "auto",
              }}
            >
              <Table
                sx={{
                  "& thead tr th:first-child": {
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
                  "& tfoot tr td:last-child": {
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
                  </tr>
                </thead>
                <tbody>{rows}</tbody>
                <tfoot>
                  <tr>
                    <td colSpan={4}>&nbsp;</td>
                    <td>{C(total)}</td>
                  </tr>
                </tfoot>
              </Table>
            </Sheet>
          </Box>

          <Box>
            <Stack direction="row" justifyContent="flex-end">
              <Button aria-label="Submit an order" title="Submit an order">
                Submit
              </Button>
            </Stack>
          </Box>
        </Box>
      </Box>
    </Container>
  );
}
