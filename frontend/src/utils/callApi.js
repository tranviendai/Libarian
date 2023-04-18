import axios from "axios";

const baseURL = 'http://localhost:5137/api';

const CallApi = axios.create({
    baseURL
});

const CallApiWithToken = (token) => {   

    return axios.create({
        baseURL, headers: {
            Authorization: `Bearer ${token}`
        }
    })
};


export default CallApi;
export {CallApiWithToken}