import axios from "axios";

const CallApi = axios.create({
    baseURL: 'http://localhost:5137/api'
});

export default CallApi;