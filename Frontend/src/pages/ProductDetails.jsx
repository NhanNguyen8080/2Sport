// src/components/ProductDetail.js
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchProductById } from '../services/productService';
import { Rating } from "@material-tailwind/react";

const ProductDetails = () => {
  const { productId } = useParams();
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getProduct = async () => {
      try {
        const productData = await fetchProductById(productId);
        setProduct(productData);
        setLoading(false);
      } catch (error) {
        setError(error);
        setLoading(false);
      }
    };

    getProduct();
  }, [productId]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error loading product details.</div>;
  }

  return (
    <>
      <div className="product-detail">
        {product && (
          <div className="px-5 flex flex-row justify-center items-center gap-5">
            <div className="product-details basis-1/2">
              <h4 className="text-lg text-orange-500">SHOES</h4>
              <h2 className="text-2xl font-bold text-black ">{product.productName}</h2>
              <div className="flex text-orange-500 mt-2">
                {product.price}VND
                <p className="text-gray-600 my-4 py-2.5">
                  <div dangerouslySetInnerHTML={{ __html: product.description }} />
                </p>
                <Rating />
                <span className="text-gray-600 ml-2">(15)</span>
              </div>
            </div>
            <p>Price: {product.price} VND</p>
            <h4 className="text-lg font-bold text-black">Size</h4>
            <div className="inline-block relative w-20 my-2 mx-4">
              <select className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline">
                <option>39</option>
                <option>40</option>
              </select>
            </div>
            <div className="product-images basis-1/2 flex flex-row">
              <img src={product.mainImagePath} alt={product.mainImageName} className="w-full h-96 object-cover mb-4" />
            </div>
            </div>
        )}
          </div>
          </>
      );
};

      export default ProductDetails;