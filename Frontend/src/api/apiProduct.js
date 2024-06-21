import axios from 'axios';

const API_BASE_URL = 'https://twosportapiv2.azurewebsites.net/api/Product';

export const getProductList = (perPage=15, sortBy = '') => {
  const url = `${API_BASE_URL}/list-products`;
  const params = {};
  if (sortBy) {
    params.perPage = 15;
    params.sortBy = sortBy;
  } else {
    params.perPage = 15;
    params.sortBy = '""'; 
  }
  return axios.get(url, {perPage,
    params,
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