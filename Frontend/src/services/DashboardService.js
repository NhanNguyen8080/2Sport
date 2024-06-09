import { fetchOrders as apiFetchOrders } from '../api/apiDashboard';
import { toast } from "react-toastify";

export const fetchOrders = async (token) => {
  try {
    const orders = await apiFetchOrders(token);
    toast.success("Orders fetched successfully");
    return orders;
  } catch (error) {
    console.error('Error fetching orders:', error);
    toast.error("Error fetching orders: " + error.message);
    throw error;
  }
};