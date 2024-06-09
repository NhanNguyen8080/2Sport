// components/ManageAccount.js

import React from "react";
import { Link, Outlet } from "react-router-dom";

function ManageAccount() {
  return (
    <div className="flex flex-col md:flex-row">
      <nav className="md:w-1/4 p-4 bg-gray-100">
        <ul className="space-y-4">
          <Link to="/manage-account/profile">
            <li className="text-orange-500 font-semibold">My Profile</li>
          </Link>
          <Link to="/manage-account/shipment">
            <li className="text-gray-500">Address Book</li>
          </Link>
          <li className="text-gray-500">My Payment Options</li>
          <li className="text-gray-500">My Returns</li>
          <li className="text-gray-500">My Cancellations</li>
          <li className="text-gray-500">My Wishlist</li>
        </ul>
      </nav>
      <div className="md:w-3/4 p-8">
        <Outlet />
      </div>
    </div>
  );
}

export default ManageAccount;
