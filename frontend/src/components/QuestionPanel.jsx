import axios from "axios";
import { useEffect, useState } from "react";
import styles from '../styles/QuestionPanel.module.css'
import QuestionCard from "./QuestionCard";

const QuestionPanel = ({ currentUserUID, session, onClose }) => {

  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(true);

  const refreshQuestions = () => {
    if (session == null) return;

    setLoading(true);
    console.log(session.sessionID);
    console.log(currentUserUID);
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
    console.log(questions);
  }, [session]);


  return (
    <div className={styles.container}>
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