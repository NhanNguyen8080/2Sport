import { toast } from 'react-toastify';
import { checkoutOrder } from '../api/apiPayment';
import { useTranslation } from "react-i18next";

export const checkout = async (token, orderMethodId, data) => {
  const { t } = useTranslation();
  try {
    const response = await checkoutOrder(token, orderMethodId, data);
    return response.data;
  } catch (error) {
    console.error(`${t("payment.error_checking_out_order")}:`, error);
    toast.error(`${t("payment.error_checking_out_order")}: ${error.message}`);
    throw error;
  }
};
