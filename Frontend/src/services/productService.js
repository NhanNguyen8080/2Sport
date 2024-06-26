// src/services/productService.js
import { toast } from 'react-toastify';
import { getProductList, getProductById, getProductFilterBy } from '../api/apiProduct';
import { useTranslation } from "react-i18next";

export const fetchProducts = async (currentPage) => {
  try {
    const response = await getProductList(currentPage);
    const { total, data } = response.data;
    return { total, products: data.$values };
  } catch (error) {
    console.error(`${t("product.error_fetching_products")}:`, error);
    throw error;
  }
};

export const fetchProductsFiltered = async (
      sortBy,
      isAscending,
      selectedBrands,
      selectedCategories,
      minPrice,
      maxPrice
    ) => {
  try {
    const response = await getProductFilterBy(
      sortBy,
      isAscending,
      selectedBrands,
      selectedCategories,
      minPrice,
      maxPrice
    );
    const { total, data } = response.data;
    const { t } = useTranslation();
    return { total, products: data.$values };
  } catch (error) {
    console.error(`${t("product.error_fetching_sorted_products")}:`, error);
    throw error;
  }
};

export const fetchProductById = async (id) => {
  const { t } = useTranslation();
  try {
    const response = await getProductById(id);
    return response.data;
  } catch (error) {
    console.error(`${t("product.error_fetching_product_with_id")}:`, error);
    throw error;
  }
};