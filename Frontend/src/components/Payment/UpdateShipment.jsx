import React, { useState, useEffect } from 'react';
import { Dialog, Transition } from '@headlessui/react';
import { Fragment } from 'react';
import "react-toastify/dist/ReactToastify.css";
import { updateUserShipmentDetail } from '../../services/shipmentService';
import { useDispatch } from 'react-redux';
import { updateShipment } from '../../redux/slices/shipmentSlice';

export default function UpdateShipment({ shipment }) {
    const [isOpen, setIsOpen] = useState(false);
    const [formData, setFormData] = useState({ ...shipment });
    const token = localStorage.getItem('token');
    const dispatch = useDispatch();

    useEffect(() => {
        setFormData({ ...shipment });
    }, [shipment]);

    const handleUpdateShipment = async () => {
        try {
            const updatedShipment = await updateUserShipmentDetail(shipment.id, token, formData);
            dispatch(updateShipment(updatedShipment));
            setIsOpen(false);
        } catch (error) {
            console.error('Error updating shipment details:', error);
        }
    };

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value });
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
                    className=""
                >
                    Update
                </button>
            </div>
            <Transition appear show={isOpen} as={Fragment}>
                <Dialog as="div" className="relative z-10" onClose={closeModal}>
                    <div className="fixed inset-0 overflow-y-auto">
                        <div className="flex items-center justify-center min-h-screen">
                            <Dialog.Panel className="bg-white p-6 rounded-md shadow-xl">
                                <Dialog.Title className="text-lg font-bold">Update Shipment</Dialog.Title>
                                <div className="mt-4">
                                    <label className="block text-sm font-medium">Full Name</label>
                                    <input
                                        type="text"
                                        name="fullName"
                                        value={formData.fullName}
                                        onChange={handleInputChange}
                                        className="mt-1 block w-full border rounded-md p-2"
                                    />
                                </div>
                                <div className="mt-4">
                                    <label className="block text-sm font-medium">Phone Number</label>
                                    <input
                                        type="tel"
                                        name="phoneNumber"
                                        value={formData.phoneNumber}
                                        onChange={handleInputChange}
                                        className="mt-1 block w-full border rounded-md p-2"
                                    />
                                </div>
                                <div className="mt-4">
                                    <label className="block text-sm font-medium">Address</label>
                                    <input
                                        type="text"
                                        name="address"
                                        value={formData.address}
                                        onChange={handleInputChange}
                                        className="mt-1 block w-full border rounded-md p-2"
                                    />
                                </div>
                                <div className="mt-6 flex justify-end">
                                    <button
                                        type="button"
                                        className="bg-gray-500 text-white px-4 py-2 rounded-md mr-2"
                                        onClick={closeModal}
                                    >
                                        Cancel
                                    </button>
                                    <button
                                        type="button"
                                        className="bg-blue-500 text-white px-4 py-2 rounded-md"
                                        onClick={handleUpdateShipment}
                                    >
                                        Confirm
                                    </button>
                                </div>
                            </Dialog.Panel>
                        </div>
                    </div>
                </Dialog>
            </Transition>
        </>
    );
}
