import styles from '../styles/SessionNavbar.module.css';

const SessionNavbar = ({ selectedTab, onSelectTab }) => {
  const tabs = [
    { label: 'Attended', value: 'Attendee' },
    { label: 'Admin', value: 'Admin' },
    { label: 'Moderated', value: 'Moderator' },
  ]; 

  return (
    <div className={styles.navbar}>
      {tabs.map(tab => (
        <button
          key={tab.value}
          className={`${styles.tabButton} ${selectedTab === tab.value ? styles.active : ''}`}
          onClick={() => onSelectTab(tab.value)}
        >
          {tab.label}
        </button>
      ))}
    </div>
  );
};

export default SessionNavbar;
