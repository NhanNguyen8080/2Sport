import axios from 'axios';

const API_BASE_URL = 'https://twosportapi.azurewebsites.net/api/Auth';

export const signIn = (userName, password) => {
  return axios.post(`${API_BASE_URL}/sign-in`, {
    userName,
    password,
  }, {
    headers: {
      'accept': '*/*'
    }
  });
};

export const signUp = (userData) => {
    return axios.post(`${API_BASE_URL}/sign-up`, userData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  };

  export const signOut = (data) => {
    return axios.post(`${API_BASE_URL}/sign-out`,
      data, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  };