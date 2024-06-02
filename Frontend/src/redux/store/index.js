import { configureStore } from "@reduxjs/toolkit";
import { persistStore } from "redux-persist";
import authSlice from "../slices/authSlice";
import productSlice from "../slices/productSlice";

export const store = configureStore({
  reducer: {
    auth: authSlice,
    product: productSlice
  },
});

export const persistor = persistStore(store);
