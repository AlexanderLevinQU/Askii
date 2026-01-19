import axios from "axios";
import { useEffect, useState } from "react";
import styles from '../styles/QuestionPanel.module.css'
import QuestionCard from "./QuestionCard";
import { UserRole } from "../model/user/userRoles/enums/UserRole";

const QuestionPanel = ({ currentUserUID, session, onClose }) => {

  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [joinKey, setJoinKey] = useState("");


  const refreshQuestions = () => {
    if (session == null) return;

    setLoading(true);
    axios.get(`/api/question/session/${session.sessionID}`, {
      params: { userId: currentUserUID }
    })
      .then((res) => setQuestions(res.data))
      .catch((err) => {
        console.error("Failed to fetch questions:", err);
        setQuestions([]);
      })
      .finally(() => setLoading(false));
  }

  useEffect(() => {
    if (!session || !currentUserUID) return;
    refreshQuestions(); //fine because we won't have that many questions. Can change if this app is actually used
    const sessionLeaders = session.users.filter(user => user.role === UserRole.ADMIN || user.role === UserRole.MODERATOR);
    var isUserASessionLeader = sessionLeaders.some(
      leader => leader.uid === currentUserUID);
    if (isUserASessionLeader) setJoinKey(session.sessionID);
    else setJoinKey("");
  }, [session]);


  return (
    <div className={styles.container}>
      {joinKey && <p><strong>Invite Key: </strong>{joinKey}</p>}
      <div className={styles.header}> {session.sessionTopic}
        <button className={styles.closeButton} onClick={onClose}>✕</button>
      </div>
      {questions.map((question) => (
        <QuestionCard
          key={question.questionID}
          question={question}
          currentUserUID={currentUserUID}
          onVote={refreshQuestions}
        />
      ))}
    </div>
  );
};

export default QuestionPanel;