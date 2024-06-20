import { toast } from "react-toastify";
import { fetchOrdersAPI, fetchOrdersbyStatusAPI } from "../api/apiDashboard";

export const fetchOrders = async () => {
  try {
    const  response = await fetchOrdersAPI();
    toast.success("Orders fetched successfully");
    return  response.data.$values;
  } catch (error) {
    console.error('Error fetching orders:', error);
    toast.error("Error fetching orders: " + error.message);
    throw error;
  }
};

export const fetchOrdersbyStatus = async () => {
  try {
    const  response = await fetchOrdersbyStatusAPI();
    toast.success("Orders fetched successfully");
    return  response.data;
  } catch (error) {
    console.error('Error fetching orders:', error);
    toast.error("Error fetching orders: " + error.message);
    throw error;
  }
};

