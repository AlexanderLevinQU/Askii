// SessionCard.jsx
const SessionCard = ({ session }) => {
  const admin = session.users.find(u => u.role === 0)?.userName || "Unknown";
  const attendeeCount = session.users.filter(u => u.role === 2).length;
  const createdDate = new Date(session.createdAt).toLocaleString();

  return (
    <div key={session.sessionID} className="session-card">
      <h3>{session.sessionTopic}</h3>
      <p><strong>Admin:</strong> {admin}</p>
      <p><strong>Attendees:</strong> {attendeeCount}</p>
      <p><strong>Created:</strong> {createdDate}</p>
    </div>
  );
};

export default SessionCard;
