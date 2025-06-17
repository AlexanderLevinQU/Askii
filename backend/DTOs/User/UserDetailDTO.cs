using Askii.backend.DTOs.Session;

namespace Askii.backend.DTOs.User
{
    public class UserDetailsDTO : UserDTO
    {
        public List<SessionDTO> AdministeredSessions { get; set; }
        public List<SessionDTO> ModeratedSessions { get; set; }
        public List<SessionDTO> AttendedSessions { get; set; }
    }
}
