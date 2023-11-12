import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";

import { api } from "../services/api";
import cartReducer from "../features/cart/cart-slice";
import settingReducer from "../features/setting/setting-slice";

const store = configureStore({
  reducer: {
    [api.reducerPath]: api.reducer,
    cart: cartReducer,
    setting: settingReducer
  },
  middleware(getDefaultMiddleware) {
    return getDefaultMiddleware().concat([api.middleware]);
  },
});

export type AppState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;

export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<AppState> = useSelector;

export default store;
