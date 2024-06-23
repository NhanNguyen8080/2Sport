import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchProducts, fetchProductsFiltered } from '../services/productService';
import { addToCart } from '../services/cartService';
import { selectProducts, setProducts } from '../redux/slices/productSlice';
import { toast } from "react-toastify";
import { addCart } from '../redux/slices/cartSlice';
import { Rating } from "@material-tailwind/react";

const ProductList = ({ sortBy, selectedBrands, selectedCategories,minPrice, maxPrice }) => {
  const dispatch = useDispatch();
  const { products } = useSelector(selectProducts) || { products: [] };
  const [quantity, setQuantity] = useState(0);

  useEffect(() => {
    const getProducts = async () => {
      try {
        let productsData;
        if (selectedBrands.length === 0 && selectedCategories.length === 0 && minPrice === 0 && maxPrice === 0) {
          productsData = await fetchProducts();
        } else {
          productsData = await fetchProductsFiltered(selectedBrands, selectedCategories, minPrice, maxPrice);
        }
        dispatch(setProducts({ data: { products: productsData.products, total: productsData.total } }));
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };
    getProducts();
  }, [minPrice, maxPrice,selectedCategories, selectedBrands, dispatch]);

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
          <div key={product.id} className="bg-white hover:drop-shadow-lg  hover:bg-zinc-200 p-2">
            <div className="relative">
            <Link to={`/product/${product.id}`}>
              <div className=" bg-white">
                <img src={product.mainImagePath} alt={product.mainImageName} className="object-scale-down h-48 w-96 mb-4" />
              </div>
              </Link>
              <button
                className="absolute bottom-0 left-0 right-0 flex items-center justify-center bg-orange-600 bg-opacity-75 text-white opacity-0 hover:opacity-100 transition-opacity duration-300 py-4"
                onClick={() => handleAddToCart(product)}
              >
                ADD TO CART
              </button>
            </div>
            <Link to={`/product/${product.id}`}>
            <p className="text-orange-500 mb-2 ">{product.categoryName} - {product.brandName}</p>
              <h2 className="text-xl font-semibold ">{product.productName}</h2>
              {/* <p className="text-orange-500 mb-2"></p> */}
              {/* {product.reviews?.$values.map(review => (
                <div key={review.id}>
                  <Rating unratedColor="amber" ratedColor="amber" className="text-yellow-500 mb-2" value={review.star} readonly />
                </div>
              ))} */}
              <p className="text-gray-700 mb-2">{product.price.toLocaleString()} VND</p>

            </Link>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProductList;
