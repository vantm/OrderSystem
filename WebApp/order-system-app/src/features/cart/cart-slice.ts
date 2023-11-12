import { createSelector, createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { find, omit, remove } from "lodash";
import { AppState } from "../../app/store";

type CartLine = {
  productId: string;
  quantity: number;
};

type CartState = {
  lines: CartLine[];
};

type UpdateCartLinePayload = CartLine & {
  op: "add" | "set";
};

const initialState: CartState = {
  lines: [],
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    updateCartLine(state, { payload }: PayloadAction<UpdateCartLinePayload>) {
      const prevLine = find(state.lines, function (line: CartLine): boolean {
        return line.productId == payload.productId;
      });
      if (prevLine == null) {
        state.lines.unshift(omit(payload, ["op"]));
      } else if (payload.op == "add") {
        prevLine.quantity += payload.quantity;
      } else if (payload.quantity > 0) {
        prevLine.quantity = payload.quantity;
      } else {
        remove(state.lines, prevLine);
      }
    },
    clearCart(state) {
      state.lines = [];
    },
  },
});

export const { updateCartLine, clearCart } = cartSlice.actions;

const selectCart = (state: AppState) => state.cart;

export const selectCartLineCount = createSelector(
  selectCart,
  (cart) => cart.lines.length
);

export const selectCartLines = createSelector(selectCart, (cart) => cart.lines);

export default cartSlice.reducer;
