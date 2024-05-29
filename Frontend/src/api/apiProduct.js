// src/api/productApi.js
import axios from 'axios';

const API_BASE_URL = 'https://localhost:7276/api/Product';

export const getProductList = (sortBy = '') => {
  const url = `${API_BASE_URL}/list-products`;
  const params = {};
  if (sortBy) {
    params.sortBy = sortBy;
  } else {
    params.sortBy = '""'; // To ensure the same behavior as your original working code
  }
  return axios.get(url, {
    params,
    headers: {
      'accept': '*/*'
    }
  });
};

export const getProductById = (id) => {
  return axios.get(`${API_BASE_URL}/${id}`, {
    headers: {
      'accept': '*/*'
    }
  });
};

export const getProductSortBy = (sortBy = '') => {
  const url = `${API_BASE_URL}/filter-sort-products`;
  const params = { sortBy, perPage: 10, currentPage: 0, isAscending: true };
  return axios.get(url, {
    params,
    headers: {
      'accept': '*/*'
    }
  });
};