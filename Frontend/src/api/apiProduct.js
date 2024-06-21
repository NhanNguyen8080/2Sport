import axios from 'axios';

const API_BASE_URL = 'https://twosportapiv2.azurewebsites.net/api/Product';

export const getProductList = (perPage, currentPage, sortBy = '', isAscending = true) => {
  const url = `${API_BASE_URL}/list-products`;
  const params = { perPage, currentPage, sortBy, isAscending };
  return axios.get(url, {
    params,
    headers: {
      'accept': '*/*'
    }
  });
};

export const getTotalProducts = () => {
  const url = `${API_BASE_URL}/list-products`;
  return axios.get(url, {
    headers: {
      'accept': '*/*'
    }
  });
};


export const getProductById = (id) => {
  const url = `${API_BASE_URL}/get-product/${id}`;
  return axios.get(url, {
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