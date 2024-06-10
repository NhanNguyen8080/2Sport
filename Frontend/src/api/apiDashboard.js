import axios from 'axios';

const API_BASE_URL = 'https://localhost:7276/api/Order/get-all-orders';

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

// export const getTotalOrder = async () => {
//   try {
//     const url = `https://localhost:7276/api/Order/get-orders-sales?month=month`;
//     const response = await axios.get(url);
//     return response.data;
//   } catch (error) {
//     console.error('Error fetching orders by status:', error);
//     throw error;
//   }
// };


