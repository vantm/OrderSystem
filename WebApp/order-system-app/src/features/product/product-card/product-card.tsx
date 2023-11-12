import AspectRatio from "@mui/joy/AspectRatio";
import Card from "@mui/joy/Card";
import CardContent from "@mui/joy/CardContent";
import IconButton from "@mui/joy/IconButton";
import Typography from "@mui/joy/Typography";

import AddToCartIcon from "@mui/icons-material/AddShoppingCartRounded";
import FallbackImageIcon from "@mui/icons-material/ImageRounded";

import { Product } from "../../../services/types";
import useFormat from "../../setting/use-format";

export interface ProductCardProps {
  item: Product;
  onSelect?: (item: Product) => MayBePromise;
}

function ProductCard(props: ProductCardProps) {
  const { currency: C } = useFormat();

  const { item: product } = props;

  function handleSelect(): void {
    props.onSelect?.(product);
  }

  return (
    <Card>
      <div>
        <Typography level="title-lg">{product.name}</Typography>
      </div>
      <AspectRatio minHeight="120px" maxHeight="300px" objectFit="cover">
        <object data={product.image} type="image/jpeg">
          <FallbackImageIcon />
        </object>
      </AspectRatio>
      <CardContent orientation="horizontal">
        <div>
          <Typography level="body-xs">Total price:</Typography>
          <Typography fontSize="lg" fontWeight="lg">
            {C(product.price)}
          </Typography>
        </div>
        <IconButton
          variant="solid"
          size="md"
          color="primary"
          aria-label={`Add ${product.name} to cart`}
          sx={{ ml: "auto", alignSelf: "center", fontWeight: 600 }}
          onClick={handleSelect}
        >
          <AddToCartIcon />
        </IconButton>
      </CardContent>
    </Card>
  );
}

export default ProductCard;
