import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { selectShipment, selectShipments } from '../../redux/slices/shipmentSlice';
import { Dialog, Transition } from '@headlessui/react';
import { Fragment } from 'react';
import "react-toastify/dist/ReactToastify.css";
import UpdateShipment from './UpdateShipment';
import DeleteShipment from './DeleteShipment';

export default function ShipmentList() {
    const dispatch = useDispatch();
    const shipments = useSelector(selectShipment);
    const [selectedShipment, setSelectedShipment] = useState(null);
    const [isOpen, setIsOpen] = useState(false);

    const handleSelectShipment = (shipment) => {
        dispatch(selectShipments(shipment));
        setSelectedShipment(shipment);
        setIsOpen(false);
    };

    function closeModal() {
        setIsOpen(false);
    }

    function openModal() {
        setIsOpen(true);
    }

    return (
        <>
            <div>
                <button
                    type="button"
                    onClick={openModal}
                    className="border-r-2 pr-4"
                >
                    Address archives
                </button>
            </div>
            <Transition appear show={isOpen} as={Fragment}>
                <Dialog as="div" className="relative z-10" onClose={closeModal}>
                    <div className="fixed inset-0 overflow-y-auto">
                        <div className="flex items-center justify-center min-h-screen">
                            <Dialog.Panel className="bg-white p-6 rounded-md shadow-xl">
                                {shipments.length === 0 ? (
                                    <p>Your address book is empty</p>
                                ) : (
                                    <div>
                                        <div>
                                            <h2 className="font-bold text-lg">My Address</h2>
                                        </div>
                                        {shipments.map((shipment) => (
                                            <div className="p-4 border-b flex justify-between" key={shipment.id}>
                                                <input
                                                    type="radio"
                                                    name="selectedShipment"
                                                    onChange={() => handleSelectShipment(shipment)}
                                                    checked={selectedShipment?.id === shipment.id}
                                                />
                                                <div>
                                                    <div className="flex">
                                                        <label className="pr-2">{shipment.fullName}</label>
                                                        <p className="border-l-2 pl-2">{shipment.phoneNumber}</p>
                                                    </div>
                                                    <p>{shipment.address}</p>
                                                </div>
                                                <div className="flex">
                                                    <UpdateShipment shipment={shipment} />
                                                </div>

                                            </div>
                                        ))}
                                    </div>
                                )}
                            </Dialog.Panel>
                        </div>
                    </div>
                </Dialog>
            </Transition>
        </>
    );
}
