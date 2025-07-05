import { useState, useEffect } from 'react';
import axios from 'axios';
import SessionNavbar from './SessionNavBar';
import SessionCard from './SessionCard';

const SessionCardList = ({ currentUserUID, onSessionClick }) => {
  const [selectedTab, setSelectedTab] = useState('Admin');
  const [sessions, setSessions] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    
    if (!currentUserUID) return;

    setLoading(true);
    axios.get(`/api/user/${currentUserUID}/sessions`, {
        params: { role: selectedTab },
      })
      .then((res) => setSessions(res.data))
      .catch((err) => {
        console.error("Failed to fetch sessions:", err);
        setSessions([]);
      })
      .finally(() => setLoading(false));
      
  }, [selectedTab, currentUserUID]);

  return (
    <div>
        <SessionNavbar selectedTab={selectedTab} onSelectTab={setSelectedTab} />
        {loading ? (
        <p>Loading {selectedTab.toLowerCase()} sessions...</p>
        ) : sessions.length === 0 ? (
        <p>No {selectedTab.toLowerCase()} sessions found.</p>
        ) : (
        sessions.map((session) => (
            <SessionCard 
                key={session.sessionID} 
                session={session} 
                onClick={() => onSessionClick(session)}
            />
            ))
        )}
    </div>
  );
};

export default SessionCardList;
