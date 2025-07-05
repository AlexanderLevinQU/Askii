import axios from "axios";
import { useEffect, useState } from "react";
import styles from '../styles/QuestionPanel.module.css'
import QuestionCard from "./QuestionCard";

const QuestionPanel = ({ session, onClose }) => {
    
    const [questions, setQuestions] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {

        if (session == null) return;

        setLoading(true);
        axios.get(`api/question/session/${session.sessionID}`)
        .then((res) => setQuestions(res.data))
        .catch((err) => {
            console.error("Failed to fetch sessions:", err);
            setQuestions([]);
        })
        .finally(() => setLoading(false));

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
            />
            ))}
    </div>
  );
};

export default QuestionPanel;