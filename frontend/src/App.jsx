import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
/*
import SessionsPage from './pages/SessionsPage';
import SessionPage from './pages/SessionPage';
import QuestionPage from './pages/QuestionPage';
import UserProfilePage from './pages/UserProfilePage';
import NotFoundPage from './pages/NotFoundPage';
*/

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        
        <Route path="/sessions" element={<SessionsPage />} />
        {/*
        <Route path="/session/:SessionID" element={<SessionPage />} />
        <Route path="/question/:QuestionID" element={<QuestionPage />} />
        <Route path="/user/:UID" element={<UserProfilePage />} />
        <Route path="*" element={<NotFoundPage />} />
        */}
      </Routes>
    </Router>
  );
}

export default App;

