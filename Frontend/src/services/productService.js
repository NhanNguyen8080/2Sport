// src/services/productService.js
import { toast } from 'react-toastify';
import { getProductList, getProductById,getProductSortBy, getTotalProducts   } from '../api/apiProduct';

export const fetchProducts = async (perPage, currentPage, sortBy = '') => {
  try {
    const response = await getProductList(perPage, currentPage, sortBy);
    const { data } = response.data;
    return { products: data.$values };
  } catch (error) {
    console.error('Error fetching products:', error);
    throw error;
  }
};

export const fetchTotalProducts = async () => {
  try {
    const response = await getTotalProducts();
    const { total } = response.data;
    return total;
  } catch (error) {
    console.error('Error fetching total products:', error);
    throw error;
  }
};


export const fetchProductsSorted = async (sortBy = '') => {
  try {
    const response = await getProductSortBy(sortBy);
    return response.data.data.$values;
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