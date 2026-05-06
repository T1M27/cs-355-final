import axios from "axios";

const API_BASE_URL = "https://localhost:7280/api";

export default {
    fetchChallenge: () => {
        return axios.get(`${API_BASE_URL}/challenges`);
    },
    deleteChallenge: (challengeId) => {
        return axios.delete(`${API_BASE_URL}/challenges/${challengeId}`);
    },
    addChallenge: (challenge) => {
        return axios.post(`${API_BASE_URL}/challenges`,challenge);
    },
    updateChallenge: (challenge) => {
        return axios.put(`${API_BASE_URL}/challenges/${challenge.id}`,challenge)
    }
}