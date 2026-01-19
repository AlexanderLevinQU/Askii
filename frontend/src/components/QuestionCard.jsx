import styles from '../styles/QuestionCard.module.css'
import axios from "axios";
import { VoteType } from "../model/votes/enums/VoteType"


const QuestionCard = ({ question, currentUserUID, onVote }) => {
  const askerUserName = question.askerUserName || "Unknown";
  const questionContent = question.content;
  const questionVotes = question.votes;
  const createdDate = question.createdAt;
  const userVote = question.userVote;

  const handleVote = async (voteType) => {
    try {
      if (!question.userVote) { //no vote
        await axios.post(
          `/api/questionvote?questionId=${question.questionID}`,
          {
            userID: currentUserUID,
            voteType: voteType
          }
        );
      } else { // update vote
        if (voteType === userVote.voteType) {
          voteType = VoteType.NoVote;
        }
        await axios.put(`/api/questionvote/${question.userVote.voteID}`, {
          voteType: voteType
        });
      }

      onVote();
    } catch (err) {
      console.error("Failed to vote:", err);
    }
  };

  return (
    <div key={question.questionID} className={styles.container}>
      <div className={styles.header} > {questionContent} </div>
      <div className={styles.cardInfo}>
        <p><strong>Asker:</strong> {askerUserName}</p>
        <p><strong>Votes:</strong> {questionVotes}</p>
        <button onClick={() => handleVote(VoteType.UpVote)} className={styles.voteBtn}>👍</button>
        <button onClick={() => handleVote(VoteType.DownVote)} className={styles.voteBtn}>👎</button>
        <p>
          <strong>You voted:</strong>{" "}
          {userVote?.voteType == VoteType.UpVote && "👍"}
          {userVote?.voteType == VoteType.DownVote && "👎"}
          {(userVote === null || userVote.voteType === VoteType.NoVote) && "You haven't voted"}
        </p>
        <p><strong>Created:</strong> {createdDate}</p>
      </div>
    </div>
  );
};

export default QuestionCard;
