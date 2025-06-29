import SessionCard from './SessionCard';

//Can change how it is sorted in the future
const SessionCardList = ({ sessions }) => {
  const sortedSessions = [...sessions].sort(
    (a, b) => new Date(b.session.createdAt) - new Date(a.session.createdAt)
  );

  return (
    <>
      {sortedSessions.map(({ session }) => (
        <SessionCard key={session.sessionID} session={session} />
      ))}
    </>
  );
};

export default SessionCardList;
