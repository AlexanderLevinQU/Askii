using Askii.backend.Model.Enums;

namespace Askii.backend.DTOs.Session
{

    public class SessionUserDTO
    {
        public string UID { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}
