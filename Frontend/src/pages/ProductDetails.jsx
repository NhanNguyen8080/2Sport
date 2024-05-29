import { Rating } from "@material-tailwind/react";
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchProductById } from '../services/productService';

export default function ProductDetails() {
  const { id } = useParams();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    const getProduct = async () => {
      try {
        const productData = await fetchProductById(id);
        setProduct(productData);
      } catch (error) {
        console.error('Error fetching product details:', error);
      }
    };

    getProduct();
  }, [id]);

  if (!product) {
    return <div>Loading...</div>;
  }
  return (
    <div className="px-5 flex flex-row justify-center items-center gap-5">
      <div className="product-details basis-1/2">
        <h4 className="text-lg text-orange-500">SHOES</h4>
        <h2 className="text-2xl font-bold text-black ">{product.productName}</h2>
        <div className="flex text-orange-500 mt-2">
          <Rating />
          <span className="text-gray-600 ml-2">(15)</span>
        </div>
        {product.price}
        <p className="text-gray-600 my-4 py-2.5">
          {product.description}
        </p>
        <h4 className="text-lg font-bold text-black">Size</h4>
        <div className="inline-block relative w-20 my-2 mx-4">
          <select className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline">
            <option>39</option>
            <option>40</option>
            <option>41</option>
            <option>42</option>
          </select>
          <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
            <svg
              className="fill-current h-4 w-4"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
            >
              <path d="M7 10l5 5 5-5H7z" />
            </svg>
          </div>
        </div>
      </div>

      <div className="product-images basis-1/2 flex flex-row">
      <img src={product.mainImagePath} alt={product.mainImageName} className="w-full h-96 object-cover mb-4" />
      </div>
    </div>
  );
}
