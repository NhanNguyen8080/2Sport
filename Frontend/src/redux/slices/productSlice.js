import { createSlice } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

const productPersistConfig = {
    key: "product",
    storage,
};

const initialState = {
    products: [],
    total: 0, 
};

const productSlice = createSlice({
    name: "product",
    initialState,
    reducers: {
        setProducts: (state, action) => {
            const { products } = action.payload.data;
            state.products = products;
        },
        setTotalProducts: (state, action) => {
            state.total = action.payload;
        },
    },
});

export const { setProducts, setTotalProducts } = productSlice.actions;

export const selectProducts = (state) => state.product;

export default persistReducer(productPersistConfig, productSlice.reducer);
