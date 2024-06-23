// src/services/productService.js
import { toast } from 'react-toastify';
import { getProductList, getProductById, getProductFilterBy   } from '../api/apiProduct';

export const fetchProducts = async (sortBy = '') => {
  try {
    const response = await getProductList(sortBy);
    const { total, data } = response.data;
    return { total, products: data.$values };
  } catch (error) {
    console.error('Error fetching products:', error);
    throw error;
  }
};

export const fetchProductsFiltered = async (brandIds, categoryIds, minPrice, maxPrice) => {
  try {
    const response = await getProductFilterBy(brandIds, categoryIds, minPrice, maxPrice);
    const { total, data } = response.data;
    return { total, products: data.$values };
  } catch (error) {
    console.error('Error fetching sorted products:', error);
    throw error;
  }
};

export const fetchProductById = async (id) => {
  try {
    const response = await getProductById(id);
    return response.data;
  } catch (error) {
    console.error(`Error fetching product with id ${id}:`, error);
    throw error;
  }
};