namespace Askii.backend.DTOs.User
{
    public class UserDTO
    {
        public string UID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
