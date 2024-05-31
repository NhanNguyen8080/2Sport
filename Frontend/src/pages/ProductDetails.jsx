// src/components/ProductDetail.js
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchProductById } from '../services/productService';

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
    <div className="product-detail">
      {product && (
        <>
          <img src={product.mainImagePath} alt={product.mainImageName} />
          <h1>{product.productName}</h1>
          <div  dangerouslySetInnerHTML={{ __html: product.description }} />
          <p>Price: {product.price} VND</p>
        </>
      )}
    </div>
  );
};

export default ProductDetails;
