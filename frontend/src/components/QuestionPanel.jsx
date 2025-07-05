import axios from "axios";
import { useEffect, useState } from "react";

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

    console.log(questions);

  return (
    <div className="question-panel">
      <button className="close-button" onClick={onClose}>✕</button>
      <h2>Questions for {session.sessionTopic}</h2>
      {/* Load questions here or show static for now */}
      <p>Session ID: {session.sessionID}</p>
    </div>
  );
};

export default QuestionPanel;