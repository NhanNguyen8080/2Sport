import axios from 'axios';

const API_BASE_URL = 'https://twosportapi.azurewebsites.net/api/Brand';

export const getAllBrands = () => {
  return axios.get(`${API_BASE_URL}/list-all`, {
    headers: {
      'accept': '*/*'
    }
  });
};