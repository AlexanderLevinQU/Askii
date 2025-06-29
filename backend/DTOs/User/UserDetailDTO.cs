namespace Askii.backend.DTOs.User
{
    public class UserDetailsDTO : UserDTO
    {
        public List<UserSessionDTO> Sessions { get; set; }
    }
}
