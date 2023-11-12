import { createBrowserRouter } from "react-router-dom";

import HomeIcon from "@mui/icons-material/HomeRounded";

import createRouteHandle from "../features/common/create-route-handle";

import HomePage from "../features/home/home-page";
import LoginPage from "../features/auth/login-page";
import CartPage from "../features/cart/cart-page";
import OrderPage from "../features/order/order-page";
import CheckoutPage from "../features/cart/checkout-page";
import ProductPage from "../features/product/product-page";
import SettingPage from "../features/setting/setting-page";

const router = createBrowserRouter(
  [
    {
      path: "/",
      handle: createRouteHandle({
        title: "Home",
        icon: <HomeIcon fontSize="small" />,
        path: "/",
      }),
      children: [
        {
          index: true,
          Component: HomePage,
        },
        {
          path: "product",
          handle: createRouteHandle({
            title: "Product",
            path: "/product",
          }),
          Component: ProductPage,
        },
        {
          path: "cart",
          handle: createRouteHandle({
            title: "Your cart",
            path: "/cart",
          }),
          children: [
            {
              index: true,
              Component: CartPage,
            },
            {
              path: "checkout",
              Component: CheckoutPage,
            },
          ],
        },
        {
          path: "order",
          handle: createRouteHandle({
            title: "Your orders",
            path: "/order",
          }),
          Component: OrderPage,
        },
        {
          path: "setting",
          handle: createRouteHandle({
            title: "Setting",
            path: "/setting",
          }),
          Component: SettingPage,
        },
      ],
    },
    {
      path: "/login",
      handle: createRouteHandle({
        title: "Login",
        path: "/login",
      }),
      Component: LoginPage,
    },
  ],
  {
    future: {
      v7_fetcherPersist: true,
    },
  }
);

if (import.meta.hot) {
  import.meta.hot.dispose(() => router.dispose());
}

export default router;
