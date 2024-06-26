import { getAllCategories } from '../api/apiCategory';
import { useTranslation } from "react-i18next";

export const fetchCategories = async () => {
  const { t } = useTranslation();
  try {
    const response = await getAllCategories();
    return response.data.data.$values;
  } catch (error) {
    console.error(`${t("category.error_fetching_category_data")}:`, error);
    throw error;
  }
};
