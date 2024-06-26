import { fetchAllUsers as apiFetchAllUsers } from '../api/apiManageUser';
import { toast } from "react-toastify";
import { useTranslation } from "react-i18next";

export const fetchAllUsers = async (token) => {
  const { t } = useTranslation();
  try {
    const users = await apiFetchAllUsers(token);
    toast.success(`${t("manage_user.users_fetched_successfully")}`);
    return users;
  } catch (error) {
    console.error(`${t("manage_user.error_fetching_users")}:`, error);
    toast.error(`${t("manage_user.error_fetching_users")}: ${error.message}`);
    throw error;
  }
};

