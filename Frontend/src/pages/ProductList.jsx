import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchProducts, fetchProductsSorted } from '../services/productService';
import { addToCart } from '../services/cartService';
import { selectProducts, setProducts } from '../redux/slices/productSlice';
import { toast } from "react-toastify";
import { addCart } from '../redux/slices/cartSlice';

const ProductList = ({ sortBy }) => {
  const dispatch = useDispatch();
  const { products } = useSelector(selectProducts) || { products: [] };
  const [quantity, setQuantity] = useState(0);

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
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };
    getProducts();
  }, [sortBy, dispatch]);

  const handleAddToCart = async (product) => {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const newQuantity = quantity + 1;
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
          <div key={product.id} className="bg-white hover:drop-shadow-lg">
            <div className="relative">
              <img src={product.mainImagePath} alt={product.mainImageName} className="w-full h-48 object-cover mb-4" />
              <button
                className="absolute bottom-0 left-0 right-0 flex items-center justify-center bg-gray-900 bg-opacity-75 text-white opacity-0 hover:opacity-100 transition-opacity duration-300 py-2"
                onClick={() => handleAddToCart(product)}
              >
                ADD TO CART
              </button>
            </div>
            <Link to={`/product/${product.id}`}>
              <h2 className="text-xl font-semibold mb-2">{product.productName}</h2>
              <p className="text-gray-700 mb-2">{product.brandName}</p>
              <p className="text-gray-700 mb-2">{product.price} VND</p>
              <p className="text-gray-700 mb-2">{product.likes} </p>
              {product.reviews?.$values.map(review => (
                <p key={review.id} className="text-gray-700 mb-2">{review.star}</p>
              ))}
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProductList;
