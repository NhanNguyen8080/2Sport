// src/components/ProductList.js
import React, { useEffect, useState } from 'react';
import { fetchProducts, fetchProductsSorted  } from '../services/productService';
import { Link } from 'react-router-dom';

const ProductList = ({ sortBy }) => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    const getProducts = async () => {
      try {
        const productsData = sortBy ? await fetchProductsSorted(sortBy) : await fetchProducts();
        setProducts(productsData);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    getProducts();
  }, [sortBy]);

  return (
    <div className="">
      <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
        {products.map(product => (
          <Link key={product.id} to={`/product/${product.id}`}>
            <div className="bg-white hover:drop-shadow-lg">
              <div className="relative">
                <img src={product.mainImagePath} alt={product.mainImageName} className="w-full h-48 object-cover mb-4" />
                <button className="absolute bottom-0 left-0 right-0 flex items-center justify-center bg-gray-900 bg-opacity-75 text-white opacity-0 hover:opacity-100 transition-opacity duration-300 py-2">
                  ADD TO CART
                </button>
              </div>
              <h2 className="text-xl font-semibold mb-2">{product.productName}</h2>
              <p className="text-gray-700 mb-2">{product.brandName}</p>
              <p className="text-gray-700 mb-2">{product.price} VND</p>
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default ProductList;
