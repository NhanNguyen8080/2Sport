import axios from 'axios';

const API_BASE_URL = 'https://twosportapi.azurewebsites.net/api/Order';

export const fetchOrdersAPI = () => {
  return axios.get(`${API_BASE_URL}/get-all-orders`, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};
