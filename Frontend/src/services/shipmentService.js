
import { toast } from "react-toastify";
import { addShipmentDetail, deleteShipmentDetail, getShipmentDetails, updateShipmentDetail } from "../api/apiShipment";
import { useTranslation } from "react-i18next";

export const getUserShipmentDetails = async (token) => {
  const { t } = useTranslation();
  try {
    const response = await getShipmentDetails(token);
    return response.data;
  } catch (error) {
    console.error(`${t("shipment.error_fetching_cart")}:`, error);
    toast.error(`${t("shipment.error_fetching_cart")}: ${error.message}`);
    throw error;
  }
};

export const addUserShipmentDetail = async (token, data) => {
  const { t } = useTranslation();
  try {
    const response = await addShipmentDetail(token, data);
    return response;
  } catch (error) {
    console.error(`${t("shipment.error_saving_shipment_details")}:`, error);
    toast.error(`${t("shipment.error_saving_shipment_details")}: ${error.message}`);
    throw error;
  }
};

export const updateUserShipmentDetail = async (id, token, data) => {
  const { t } = useTranslation();
  try {
    const response = await updateShipmentDetail(id, token, data);
    return response.data;
  } catch (error) {
    console.error(`${t("shipment.error_updating_shipment_details")}:`, error);
    toast.error(`${t("shipment.error_updating_shipment_details")}: ${error.message}`);
    throw error;
  }
};

export const deleteUserShipmentDetail = async (id, token) => {
  const { t } = useTranslation();
  try {
    const response = await deleteShipmentDetail(id, token);
    return response;
  } catch (error) {
    console.error(`${t("shipment.error_deleting_shipment_details")}:`, error);
    toast.error(`${t("shipment.error_deleting_shipment_details")}: ${error.message}`);
    throw error;
  }
};