import { toast } from "react-toastify";
import { fetchOrdersAPI, fetchOrdersbyStatusAPI } from "../api/apiDashboard";
import { useTranslation } from "react-i18next";

export const fetchOrders = async () => {
  const { t } = useTranslation();
  try {
    const  response = await fetchOrdersAPI();
    toast.success(`${t("dashboard.orders_fetched_successfully")}`);
    return  response.data.$values;
  } catch (error) {
    console.error(`${t("dashboard.error_fetching_orders")}:`, error);
    toast.error(`${t("dashboard.error_fetching_orders")}: ${error.message}`);
    throw error;
  }
};

export const fetchOrdersbyStatus = async () => {
  const { t } = useTranslation();
  try {
    const  response = await fetchOrdersbyStatusAPI();
    toast.success(`${t("dashboard.orders_fetched_successfully")}`);
    return  response.data;
  } catch (error) {
    console.error(`${t("dashboard.error_fetching_orders")}:`, error);
    toast.error(`${t("dashboard.error_fetching_orders")}: ${error.message}`);
    throw error;
  }
};

