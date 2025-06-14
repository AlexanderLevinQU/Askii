using System.ComponentModel.DataAnnotations;

namespace Askii.backend.Model
{
    public class User
    {
        public User()
        {
            //basic constructor
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        [Required]
        public string UID { get; set; }

        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<Session> AttendedSessions { get; set; }
        public List<Session> ModeratedSessions { get; set; }
        public List<Session> AdministeredSessions { get; set; }

        // Optional metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}