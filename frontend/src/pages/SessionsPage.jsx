import { useState, useEffect } from "react";
import { useUser } from "../contexts/UserContext";
import SessionCardList from "../components/SessionCardList";
import axios from "axios";

function Sessions()
{
    const [userSessions, setUserSessions] = useState([]);
    const [adminSessions, setAdminSessions] = useState([]);
    const [moderatedSessions, setModeratedSessions] = useState([]);
    const { user } = useUser();
    
    useEffect(() => {
        if (!user?.user?.uid) return;
        console.log(user);

        axios.get(`/api/user/${user.user.uid}/user-with-sessions/`) // Unified endpoint returning all sessions with role info
            .then(response => {
            const data = response.data;
            console.log(response.data);
            // Assuming each session includes role info per user session (e.g., session.Users with Role)
            // Filter sessions by role
            setUserSessions(data.sessions.filter(s => s.role === 2));
            setModeratedSessions(data.sessions.filter(s => s.role === 1));
            setAdminSessions(data.sessions.filter(s => s.role === 0));
        })
            .catch(error => {
            console.error("Failed to fetch user sessions:", error);
        });
    }, [user])

    
    console.log(userSessions); 


    return (  
        <div className="admin-sessions">
        <h2>Your Admin Sessions</h2>
            <SessionCardList sessions={adminSessions} />
        <h2>Your Attended Sessions</h2>
            <SessionCardList sessions={userSessions} />
        <h2>Your Moderated Sessions</h2>
            <SessionCardList sessions={moderatedSessions} />
        </div>
    );
}

export default Sessions;