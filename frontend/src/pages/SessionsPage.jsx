import { useState, useEffect } from "react";
import { useUser } from "../contexts/UserContext";
import axios from "axios";

function Sessions()
{
    const [userSessions, setUserSessions] = useState([]);
    const [adminSessions, setAdminSessions] = useState([]);
    const [moderatedSessions, setModeratedSessions] = useState([]);
    const { user } = useUser();

    useEffect(() => {

        console.log(user);
        if (!user?.user?.uid) return;

        axios.get(`/api/user/${user.user.uid}/attended-sessions`)
            .then(response => {
                setUserSessions(response.data);
            })
            .catch(error => {
                console.error("Failed to fetch user sessions:", error);
            });
        
        axios.get(`/api/user/${user.user.uid}/moderated-sessions`)
            .then(response => {
                setModeratedSessions(response.data);
            })
            .catch(error => {
                console.error("Failed to fetch user sessions:", error);
            });

        axios.get(`/api/user/${user.user.uid}/admin-sessions`)
            .then(response => {
                setAdminSessions(response.data);
            })
            .catch(error => {
                console.error("Failed to fetch user sessions:", error);
            });

    }, [user]);
    
    /*console.log(userSessions);
    console.log(moderatedSessions);
    */
    console.log(adminSessions);

    const myAdminSessions = adminSessions.map((session) => (
        <div key={session.sessionID} className="session-card">
            <h3>{session.sessionTopic}</h3>
            <p><strong>Admin:</strong> {session.sessionAdminUserName}</p>
            <p><strong>Attendees:</strong> {session.sessionAttendeeUIDs?.length}</p>
            <p><strong>Created:</strong> {new Date(session.createdAt).toLocaleString()}</p>
        </div>
    )).sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));

    return (  
        <div>
            <ul>{myAdminSessions}</ul>
        </div>
    );
}

export default Sessions;