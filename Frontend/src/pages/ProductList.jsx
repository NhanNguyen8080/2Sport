import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchProducts, fetchProductsFiltered } from '../services/productService';
import { addToCart } from '../services/cartService';
import { selectProducts, setProducts } from '../redux/slices/productSlice';
import { toast } from "react-toastify";
import { addCart } from '../redux/slices/cartSlice';
import { Rating } from "@material-tailwind/react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAnglesRight } from '@fortawesome/free-solid-svg-icons';

import { useTranslation } from "react-i18next";

const ProductList = ({ sortBy, isAscending, selectedBrands, selectedCategories, minPrice, maxPrice }) => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const { products } = useSelector(selectProducts) || { products: [] };
  const [quantity, setQuantity] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  // console.log(selectedBrands, selectedCategories);

  useEffect(() => {
    const getProducts = async () => {
      try {
        let productsData;
        if (
          sortBy === "" &&
          selectedBrands.length === 0 &&
          selectedCategories.length === 0 &&
          minPrice === 0 &&
          maxPrice === 3000000
        ) {
          productsData = await fetchProducts();
        } else {
          productsData = await fetchProductsFiltered(
            sortBy,
            isAscending,
            selectedBrands,
            selectedCategories,
            minPrice,
            maxPrice
          );
        }
        await checkWarehouseQuantities(productsData.products);
        dispatch(setProducts({ data: { products: productsData.products, total: productsData.total } }));
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    const fetchWarehouseData = async () => {
      const response = await fetch('https://twosportapiv2.azurewebsites.net/api/Warehouse/list-all', {
        headers: {
          'accept': '*/*'
        }
      });
      const data = await response.json();
      return data.data.$values;
    };

    const checkWarehouseQuantities = async (products) => {
      try {
        const warehouseData = await fetchWarehouseData();
        products.forEach(product => {
          const warehouseProduct = warehouseData.find(w => w.productName === product.productName);
          if (warehouseProduct) {
            product.quantity = warehouseProduct.quantity;
          }
        });
      } catch (error) {
        console.error('Error checking warehouse quantities:', error);
      }
    };

    getProducts();
  }, [sortBy, isAscending, minPrice, maxPrice, selectedCategories, selectedBrands, dispatch]);

  const handleAddToCart = async (product, quantityToAdd = 1) => {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const newQuantity = quantity + quantityToAdd;
        await addToCart(product.id, newQuantity, token);
        setQuantity(newQuantity);
        toast.success(`${product.productName} is added to cart`);
      } catch (error) {
        console.error('Error adding product to cart:', error);
        toast.error('Error adding product to cart');
      }
    } else {
      toast.success(product.productName + " is added to cart");
      dispatch(addCart(product));
    }
  };

  return (
    <div className="">
      <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
        {products?.map(product => (
          <div key={product.id} className="bg-white border hover:drop-shadow-lg  hover:bg-blue-100 p-2">
            <div className="relative">
              <Link to={`/product/${product.id}`}>
                <div className="bg-white">
                  <img src={product.mainImagePath} alt={product.mainImageName} className="object-scale-down h-48 w-96 mb-4" />
                </div>
              </Link>
              {product.quantity === 0 ? (
                <button
                  className="absolute bottom-0 left-0 right-0 flex items-center justify-center bg-gray-400 bg-opacity-75 text-white opacity-100 transition-opacity duration-300 py-4"
                  disabled
                >
                 SOLD OUT
                </button>
              ) : (
                <button
                  className="absolute bottom-0 left-0 right-0 flex items-center justify-center bg-orange-600 bg-opacity-75 text-white opacity-0 hover:opacity-100 transition-opacity duration-300 py-4"
                  onClick={() => handleAddToCart(product)}
                >
                  {t("product_list.add_to_cart")}
                </button>
              )}
            </div>
            <Link to={`/product/${product.id}`}>
              <p className="text-orange-500 mb-2 ">{product.categoryName} - {product.brandName}</p>
              <h2 className="text-xl font-semibold ">{product.productName}</h2>
              <p className="text-gray-700 mb-2">{product.price.toLocaleString()} {t("product_list.vnd")}</p>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProductList;
