import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import 'react-toastify/dist/ReactToastify.css';
import { getUserShipmentDetails } from '../../services/shipmentService';
import { useDispatch, useSelector } from 'react-redux';
import { selectShipment, setShipment } from '../../redux/slices/shipmentSlice';

const UserShipment = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const shipments = useSelector(selectShipment);

  useEffect(() => {
    const getShipment = async () => {
      try {
        const token = localStorage.getItem('token');
        if (token) {
          const shipmentData = await getUserShipmentDetails(token);
          dispatch(setShipment(shipmentData.$values));
        }
      } catch (error) {
        console.error('Error fetching shipment:', error);
      }
    };

    getShipment();
  }, [dispatch]);

  return (
    <div className="container mx-auto px-20 py-5">
      {shipments.length === 0 ? (
        <p>Your address book is empty</p>
      ) : (
        <div>
          {shipments.map((shipment) => (
            <div key={shipment.id}>
              <div>
                {shipment.fullName}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default UserShipment;
