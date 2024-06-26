import { addToCartAPI, getCartAPI, reduceCartItemAPI, remmoveCartItemAPI, updateCartItemQuantityAPI } from '../api/apiCart';
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

export const addToCart = async (productId, quantityToAdd, token) => {
  const { t } = useTranslation();
  // console.log(quantity, token, productId);
  try {
    const response = await addToCartAPI(productId, quantityToAdd, token);
    // toast.success("Product added to cart successfully");
    return response.data;
  } catch (error) {
    console.error(`${t("cart.add_to_cart_failed")}:`, error);
    toast.error(`${t("cart.add_to_cart_failed")}: ${error.message}`);
    throw error;
  }
};

export const getUserCart = async (sortBy = '') => {
  const { t } = useTranslation();
  try {
    const response = await getCartAPI(sortBy);
    return response.data.data.$values;
  } catch (error) {
    // console.error('Error fetching cart:', error);
    // toast.error('Error fetching cart');
    throw error;
  }
};

export const reduceCartItem = async (id, token) => {
  const { t } = useTranslation();
  try {
    const response = await reduceCartItemAPI(id, token);
    return response;
  } catch (error) {
    console.error(`${t("cart.error_fetching_cart")}:`, error);
    // toast.error('Error fetching cart');
    throw error;
  }
};

export const removeCartItem = async (id, token) => {
  const { t } = useTranslation();
  try {
    const response = await remmoveCartItemAPI(id, token);
    return response;
  } catch (error) {
    console.error(`${t("cart.error_fetching_cart")}:`, error);
    toast.error(`${t("cart.error_fetching_cart")}: ${error.message}`);
    throw error;
  }
};

export const updateCartItemQuantity = async (cartItemId, quantity, token) => {
  const { t } = useTranslation();
  try {
    const response = await updateCartItemQuantityAPI(cartItemId, quantity, token);
    toast.success(`${t("cart.cart_item_quantity_updated_successfully")}`);
    return response.data;
  } catch (error) {
    console.error(`${t("cart.error_updating_cart_item_quantity")}:`, error);
    toast.error(`${t("cart.error_updating_cart_item_quantity")}: ${error.message}`);
    throw error;
  }
};