import AuthService from "../services/AuthService";
import { useState } from "react";
import {Link, useNavigate} from "react-router-dom";
import axios from "axios";

export default function Login({onCompletion, refreshChallenges}) {

    const [formData, setFormData] = useState({
        username: "",
        password: ""
    })

    const navigate = useNavigate();

    function handleChange(e) {
        const { name, value } = e.target;
        setFormData(prevState => (
            { ...prevState, [name]: value }
        ))
    }

    function handleSubmit(e){
        e.preventDefault();
        AuthService.loginUser(formData)
        .then((response) => {
            onCompletion("Successfully logged in.");
            console.log(response.data);
            localStorage.setItem("token",response.data.token);
            localStorage.setItem("user",JSON.stringify(response.data));
            axios.defaults.headers.common["Authorization"] = `Bearer ${response.data.token}`;
            refreshChallenges();
            navigate("/");
        })
        .catch((error) => {
            onCompletion("Wong passwod bud");
        })
    }
    return (
        <>
            <form onSubmit={handleSubmit}>
                <input type="text" name="username" value={formData.username} onChange={handleChange} required />
                <input type="password" name="password" value={formData.password} onChange={handleChange} required />
                <button type="submit">Login</button>
            </form>
            <Link to="/registration">It's registering time!!</Link>
        </>
    )
}