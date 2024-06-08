import React from 'react';
import { Menu, MenuHandler, MenuList, MenuItem, Button } from "@material-tailwind/react";
import { useDispatch, useSelector } from 'react-redux';
import { selectShipment, selectShipments,  } from '../../redux/slices/shipmentSlice';

export default function ShipmentList() {
    const dispatch = useDispatch();
    const shipments = useSelector(selectShipment);

    const handleSelectShipment = (shipment) => {
        dispatch(selectShipments(shipment));
    };

    return (
        <Menu>
            <MenuHandler>
                <label>Address archives</label>
            </MenuHandler>
            <MenuList>
                {shipments.map((shipment) => (
                    <MenuItem key={shipment.id} onClick={() => handleSelectShipment(shipment)}>
                        <div className="p-4 border-b hover:bg-zinc-200">
                            <label>{shipment.fullName}</label>
                            <p>{shipment.address}</p>
                        </div>
                    </MenuItem>
                ))}
            </MenuList>
        </Menu>
    );
}
