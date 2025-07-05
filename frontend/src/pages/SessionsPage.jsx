import { useState } from "react";
import { useUser } from "../contexts/UserContext";
import SessionCardList from "../components/SessionCardList";
import QuestionPanel from "../components/QuestionPanel";
import styles from "../styles/SessionPage.module.css"

function Sessions()
{
    const { user } = useUser();
    const [selectedSession, setSelectedSession] = useState(null);

    const handleSessionClick = (session) => {
        setSelectedSession(session);
    };
 
    const closePanel = () => {
        setSelectedSession(null);
    };

    return (  
        <div className={styles.container}>
            <div className={styles.sessionsListContainer}>
                <h2 className={styles.header}>Sessions</h2>
                    <SessionCardList currentUserUID={user?.user?.uid} 
                        onSessionClick={handleSessionClick}
                    />
            </div>
            {
                selectedSession && 
                (
                    <QuestionPanel session={selectedSession} onClose={closePanel} />
                )
            }
        </div>
    );
}

export default Sessions;