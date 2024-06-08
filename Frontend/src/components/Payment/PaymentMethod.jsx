// PaymentMethod.js
import React from "react";

const PaymentMethod = ({ selectedOption, handleOptionChange }) => {
  return (
    <div className="pl-20 py-10">
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
                You have selected Cash on Delivery (COD) method.
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
                You have selected Bank Transfer method.
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
                You have selected Momo method.
              </p>
            )}
          </div>
        </form>
      </div>
    </div>
  );
};

export default PaymentMethod;
