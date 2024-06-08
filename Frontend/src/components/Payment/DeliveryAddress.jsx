import React, { useEffect, useState } from "react";
import { Input, Button } from "@material-tailwind/react";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";
import SignInModal from "../Auth/SignInModal";
import ShipmentList from "./ShipmentList";
import { selectedShipment } from "../../redux/slices/shipmentSlice";
import { selectUser } from "../../redux/slices/authSlice";

const DeliveryAddress = ({ userData, setUserData, setIsEditing }) => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const user = useSelector(selectUser);
  const shipment = useSelector(selectedShipment);
  const token = localStorage.getItem('token');

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

  const handleSaveClick = async () => {
    setIsSubmitting(true);
    try {
      const response = await axios.post('https://twosportapi.azurewebsites.net/add-shipment-detail', {
        fullName: userData.fullName,
        address: userData.address,
        phoneNumber: userData.phoneNumber,
      }, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });

      if (response.status === 200) {
        setIsSubmitting(false);
        setIsEditing(false);
        alert("Shipment details saved successfully.");
      }
    } catch (error) {
      setIsSubmitting(false);
      console.error('Error saving shipment details:', error);
      alert("An error occurred while saving shipment details.");
    }
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
      ):( <ShipmentList />)}
      <div className="flex flex-col pt-4 flex-1">
        <Input
          className="text-black w-full"
          size="lg"
          placeholder="Full name"
          value={userData.fullName}
          onChange={handleInputChange}
          name="fullName"
        />
      </div>
      <div className="flex flex-row pt-4 space-x-2 w-full">
        <Input
          className="text-black w-[450px]"
          size="lg"
          placeholder="Phone number"
          type="tel"
          value={userData.phoneNumber}
          onChange={handleInputChange}
          name="phoneNumber"
        />
      </div>
      <div className="flex flex-col pt-4 flex-1">
        <Input
          className="text-black w-full"
          size="lg"
          placeholder="Address"
          value={userData.address}
          onChange={handleInputChange}
          name="address"
        />
      </div>
      <div className="flex justify-end pt-4">
        <Button
          className="bg-[#FA7D0B] text-white w-20"
          onClick={handleSaveClick}
          disabled={isSubmitting}
        >
          Save
        </Button>
      </div>
    </div>
  );
};

export default DeliveryAddress;
