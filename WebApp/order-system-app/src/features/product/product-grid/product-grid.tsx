import { Grid } from "@mui/joy";

import type { Product } from "../../../services/types";

import ProductCard from "../product-card";

interface ProductGridProps {
  items: Product[];
  onSelect?: (item: Product) => MayBePromise;
}

function ProductGrid(props: ProductGridProps) {
  return (
    <Grid container spacing={2}>
      {props.items.map((item) => (
        <Grid key={item.id} xs={12} md={4} lg={3} xl={2}>
          <ProductCard item={item} onSelect={props.onSelect} />
        </Grid>
      ))}
    </Grid>
  );
}

export default ProductGrid;
