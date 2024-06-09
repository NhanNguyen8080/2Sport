// Checkout.js
import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Button } from "@material-tailwind/react";
import DeliveryAddress from '../components/Payment/DeliveryAddress';
import PaymentMethod from '../components/Payment/PaymentMethod';
import OrderSuccess from '../components/Payment/OrderSuccess';

const Checkout = () => {
  const location = useLocation();
  const { selectedProducts } = location.state || { selectedProducts: [] };

  const [selectedOption, setSelectedOption] = useState(null);
  const [orderSuccess, setOrderSuccess] = useState(false);
  const [orderCode] = useState("123456");
  const [isEditing, setIsEditing] = useState(false);
  const [userData, setUserData] = useState({
    fullName: "",
    email: "",
    phoneNumber: "",
    address: "",
  });

  const totalPrice = selectedProducts.reduce((acc, item) => acc + item.totalPrice, 0);

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value);
  };

  const handleCheckout = () => {
    setOrderSuccess(true);
  };

  if (orderSuccess) {
    return <OrderSuccess orderCode={orderCode} />;
  }

  return (
    <div className="px-5 py-5 flex flex-row bg-slate-200">
      <div className="text-nowrap basis-2/3 bg-white mx-2 pr-14">
        <DeliveryAddress
          userData={userData}
          setUserData={setUserData}
          isEditing={isEditing}
          setIsEditing={setIsEditing}
        />
        <PaymentMethod
          selectedOption={selectedOption}
          handleOptionChange={handleOptionChange}
        />
      </div>
      <div className="basis-1/3 mx-2 h-1/4">
        <div className="font-rubikmonoone text-center p-5 border rounded  text-black">
          Order Summary
        </div>
        {selectedProducts.length === 0 ? (
          <div className="flex justify-center items-center py-4 text-center">
            <p className="text-lg text-black">No items selected for checkout.</p>
          </div>
        ) : (
          <div>
            {selectedProducts.map(item => (
              <div key={item.id} className="flex items-center p-4 border rounded my-4">
                <div className="w-1/4">
                  <img src={item.mainImagePath} alt={item.mainImageName} className="w-16 h-16 object-cover" />
                </div>
                <div className="w-1/2 pr-10">
                  <h3 className="text-lg font-semibold">{item.productName}</h3>
                </div>
                <div className="w-1/4 text-right">
                  <p className="text-lg text-black">{item.totalPrice} VND</p>
                </div>
              </div>
            ))}
            <div className="h-px bg-gray-300 my-5 mx-auto font-bold"></div>
            <div className="flex justify-between items-center pt-1 border rounded mt-4">
              <h3 className="text-lg font-semibold">Subtotal</h3>
              <p className="text-lg text-black">{totalPrice} VND</p>
            </div>
          </div>
        )}
        <div className="flex justify-center items-center mt-4">
          <Button
            className="text-white bg-orange-500 w-40 py-3 rounded"
            onClick={handleCheckout}
          >
            Checkout
          </Button>
        </div>
      </div>
    </div>
  );
};

export default Checkout;
