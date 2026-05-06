import { useState } from "react";
import AuthService from "../services/AuthService";

export default function Registration({onCompletion}){
    const [formData,setFormData] = useState({
        username: "",
        email: "",
        password: "",
        confirmPassword: "",
        firstName: "",
        lastName: ""
    });

    function handleChange(e){
        const { name,value } = e.target;
        setFormData(prevState => (
            {...prevState,[name]: value}
        ))
    }

    function handleSubmit(e){
        e.preventDefault();
        if(formData.password !== formData.confirmPassword){
            onCompletion("Confirm password doesn't match your password.");
            return;
        }
        AuthService.registerUser(formData)
        .then((response) => {
            onCompletion("Thank you for registering today!");
        })
        .catch((error) => {
            onCompletion("Sorry, couldn't register you right now.");
        })
    }
    return (
        <>
            <div>
                <form onSubmit={handleSubmit}>
                    <input type="text" name="username" value={formData.username} onChange={handleChange} placeholder="Username" required/>
                    <input type="email" name="email" value={formData.email} onChange={handleChange} placeholder="Email" required/>
                    <input type="password" name="password" value={formData.password} onChange={handleChange} placeholder="Password" required/>
                    <input type="password" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} placeholder="Confirm Password" required/>
                    <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} placeholder="First Name" required/>
                    <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} placeholder="Last Name" required/>
                    <button type="submit">Register</button>
                </form>
            </div>
        </>
    )
}