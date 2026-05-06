import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import heroImg from './assets/hero.png'
import './App.css'
import APIService from './services/APIService'
import { useEffect } from 'react'

function App() {
  const [challenges, setChallenges] = useState([]);
  const [notification, setNotification] = useState(null);
  
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
    challenges.map((chall) => (
      <Challenge key={chall.id} challenge={chall}/>
    ))
  )
}

export default App
