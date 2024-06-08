import axios from 'axios';

const API_BASE_URL = 'https://twosportapi.azurewebsites.net/api/Order';

export const fetchOrders = (token) => {
  return axios.get(API_BASE_URL, {
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  })
  .then(response => response.data)
  .catch(error => {
    console.error('Error fetching orders:', error);
    throw error;
  });
};
