import React, { useEffect, useState } from 'react';
import { fetchProducts, fetchProductsSorted } from '../services/productService';
import { Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { selectProducts, setProducts } from '../redux/slices/productSlice';

const ProductList = ({ sortBy }) => {
  const dispatch = useDispatch();
  const products = useSelector(selectProducts);

  useEffect(() => {
    const getProducts = async () => {
       try {
        let productsData;
        if (sortBy) {
          productsData = await fetchProductsSorted(sortBy);
        } else {
          productsData = await fetchProducts();
        }

        const productsArray = sortBy ? productsData : productsData.products;
        dispatch(setProducts({ data: { products: productsArray, total: productsData.total } }));
        console.log(productsArray);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
      // try {
      //   const productsData = sortBy ? await fetchProductsSorted(sortBy) : await fetchProducts();
      //   dispatch(setProducts({ data: productsData }));
      //   console.log(productsData);
      // } catch (error) {
      //   console.error('Error fetching products:', error);
      // }
    };

    getProducts();
  }, [sortBy, dispatch]);

  return (
    <div className="">
      <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
        {products.products.map(product => (
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
