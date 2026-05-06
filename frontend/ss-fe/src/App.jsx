import { useState, useEffect } from 'react'
import './App.css'
import APIService from './services/APIService'
import Challenge from './components/Challenge'
import UserNotification from './components/UserNotification'
import Registration from './components/Registration'
import {Routes, Route, useNavigate} from 'react-router-dom';
import NavBar from './components/NavBar.jsx';
import Login from './components/Login'
import AddEditChallenge from './components/AddEditChallenge.jsx'

function App() {
  const [challenges, setChallenges] = useState([]);
  const [notification, setNotification] = useState(null);
  const [challengeToEdit, setChallengeToEdit] = useState(null);
  const navigate = useNavigate();
  
  function loadChallenges(){
    setNotification("Loading...");
    APIService.fetchChallenge()
      .then((response) => {
        setChallenges(response.data);
        setNotification(null);
      })
      .catch((error) => {
        if (error.response) {
          setNotification("Server inactive: " + error.response.status)
        } else if (error.request) {
          setNotification("Couldn't talk to server.")
        } else {
          setNotification("No entry")
        }
      })
  }
  useEffect(() => {
    loadChallenges();
  }
    , []);

  return (
    <>
      {notification && <UserNotification message={notification} />}
      <h2>Skill Sprint</h2>
      <Routes>
        <Route path="/registration" element={<Registration onCompletion={setNotification}/>}/>
        <Route path="/login" element={<Login onCompletion={setNotification} refreshChallenges={loadChallenges}/>}/>
        <Route path="/challenge/change" element={<AddEditChallenge onSuccess={loadChallenges} onError={setNotification} challenge={challengeToEdit}/>}/>
        <Route path="/" element={challenges.map((chall) => (
        <Challenge key={chall.id} challenge={chall}/>
        ))}/>
      </Routes>
    </>
  )
}

export default App
