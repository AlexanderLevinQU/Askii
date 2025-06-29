using Askii.backend.DTOs.Session;
using Askii.backend.Model.Enums;

namespace Askii.backend.DTOs.User
{
    public class UserSessionDTO
    {
            public SessionDTO Session { get; set; }
            public UserRole Role { get; set; }
    }
}
