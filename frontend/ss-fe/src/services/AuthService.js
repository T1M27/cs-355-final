import axios from "axios";

const API_BASE_URL = "https://localhost:7280";

export default {
    registerUser: (userData) => {
        return axios.post(`${API_BASE_URL}/register`, userData);
    },
    loginUser: (userCred) => {
        return axios.post(`${API_BASE_URL}/api/login`, userCred)
    }
}