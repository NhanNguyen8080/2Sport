import React from 'react';
import { Route, Routes } from 'react-router-dom';
import ProductList from '../pages/ProductList';
import ProductDetails from '../pages/ProductDetails';

const ProductRoutes = () => {
  return (
    <Routes>
      <Route path="/product" element={<ProductList />} />
      <Route path="/product/:productId" element={<ProductDetails />} />
    </Routes>
  );
};

export default ProductRoutes;
