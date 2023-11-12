import { useSelector } from "react-redux";

import List from "@mui/joy/List";

import HomePageIcon from "@mui/icons-material/HomeRounded";
import ProductPageIcon from "@mui/icons-material/Storefront";
import CartPageIcon from "@mui/icons-material/ShoppingCartRounded";
import OrderPageIcon from "@mui/icons-material/ShoppingBagRounded";

import SidebarNavLink from "./sidebar-navlink";

import { selectCartLineCount } from "../../cart/cart-slice";

function SidebarNav() {
  const cartLineCount = useSelector(selectCartLineCount);

  return (
    <List
      size="sm"
      sx={{
        gap: 1,
        "--List-nestedInsetStart": "30px",
        "--ListItem-radius": (theme) => theme.vars.radius.sm,
      }}
    >
      <SidebarNavLink to="/" title="Home" icon={<HomePageIcon />} />
      <SidebarNavLink
        to="/product"
        title="Product"
        icon={<ProductPageIcon />}
      />
      <SidebarNavLink
        to="/cart"
        title="Cart"
        icon={<CartPageIcon />}
        chip={cartLineCount}
      />
      <SidebarNavLink to="/order" title="Order" icon={<OrderPageIcon />} />
    </List>
  );
}

export default SidebarNav;
