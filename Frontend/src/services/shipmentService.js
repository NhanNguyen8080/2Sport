
import { toast } from "react-toastify";
import { getShipmentDetails } from "../api/apiShipment";

export const getUserShipmentDetails = async (token) => {
  try {
    const response = await getShipmentDetails(token);
    return response.data;
  } catch (error) {
    console.error('Error fetching cart:', error);
    toast.error('Error fetching cart');
    throw error;
  }
};
