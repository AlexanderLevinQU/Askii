import styles from '../styles/QuestionCard.module.css'

const QuestionCard = ({ question }) => {
  const askerUserName = question.askerUserName|| "Unknown";
  const questionContent = question.content;
  const questionVotes = question.votes;
  const createdDate = question.createdAt;

  return (
    <div key={question.questionID} className={styles.container}>
      <div className={styles.header} > {questionContent} </div>
      <div className={styles.cardInfo}>
        <p><strong>Asker:</strong> {askerUserName}</p>
        <p><strong>Votes:</strong> {questionVotes}</p>
        <p><strong>Created:</strong> {createdDate}</p>
      </div>
    </div>
  );
};

export default QuestionCard;
