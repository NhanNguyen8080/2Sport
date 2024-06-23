import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import { getUserShipmentDetails } from "../../services/shipmentService";
import { useDispatch, useSelector } from "react-redux";
import {
  selectShipment,
  selectShipments,
  setShipment,
} from "../../redux/slices/shipmentSlice";
import UpdateShipment from "../Payment/UpdateShipment";
import DeleteShipment from "../Payment/DeleteShipment";
import AddShipment from "../Payment/AddShipment";
import { useTranslation } from "react-i18next";

const UserShipment = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const shipments = useSelector(selectShipment);
  const [isUpdateModalOpen, setIsUpdateModalOpen] = useState(false);
  const [currentShipment, setCurrentShipment] = useState(null);
  const { t } = useTranslation();

  useEffect(() => {
    const getShipment = async () => {
      try {
        const token = localStorage.getItem("token");
        if (token) {
          const shipmentData = await getUserShipmentDetails(token);
          dispatch(setShipment(shipmentData.$values));
        }
      } catch (error) {
        console.error("Error fetching shipment:", error);
      }
    };

    getShipment();
  }, [dispatch]);

  const openUpdateModal = (shipment) => {
    setCurrentShipment(shipment);
    setIsUpdateModalOpen(true);
    setIsShipmentListOpen(false);
  };
  const closeUpdateModal = () => {
    setIsUpdateModalOpen(false);
    setIsShipmentListOpen(true);
  };

  return (
    <div className="container mx-auto px-20 py-5">
      {shipments.length === 0 ? (
        <p>{t("user_shipment.empty")}</p>
      ) : (
        <div>
          <div className="flex items-center justify-between">
            <h2 className="font-rubikmonoone text-2xl">{t("user_shipment.address")}</h2>
            <AddShipment />
          </div>
          {shipments.map((shipment) => (
            <div
              className="p-4 border-b flex justify-between"
              key={shipment.id}
            >
              <div>
                <div className="flex">
                  <label className="pr-2">{shipment.fullName}</label>
                  <p className="border-l-2 pl-2">{shipment.phoneNumber}</p>
                </div>
                <p>{shipment.address}</p>
              </div>
              <div className="flex space-x-4">
                <button
                  className="rounded-lg p-2 text-orange-500 hover:bg-orange-500 hover:text-white"
                  type="button"
                  onClick={() => openUpdateModal(shipment)}
                >
                  {t("user_shipment.update")}
                </button>
                <DeleteShipment id={shipment.id} />
              </div>
            </div>
          ))}
        </div>
      )}
      {isUpdateModalOpen && (
        <UpdateShipment shipment={currentShipment} onClose={closeUpdateModal} />
      )}
    </div>
  );
};

export default UserShipment;
