using Askii.backend.Model.Enums;

namespace Askii.backend.DTOs.Session
{
    public class CreateSessionDTO
    {

        // 1-to-many: One admin leads many sessions
        public string SessionAdminUID { get; set; }
        public UserRole CreatorRole { get; set; } = UserRole.Admin; // default to Admin
        public string SessionTopic { get; set; }
    }
}
