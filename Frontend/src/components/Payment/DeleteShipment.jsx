import React from 'react';
import { deleteUserShipmentDetail } from '../../services/shipmentService';

export default function DeleteShipment({ id, token }) {

  const handleDeleteShipment = async () => {
    try {
      const response = await deleteUserShipmentDetail(id, token);
      console.log(response); 
    } catch (error) {
      console.error('Error deleting shipment:', error);
    }
  };

  return (
    <>
      <div>
        <button
          type="button"
          onClick={handleDeleteShipment}
          className=""
        >
          Delete
        </button>
      </div>
    </>
  );
}