import { getAllBrands } from '../api/apiBrand';
import { useTranslation } from "react-i18next";

export const fetchBrands = async () => {
  const { t } = useTranslation();
  try {
    const response = await getAllBrands();
    return response.data.data.$values;
  } catch (error) {
    console.error(`${t("brand.error_fetching_brand_data")}:`, error);
    throw error;
  }
};