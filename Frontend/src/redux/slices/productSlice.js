import { createSlice } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

const productPersistConfig = {
    key: "product",
    storage,
};
const initialState = {
    data: [],
};
const productSlice = createSlice({
    name: "product",
    initialState,
    reducers: {
        setProducts: (state, action) => {
            state.data = action.payload.data;
        },
        // updateProduct: (state, action) => {
        //     const index = state.data.findIndex(product => product.id === action.payload.id);
        //     if (index !== -1) {
        //         state.data[index] = action.payload;
        //     }
        // },
    },
});

export const { setProducts } = productSlice.actions;

export const selectProducts = (state) => state.product.data;

export default persistReducer(productPersistConfig, productSlice.reducer);
