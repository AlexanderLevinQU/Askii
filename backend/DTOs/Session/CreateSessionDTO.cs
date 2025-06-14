namespace Askii.backend.DTOs.Session
{
    public class CreateSessionDTO
    {

        // 1-to-many: One admin leads many sessions
        public string SessionAdminUID { get; set; }
        public string SessionTopic { get; set; }
    }
}
