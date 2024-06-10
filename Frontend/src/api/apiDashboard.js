import axios from 'axios';

const API_BASE_URL = 'https://twosportapi.azurewebsites.net/api/Order/get-all-orders';

export const fetchOrders = async (token) => {
  try {
    const response = await axios.get(API_BASE_URL, {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });
    return response.data.$values;
  } catch (error) {
    console.error('Error fetching orders:', error);
    throw error;
  }
};

