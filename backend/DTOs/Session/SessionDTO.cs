namespace Askii.backend.DTOs.Session
{
    public class SessionDTO
    {
        // Primary key
        public string SessionID { get; set; }

        // Just IDs of users related to the session
        public List<string> SessionModeratorUIDs { get; set; }
        public List<string> SessionAttendeeUIDs { get; set; }

        // 1-to-many: One admin leads many sessions
        public string SessionAdminUID { get; set; }
        public string SessionAdminUserName { get; set; }
        public string SessionTopic { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}
