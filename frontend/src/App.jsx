import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { useEffect } from 'react';

function App() {
  const [message, setMessage] = useState('Loading...');

  useEffect(() => {
    fetch('http://localhost:5252/api/test')
      .then(response => response.json())
      .then(data => setMessage(data.message))
      .catch(error => setMessage('Error loading message'));
  }, []);

  return <div>{message}</div>;
}

export default App
