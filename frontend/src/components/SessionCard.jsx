import styles from '../styles/SessionCard.module.css'
import { UserRole } from '../model/user/userRoles/enums/UserRole';

const SessionCard = ({ session, onClick }) => {
  const admin = session.users.find(u => u.role === UserRole.ADMIN)?.userName || "Unknown";
  const attendeeCount = session.users.filter(u => u.role === UserRole.Attendee).length;
  const createdDate = new Date(session.createdAt).toLocaleString();

  return (
    <div key={session.sessionID} onClick={onClick} className={styles.container}>
      <div className={styles.header} > {session.sessionTopic}</div>
      <div className={styles.cardInfo}>
        <p><strong>Admin:</strong> {admin}</p>
        <p><strong>Attendees:</strong> {attendeeCount}</p>
        <p><strong>Created:</strong> {createdDate}</p>
      </div>
    </div>
  ); 
};

export default SessionCard;
