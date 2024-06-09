import axios from 'axios';

const API_BASE_URL = 'https://twosportapi.azurewebsites.net/';

export const getShipmentDetails = ( token) => {

  return axios.get(`${API_BASE_URL}/list-shipment-details`,{
    headers: {
      'Accept': '*/*',
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  });
};
