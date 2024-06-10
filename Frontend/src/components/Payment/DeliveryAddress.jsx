import React, { useEffect, useState } from "react";
import { Input, Button } from "@material-tailwind/react";
import { useDispatch, useSelector } from "react-redux";
import SignInModal from "../Auth/SignInModal";
import ShipmentList from "./ShipmentList";
import { selectedShipment, selectShipment } from "../../redux/slices/shipmentSlice";
import { selectUser } from "../../redux/slices/authSlice";
import DistanceCalculator from "./DistanceCalculator";
import AddShipment from "./AddShipment";
import { useNavigate } from 'react-router-dom';
import { addUserShipmentDetail } from "../../services/shipmentService";

const DeliveryAddress = ({ userData, setUserData, setIsEditing, setDistance }) => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const user = useSelector(selectUser);
  const shipments = useSelector(selectShipment);
  const shipment = useSelector(selectedShipment);
  const token = localStorage.getItem('token');
  const navigate = useNavigate();

  useEffect(() => {
    if (shipment) {
      setUserData({
        fullName: shipment.fullName,
        address: shipment.address,
        phoneNumber: shipment.phoneNumber,
      });
    }
  }, [shipment, setUserData]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setUserData({ ...userData, [name]: value });
  };

  const handleSaveClick = async (data) => {
    setIsSubmitting(true);
    try {
      const response = await addUserShipmentDetail(token, data);

      if (response.status === 200) {
        setIsSubmitting(false);
        setIsEditing(false);
        alert("Shipment details saved successfully.");
      }
    } catch (error) {
      setIsSubmitting(false);
      console.error('Error saving shipment details:', error);
    }
  };

  const handleCancel = () => {
    navigate('/cart');
  };

  return (
    <div className="pl-20 py-10">
      <div className="flex justify-between items-center">
        <h3 className="text-2xl font-bold">Delivery Address</h3>
      </div>
      
      {!user ? (
        <div className="flex items-center">
          <p className="text-xl pr-5">Already have an account?</p>
          <SignInModal className="text-blue-500" />
        </div>
      ) : (
        <>
          <ShipmentList />
          {shipment && (
            <div>
              <h4>Selected Shipment:</h4>
              <p>Full Name: {shipment.fullName}</p>
              <p>Address: {shipment.address}</p>
              <p>Phone Number: {shipment.phoneNumber}</p>
            </div>
          )}
        </>
      )}

      {userData.address && (
        <DistanceCalculator
          userAddress={userData.address}
          onDistanceCalculated={(distance) => {
            console.log("Calculated Distance:", distance);
            setDistance(distance);
          }}
        />
      )}
    </div>
  );
};

export default DeliveryAddress;
