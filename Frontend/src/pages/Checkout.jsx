import { Button, Input } from "@material-tailwind/react";
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import SignInModal from "../components/Auth/SignInModal";
import { useSelector } from "react-redux";
import { selectUser } from "../redux/slices/authSlice";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleCheck, faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import { useLocation } from 'react-router-dom';

function Checkout() {
  const [selectedOption, setSelectedOption] = useState(null);
  const [subtotal, setSubtotal] = useState(0);
  const [shippingFees, setShippingFees] = useState(0);
  const [orderSuccess, setOrderSuccess] = useState(false);
  const [orderCode] = useState("123456");
  const navigate = useNavigate();
  const user = useSelector(selectUser);
  const [isEditing, setIsEditing] = useState(false);
  const location = useLocation();
  const { selectedProducts } = location.state || { selectedProducts: [] };

  const totalPrice = selectedProducts.reduce((acc, item) => acc + (item.price * item.quantity), 0);
  
  const [userData, setUserData] = useState({
    fullName: "",
    email: "",
    phoneNumber: "",
    address: "",
  });

  useEffect(() => {
    if (user) {
      setUserData({
        fullName: user.FullName || "",
        email: user.Email || "",
        phoneNumber: user.Phone || "",
        address: user.Address || "",
      });
    }
  }, [user]);

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value);
  };

  const handleCheckout = () => {
    setOrderSuccess(true);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setUserData({ ...userData, [name]: value });
  };

  const handleEditClick = () => {
    setIsEditing(true);
  };

  if (orderSuccess) {
    return (
      <div className="px-5 py-5 flex flex-row bg-slate-200">
        <div className="whitespace-nowrap w-1/2 bg-white mx-2 pr-14 flex items-center justify-center basis-1/2">
          <div className="text-center">
            <FontAwesomeIcon
              icon={faCircleCheck}
              className="text-green-500 text-8xl"
            />
            <div className="font-bold text-2xl pt-5">Order Success</div>
            <div>
              Order Code <span className="font-bold text-xl">{orderCode}</span>
            </div>
            <div className="text-lg">Thank you for your purchase!</div>
            <div
              className="text-blue-500 font-bold pt-5 text-lg cursor-pointer"
              onClick={() => navigate("/")}
            >
              <div className="inline-block text-2xl">
                <FontAwesomeIcon icon={faArrowLeft} />
              </div>
              <div className="inline-block ml-1 pl-3">Continue Shopping</div>
            </div>
          </div>
        </div>

        <div className="pl-14 pr-12 py-10 basis-1/2">
        <h3 className="text-2xl font-bold text-center">Order Summary</h3>
      {selectedProducts.length === 0 ? (
        <p>No products selected for checkout</p>
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
                <p className="text-lg text-black">{item.price * item.quantity} VND</p>
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
        </div>
      </div>
    );
  }

  return (
    <div className="px-5 py-5 flex flex-row bg-slate-200">
      <div className="text-nowrap basis-2/3 bg-white mx-2 pr-14">
        <div className="pl-20 py-10">
          <div className="flex justify-between items-center">
            <h3 className="text-2xl font-bold">Delivery Address</h3>
            {user && (
              <Button
                className="bg-[#FA7D0B] text-white w-20"
                onClick={handleEditClick}
              >
                Edit
              </Button>
            )}
          </div>
          {!user && (
            <div className="flex items-center">
              <p className="text-xl pr-5">Already have an account?</p>
              <SignInModal className="text-blue-500" />
            </div>
          )}
          <div className="flex flex-col pt-4 flex-1">
            <Input
              className="text-black w-full"
              size="lg"
              placeholder="Full name"
              value={userData.fullName}
              onChange={handleInputChange}
              name="fullName"
              disabled={!isEditing}
            />
          </div>
          <div className="flex flex-row pt-4 space-x-2 w-full">
            <Input
              className="text-black w-[650px]"
              size="lg"
              placeholder="Email"
              type="email"
              value={userData.email}
              onChange={handleInputChange}
              name="email"
              disabled={!isEditing}
            />
            <Input
              className="text-black w-[450px]"
              size="lg"
              placeholder="Phone number"
              type="tel"
              value={userData.phoneNumber}
              onChange={handleInputChange}
              name="phoneNumber"
              disabled={!isEditing}
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
              disabled={!isEditing}
            />
          </div>

          <h3 className="text-2xl font-bold pt-1">Payment method</h3>

          <div className="flex pt-3">
            <form className="bg-white p-6 rounded shadow-md w-full border border-black">
              <div className="mb-4">
                <label className="inline-flex items-center">
                  <input
                    type="radio"
                    name="option"
                    value="1"
                    className="form-radio text-[#FA7D0B]"
                    onChange={handleOptionChange}
                  />
                  <span className="ml-2">Cash on Delivery (COD)</span>
                </label>
                {selectedOption === "1" && (
                  <p className="mt-4 text-black bg-gray-300 p-2 rounded">
                    Bạn đã chọn phương thức thanh toán bằng Cash on
                    Delivery(COD).
                  </p>
                )}
              </div>
              <div className="mb-4">
                <label className="inline-flex items-center">
                  <input
                    type="radio"
                    name="option"
                    value="2"
                    className="form-radio text-[#FA7D0B]"
                    onChange={handleOptionChange}
                  />
                  <span className="ml-2">Bank Transfer</span>
                </label>
                {selectedOption === "2" && (
                  <p className="mt-4 text-black bg-gray-300 p-2 rounded">
                    Bạn đã chọn phương thức thanh toán bằng Bank Transfer.
                  </p>
                )}
              </div>
              <div className="mb-4">
                <label className="inline-flex items-center">
                  <input
                    type="radio"
                    name="option"
                    value="3"
                    className="form-radio text-[#FA7D0B]"
                    onChange={handleOptionChange}
                  />
                  <span className="ml-2">Momo</span>
                </label>
                {selectedOption === "3" && (
                  <p className="mt-4 text-black bg-gray-300 p-2 rounded">
                    Bạn đã chọn phương thức thanh toán bằng MoMo.
                  </p>
                )}
              </div>
            </form>
          </div>
        </div>
      </div>

      <div className="pl-14 pr-12 py-10 basis-1/3">
        <h3 className="text-2xl font-bold text-center">Order Summary</h3>
      {selectedProducts.length === 0 ? (
        <p>No products selected for checkout</p>
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
                <p className="text-lg text-black">{item.price * item.quantity} VND</p>
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
}

export default Checkout;
