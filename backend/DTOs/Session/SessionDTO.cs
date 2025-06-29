namespace Askii.backend.DTOs.Session
{
    public class SessionDTO
    {
        public string SessionID { get; set; }
        public string SessionTopic { get; set; }
        public DateTime CreatedAt { get; set; }

        // New: List of users with roles
        public List<SessionUserDTO> Users { get; set; }
    }
}
