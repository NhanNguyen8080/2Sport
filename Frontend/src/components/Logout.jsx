// import React from "react";
// import { useNavigate } from "react-router-dom";
// import { useDispatch } from "react-redux";
// import { logout } from "../redux/slices/authSlice";
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
// import { faRightFromBracket } from '@fortawesome/free-solid-svg-icons'

// const Logout = () => {
//     const navigate = useNavigate();
//     const dispatch = useDispatch();
//     // Get all cookies
//     const cookiesArray = document.cookie.split(';');
//     let token = null;
//     let refreshToken = null;

//     // Iterate through the array to find the token and refreshToken
//     cookiesArray.forEach(cookie => {
//         const [name, value] = cookie.trim().split('=');
//         if (name === 'token') {
//             token = value;
//         } else if (name === 'refreshToken') {
//             refreshToken = value;
//         }
//     });

//     console.log('Token:', token);
//     console.log('RefreshToken:', refreshToken);

//     const handleLogout = () => {
//         document.cookie = "token=; path=/; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
//         document.cookie = "refreshToken=; path=/; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
//         dispatch(logout());
//         navigate("/");
//     };


//     return (
//         <button onClick={handleLogout} className="">
//             <FontAwesomeIcon className="pr-1" icon={faRightFromBracket} />
//             Logout
//         </button>
//     );
// };

// export default Logout;


// import React from "react";
// import { useNavigate } from "react-router-dom";
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import { faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
// import axios from 'axios';

// const Logout = () => {
//     const navigate = useNavigate();

//     const handleLogout = async () => {
//         // Get all cookies
//         const cookiesArray = document.cookie.split(';');
//         let token = null;
//         let refreshToken = null;

//         // Iterate through the array to find the token and refreshToken
//         cookiesArray.forEach(cookie => {
//             const [name, value] = cookie.trim().split('=');
//             if (name === 'token') {
//                 token = value;
//             } else if (name === 'refreshToken') {
//                 refreshToken = value;
//             }
//         });

//         console.log('Token:', token);
//         console.log('RefreshToken:', refreshToken);

//             try {
//               const response = await axios.post('https://localhost:7276/api/Auth/sign-out', {
//                 token: token,
//                 refreshToken: refreshToken
//               }, {headers: {
//                 'Content-Type': 'application/json'
//             }}
//             );
//               console.log(response);
//               navigate('/login');
//             } catch (error) {
//               console.error('There was an error making the request!', error);
//             }
//     };

//     return (
//         <button onClick={handleLogout} className="">
//             <FontAwesomeIcon className="pr-1" icon={faRightFromBracket} />
//             Logout
//         </button>
//     );
// };

// export default Logout;
import React from "react";
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import { useDispatch } from "react-redux";
import { logout } from "../redux/slices/authSlice";

const Logout = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleLogout = async () => {
        // // Get all cookies
        // const cookiesArray = document.cookie.split(';');
        // let token = null;
        // let refreshToken = null;

        // // Iterate through the array to find the token and refreshToken
        // cookiesArray.forEach(cookie => {
        //     const [name, value] = cookie.trim().split('=');
        //     if (name === 'token') {
        //         token = value;
        //     } else if (name === 'refreshToken') {
        //         refreshToken = value;
        //     }
        // });

        // console.log('Token:', token);
        // console.log('RefreshToken:', refreshToken);

        // // Ensure tokens are not null
        const token = window.localStorage.getItem('token');
        const refreshToken = window.localStorage.getItem('refreshToken');
        if (!token || !refreshToken) {
            console.error('Token or RefreshToken is missing!');
            return;
        }

        const data = {
            token: token,
            refreshToken: refreshToken
        };

        console.log('Request Payload:', JSON.stringify(data));

        try {
            const response = await axios.post('https://localhost:7276/api/Auth/sign-out', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            console.log('Response:', response.data);
            // Optionally, handle logout success (e.g., redirect to login page)
            localStorage.removeItem('token');
            localStorage.removeItem('refreshToken');

            navigate('/');
            dispatch(logout());
        } catch (error) {
            console.error('There was an error making the request!', error);
            console.error('Response data:', error.response?.data);
        }
    };

    return (
        <button onClick={handleLogout} className="">
            <FontAwesomeIcon className="pr-1" icon={faRightFromBracket} />
            Logout
        </button>
    );
};

export default Logout;
